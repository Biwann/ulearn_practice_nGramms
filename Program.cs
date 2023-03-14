using System;
using System.Collections.Generic;
using System.IO;
using NUnitLite;

namespace TextAnalysis
{
    internal static class Program
    {
        public static void Main(string[] args)
        {

            /*var testsToRun = new string[]
            {
                "TextAnalysis.SentencesParser_Tests",
                "TextAnalysis.FrequencyAnalysis_Tests",
                "TextAnalysis.TextGenerator_Tests",
            };
            new AutoRun().Execute(new[]
            {
                "--stoponerror", 
                "--noresult",
                "--test=" + string.Join(",", testsToRun),
            });*/
            Console.WriteLine("Подождите, идет загрузка...");
            var text = File.ReadAllText("test.txt");
            Console.WriteLine("... 10%");
            var sentences = SentencesParserTask.ParseSentences(text);
            Console.WriteLine("... 40%");
            var frequency = FrequencyAnalysisTask.GetMostFrequentNextWords(sentences);
            Console.WriteLine("Готово!");

            while (true)
            {
                Console.WriteLine("Введите текст для продолжения: ");
                var beginning = Console.ReadLine();
                if (string.IsNullOrEmpty(beginning)) return;
                var phrase = TextGeneratorTask.ContinuePhrase(frequency, beginning.ToLower(), 7);
                Console.WriteLine(phrase);
            }
        }
    }
}