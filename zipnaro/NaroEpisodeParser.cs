using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Microsoft.Web.WebView2.Core;
using static System.Net.Mime.MediaTypeNames;
namespace zipnaro
{
    internal partial class NaroEpisodeParser(ZipBook zipBook)
    {
        public class Episode(int epNum, string title, ReadOnlyCollection<string> body)
        {
            public readonly int EpisodeNumber = epNum;
            public readonly string Title = title;
            public readonly ReadOnlyCollection<string> Body = body;
        }

        private readonly ZipBook _zipBook = zipBook;
        private readonly HtmlParser _parser = new ();
        private readonly Encoding _enc = new UTF8Encoding(false);

        public class ParseError(string msg) : Exception(msg)
        {
        }

        public string GetMainHtmlJapascript()
        {
            var strJs = """
                let elemMain = document.getElementsByTagName('main')[0];
                elemMain.outerHTML;
             """;
            return strJs;
        }

        public string ParsePage(string strMain)
        {
            var dom = _parser.ParseDocument("<html><body></body></html>");
            if (dom.Body == null)
            {
                throw new ParseError($"Internal Error");
            }
            var elems = _parser.ParseFragment(strMain, dom.Body);
            var elemMain = (IElement)elems[0];

            var elemEpisodeAnker = elemMain.QuerySelector("a.p-eplist__subtitle");
            if (elemEpisodeAnker != null)
            {
                // 目次ページ
                var strJs = """
                    let anker = [].filter.call(document.getElementsByTagName('a'), a => a.classList.contains('p-eplist__subtitle'))[0];
                    anker.click();
                    'OK';

                """;
                return strJs;
            }
            var elemNovelLine = elemMain.QuerySelector("p#L1");
            if (elemNovelLine != null)
            {
                ParseEpisode(elemMain, elemNovelLine);
                var elemAnkerNext = elemMain.QuerySelector("a.c-pager__item--next");
                if (elemAnkerNext != null)
                {
                    // エピソードページ
                    var strJs = """
                       let anker = [].filter.call(document.getElementsByTagName('a'), a => a.classList.contains('c-pager__item--next'))[0];
                       anker.click();
                       'OK';
                     """;
                    return strJs;
                }
                // 次のページは無い
                return string.Empty;
            }
            // 知らないページ
            throw new ParseError("Unknown Page");
        }

        private void ParseEpisode(IElement elemMain, IElement elemNovelLine)
        {
            // エピソード番号
            var regexNovelNumber = RegexNovelNumber();
            var elemH1NumEp = elemMain.QuerySelector("div.p-novel__number") ?? throw new ParseError("No Title");
            var match = regexNovelNumber.Match(elemH1NumEp.TextContent);
            if (!match.Success)
            {
                throw new ParseError($"Invalid Episode Nuber: {elemH1NumEp.TextContent}");
            }
            var numEpisode = Int32.Parse(match.Groups[1].Value);
            if (numEpisode > 9999)
            {
                throw new ParseError($"Eposode Number Too Big: {numEpisode}");
            }

            // エピソード タイトル
            var elemH1Title = elemMain.QuerySelector("h1.p-novel__title") ?? throw new ParseError("No Title");
            var strTitle = elemH1Title.TextContent;

            // 本文
            List<string> listNovelLine = [];
            for (var elem = elemNovelLine; elem != null; elem = elem.NextElementSibling)
            {
                listNovelLine.Add(elem.TextContent);
            }

            var filename = $"episode_{numEpisode:d4}.txt";
            _zipBook.CreateEpisode(filename, strTitle, listNovelLine);
        }

        [GeneratedRegex(@"^(\d+)/")]
        private static partial Regex RegexNovelNumber();
    }
}
