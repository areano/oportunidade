using HtmlAgilityPack;
using System;
using System.Linq;

namespace MinutoSeguros.Helpers
{
    /// <summary>
    /// This class contains methods to manipulate HTML code
    /// </summary>
    class HtmlToText
    {
        /// <summary>
        /// Extracts plain text from an HTML string.
        /// </summary>
        /// <param name="html">HTML code to process</param>
        /// <seealso cref="https://htmlagilitypack.codeplex.com/"/>
        /// <returns>Plain text from the HTML (no tags)</returns>
        public static string GetText(string html)
        {
            /// This method uses a third party dependency to process the text. Removing HTML tags could 
            /// be tricky and I considered that is not part of the problem's scope

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var text = htmlDocument.DocumentNode
                .SelectNodes("//text()")
                .Select(x => x.InnerText);

            return String.Concat(text);
        }
    }
}
