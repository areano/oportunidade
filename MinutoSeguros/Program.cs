using MinutoSeguros.DataStructures;
using MinutoSeguros.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinutoSeguros
{
    class Program
    {
        static void Main(string[] args)
        {
            // fetch items from feed
            var fp = new FeedProvider();
            var items = fp.FetchItems("http://www.minutoseguros.com.br/blog/feed/", 10);

            // get each item stats
            var topicsStats = items
                .Select(x => new ItemStats(x))
                .ToList();

            // get top 10 word in all items
            var top10Words = GetTop10Words(topicsStats);

            // print top 10 words
            Console.WriteLine("Dez principais palavras abordadas:");

            foreach (var w in top10Words)
            {
                Console.WriteLine("Palavra: {0} - Quantidade: {1}", w.Word, w.Ocurrences);
            }

            Console.WriteLine();


            // print topic word count
            Console.WriteLine("Quantidade de palavras por tópico:");

            foreach (var t in topicsStats)
            {
                Console.WriteLine("Quantidade de palavras: {0}", t.WordCount);
            }

            Console.ReadKey();
        }

        private static List<WordStats> GetTop10Words(List<ItemStats> topics)
        {
            // aggregate words from all topics
            var wordCountTotals = topics
                .SelectMany(x => x.Words)
                .GroupBy(x => x.Word, x => x.Ocurrences)
                .Select(x => new WordStats(x.Key, x.Sum()));

            // get ten most used
            var top10Words = wordCountTotals
                .OrderByDescending(x => x.Ocurrences)
                .Take(10)
                .ToList();

            return top10Words;
        }
    }
}
