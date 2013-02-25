using System;
using System.Text.RegularExpressions;

namespace GrigCorePlayer.Services
{
    public class TextParser : ITextParser
    {
        #region Methods

        /// <summary>
        /// Transform String Def to String Transto
        /// </summary>
        /// <param name="text"></param>
        /// <param name="def"></param>
        /// <param name="transto"></param>
        /// <returns></returns>
        public string StringTrancformation(string text, string def, string transto)
        {
            var tr = new Regex(def, RegexOptions.IgnoreCase);
            return tr.Replace(text, transto);
        }

        /// <summary>
        /// Replace Ampersand Symbol with 'And'
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string TransformAmpersandSymbol(string text)
        {
            var Amp = new Regex("&", RegexOptions.IgnoreCase);
            return Amp.Replace(text, "and");
        }

        public string ReplaceAmpersand(string inputText)
        {
            return inputText.Replace(@"&amp;", @"&");
        }

        #endregion

        #region Temp

        /// <summary>
        /// Remove HTML from string with Regex.
        /// </summary>
        public string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        /// <summary>
        /// Compiled regular expression for performance.
        /// </summary>
        Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        public string StripTagsRegexCompiled(string source)
        {
            source = StringTrancformation(source, "&quot;", "");
            source = StringTrancformation(source, "amp;", "");
            return _htmlRegex.Replace(source, string.Empty);
        }

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        public string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        public string FormatToTime(string text)
        {
            try
            {
                return TimeSpan.FromSeconds(int.Parse(text)).ToString();
            }
            catch (Exception exception)
            {
                return string.Empty;
            }

        }

        #endregion
    }
}
