namespace zipnaro
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            webView2 = new Microsoft.Web.WebView2.WinForms.WebView2();
            buttonCancel = new Button();
            buttonOK = new Button();
            textBoxAddr = new TextBox();
            label1 = new Label();
            label2 = new Label();
            textBoxZipPath = new TextBox();
            buttonBrowseZipPath = new Button();
            saveFileDialogZipPath = new SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)webView2).BeginInit();
            SuspendLayout();
            // 
            // webView2
            // 
            webView2.AllowExternalDrop = true;
            webView2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            webView2.CreationProperties = null;
            webView2.DefaultBackgroundColor = Color.White;
            webView2.Location = new Point(12, 70);
            webView2.Name = "webView2";
            webView2.Size = new Size(776, 483);
            webView2.TabIndex = 5;
            webView2.ZoomFactor = 1D;
            webView2.NavigationCompleted += webView2_NavigationCompleted;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Location = new Point(713, 559);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 7;
            buttonCancel.Text = "キャンセル";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // buttonOK
            // 
            buttonOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOK.DialogResult = DialogResult.OK;
            buttonOK.Location = new Point(622, 559);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(75, 23);
            buttonOK.TabIndex = 6;
            buttonOK.Text = "開始";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // textBoxAddr
            // 
            textBoxAddr.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxAddr.Location = new Point(115, 6);
            textBoxAddr.Name = "textBoxAddr";
            textBoxAddr.Size = new Size(673, 23);
            textBoxAddr.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 0;
            label1.Text = "アドレス";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 43);
            label2.Name = "label2";
            label2.Size = new Size(77, 15);
            label2.TabIndex = 2;
            label2.Text = "出力ファイル名";
            // 
            // textBoxZipPath
            // 
            textBoxZipPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxZipPath.Location = new Point(115, 40);
            textBoxZipPath.Name = "textBoxZipPath";
            textBoxZipPath.Size = new Size(635, 23);
            textBoxZipPath.TabIndex = 3;
            textBoxZipPath.Text = "d:\\MyKindle\\noname.zip";
            // 
            // buttonBrowseZipPath
            // 
            buttonBrowseZipPath.Location = new Point(756, 40);
            buttonBrowseZipPath.Name = "buttonBrowseZipPath";
            buttonBrowseZipPath.Size = new Size(32, 23);
            buttonBrowseZipPath.TabIndex = 4;
            buttonBrowseZipPath.Text = "...";
            buttonBrowseZipPath.UseVisualStyleBackColor = true;
            buttonBrowseZipPath.Click += buttonBrowseZipPath_Click;
            // 
            // saveFileDialogZipPath
            // 
            saveFileDialogZipPath.DefaultExt = "zip";
            saveFileDialogZipPath.Filter = "Zip files(*.zip)|*.zip|All files(*.*)|*.*";
            // 
            // FormMain
            // 
            AcceptButton = buttonOK;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(800, 599);
            Controls.Add(buttonBrowseZipPath);
            Controls.Add(textBoxZipPath);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBoxAddr);
            Controls.Add(buttonOK);
            Controls.Add(buttonCancel);
            Controls.Add(webView2);
            Name = "FormMain";
            Text = "ZipNano";
            Load += FormMain_Load;
            ((System.ComponentModel.ISupportInitialize)webView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView2;
        private Button buttonCancel;
        private Button buttonOK;
        private TextBox textBoxAddr;
        private Label label1;
        private Label label2;
        private TextBox textBoxZipPath;
        private Button buttonBrowseZipPath;
        private SaveFileDialog saveFileDialogZipPath;
    }
}
