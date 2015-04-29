
namespace MinutoSeguros.DataStructures
{
    /// <summary>
    /// Contains stats related to the given word
    /// </summary>
    class WordStats
    {
        public string Word { get; private set; }
        public int Ocurrences { get; private set; }

        public WordStats(string word, int ocurrences)
        {
            Word = word;
            Ocurrences = ocurrences;
        }
    }
}
