using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zipnaro
{
    internal class ZipBook : IDisposable
    {
        private readonly string _pathZip;
        private readonly string _pathTemp;
        private readonly FileStream _fs;
        private readonly ZipArchive _zipArch;
        private readonly Encoding _enc = new UTF8Encoding(false);
        private bool disposedValue = false;

        public ZipBook(string pathZip)
        {
            _pathZip = pathZip;
            _pathTemp = $"{_pathZip}.tmp";
            var dirName = Path.GetDirectoryName(_pathZip) ?? string.Empty;
            if (dirName != string.Empty)
            {
                Directory.CreateDirectory(dirName);
            }
            _fs = new FileStream(_pathTemp, FileMode.Create, FileAccess.ReadWrite);
            _zipArch = new ZipArchive(_fs, ZipArchiveMode.Create);
        }

        public void CreateEpisode(string entryName, string title, IEnumerable<string> textLines)
        {
            var entry = _zipArch.CreateEntry(entryName);
            using var sw = new StreamWriter(entry.Open(), _enc);
            sw.WriteLine(title);
            foreach (var line in textLines)
            {
                sw.WriteLine(line);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                }
                _zipArch.Dispose();
                _fs.Dispose();
                if (File.Exists(_pathTemp))
                {
                    File.Move(_pathTemp, _pathZip, true);
                }
                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                disposedValue = true;
            }
        }

        // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
        // ~ZipBook()
        // {
        //     // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
