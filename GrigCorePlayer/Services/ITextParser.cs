namespace GrigCorePlayer.Services
{
    public interface ITextParser
    {        
        /// <summary>
        /// Transform String Def to String Transto
        /// </summary>
        /// <param name="text"></param>
        /// <param name="def"></param>
        /// <param name="transto"></param>
        /// <returns></returns>
        string StringTrancformation(string text, string def, string transto);

        /// <summary>
        /// Replace Ampersand Symbol with 'And'
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        string TransformAmpersandSymbol(string text);

        /// <summary>
        /// Remove HTML from string with Regex.
        /// </summary>
        string StripTagsRegex(string source);

        /// <summary>
        /// Replace &amp; with &
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        string ReplaceAmpersand(string inputText);

        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        string StripTagsRegexCompiled(string source);

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        string StripTagsCharArray(string source);


        string FormatToTime(string text);

    }
}
