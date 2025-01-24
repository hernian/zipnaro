using AngleSharp.Common;
using Microsoft.Web.WebView2.Core;
using System.Diagnostics;
using System.Drawing.Text;
using System.IO;
using System.Resources.Tools;
using System.Text;
using System.Text.Json;
using System.Web;

namespace zipnaro
{
    public partial class FormMain : Form
    {
        private const string HTML_COMPL = "<html><head></head><body>Completed.</body></html>";
        private const string HTML_CANCELED = "<html><head></head><body>Canceled.</body></html>";

        private readonly Encoding _enc = new UTF8Encoding(false);
        private ZipBook? _zipBook;
        private NaroEpisodeParser? _parser;
        private bool _cancelSaving = false;

        public FormMain()
        {
            InitializeComponent();
            this.buttonOK.Enabled = false;
            this.buttonCancel.Enabled = false;
        }

        private async void FormMain_Load(object sender, EventArgs e)
        {
            await this.webView2.EnsureCoreWebView2Async(null);
            this.buttonOK.Enabled = true;
        }

        private async void webView2_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            Debug.WriteLine("webView2_NavigationCompleted");
            if (_parser == null)
            {
                this.buttonOK.Enabled = true;
                this.buttonCancel.Enabled = false;
                return;
            }
            try
            {
                var strJsGetMainHTML = _parser.GetMainHtmlJapascript();
                var strMainMTHL = await ExecuteScriptAsync(strJsGetMainHTML);
                var strJsNext = await Task.Run<string>(() => _parser.ParsePage(strMainMTHL));
                if (strJsNext == string.Empty)
                {
                    // 最終ページまで処理できた
                    _parser = null;
                    _zipBook?.Dispose();
                    _zipBook = null;
                    this.webView2.NavigateToString(HTML_COMPL);
                    return;
                }
                if (_cancelSaving)
                {
                    // キャンセルボタンが押されていた
                    _parser = null;
                    _zipBook?.Dispose();
                    _zipBook = null;
                    this.webView2.NavigateToString(HTML_CANCELED);
                    return;
                }
                // 次のページへ遷移する
                await ExecuteScriptAsync(strJsNext);
            }
            catch (Exception exc)
            {
                // エラーが発生した
                _parser = null;
                _zipBook?.Dispose();
                _zipBook = null;
                var sbMsg = new StringBuilder("<html><head><title>Error</title></head><body><h1>Errer:</h1><ul>");
                for (var ex = exc; ex != null; ex = ex.InnerException)
                {
                    var htmlMsg = HttpUtility.HtmlEncode(ex.Message);
                    sbMsg.Append($"<li>{htmlMsg}</li>");
                }
                sbMsg.Append("</ul></body></html>");
                this.webView2.NavigateToString(sbMsg.ToString());
            }
        }

        private async Task<string> ExecuteScriptAsync(string strJs)
        {
            var res = await this.webView2.CoreWebView2.ExecuteScriptWithResultAsync(strJs);
            if (!res.Succeeded)
            {
                DebugWriteScriptErrer(res.Exception);
                return string.Empty;
            }
            string strRes;
            int isValid;
            res.TryGetResultAsString(out strRes, out isValid);
            if (isValid == 0)
            {
                Debug.WriteLine("Scpipt Result Is Not A String.");
                return string.Empty;
            }
            return strRes;
        }

        private void DebugWriteScriptErrer(CoreWebView2ScriptException ex)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Message: {ex.Message}");
            sb.AppendLine($"Name: {ex.Name}");
            sb.AppendLine($"LineNumber: {ex.LineNumber}");
            sb.AppendLine($"ColumnNumber: {ex.ColumnNumber}");
            Debug.Write(sb.ToString());
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (this.textBoxAddr.Text == string.Empty)
            {
                return;
            }
            if (_zipBook != null)
            {
                _zipBook.Dispose();
                _zipBook = null;
            }
            _zipBook = new ZipBook(this.textBoxZipPath.Text);
            _parser = new NaroEpisodeParser(_zipBook);

            _cancelSaving = false;
            this.buttonOK.Enabled = false;
            this.buttonCancel.Enabled = true;
            this.webView2.CoreWebView2.Navigate(this.textBoxAddr.Text);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            _cancelSaving = true;
            this.buttonCancel.Enabled = false;
        }

        private void buttonBrowseZipPath_Click(object sender, EventArgs e)
        {
            var dirOut = Path.GetDirectoryName(this.textBoxZipPath.Text) ?? string.Empty;
            if (dirOut != null)
            {
                this.saveFileDialogZipPath.InitialDirectory = dirOut;
            }
            this.saveFileDialogZipPath.FileName = Path.GetFileName(this.textBoxZipPath.Text) ?? "*.zip";
            if (this.saveFileDialogZipPath.ShowDialog(this) == DialogResult.OK)
            {
                this.textBoxZipPath.Text = this.saveFileDialogZipPath.FileName;
            }
        }
    }
}
