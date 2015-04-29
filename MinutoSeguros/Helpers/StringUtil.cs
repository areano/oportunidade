using System.Text.RegularExpressions;

namespace MinutoSeguros.Helpers
{
    /// <summary>
    /// Contains method for string manipulation
    /// </summary>
    class StringUtil
    {
        /// <summary>
        /// Removes non alphabetic characters
        /// </summary>
        /// <param name="text">text to scape</param>
        /// <param name="scapeString">optional string to scape with</param>
        /// <returns>final text without illegal characters</returns>
        public static string ScapeNonAlphabeticCharaters(string text, string scapeString = " ")
        {
            // removes everything but letters
            var rgx = new Regex(@"[^\p{L}]+");

            return rgx.Replace(text, scapeString);
        }
    }
}
