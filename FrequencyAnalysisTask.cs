using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        /*
        static void Main()
        {

            var text = "x z. x y. x y";
            var parsedText = ParseSentences(text);
            var allCombos = new List<string[]>();

            AddCombos(allCombos, parsedText, 2);
            AddCombos(allCombos, parsedText, 3);

            foreach (var a in allCombos)
            {
                Console.WriteLine(a[0] + " : " + a[1]);
            }
            Console.WriteLine();
            while (allCombos.Count > 0)
            {
                var a = FindOneMostFreq(allCombos);
                Console.WriteLine(a[0] + " : " + a[1]);
            }
        }
        */
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            var allCombos = new Dictionary<string, List<string>>();

            int maxnGramm = 3;
            for (int i = 2; i <= maxnGramm; i++)
                AddCombos(allCombos, text, i);

            foreach (var k in allCombos.Keys)
            {
                var good = FindMostFreq(allCombos[k]);
                result.Add(k, good);
            }

            return result;
        }

        public static string FindMostFreq(List<string> combos)
        {
            string result = "";
            int max = 0;

            while (combos.Count > 0)
            {
                string temp = combos[0];
                int counter = 0;

                for (int i = 0; i < combos.Count;)
                {
                    if (combos[i] == temp)
                    {
                        counter++;
                        combos.RemoveAt(i);
                    }
                    else
                        i++;
                }

                if (counter > max)
                {
                    result = temp;
                    max = counter;
                }
                else if (counter == max && string.CompareOrdinal(temp, result) < 0)
                {
                    result = temp;
                }
            }


            return result;
        }

        public static void AddCombos(Dictionary<string, List<string>> allCombos, List<List<string>> text, int nGramm)
        {

            foreach (List<string> sentence in text)
            {
                for (int i = sentence.Count - 1; i - (nGramm - 1) >= 0; i--)
                {

                    string start = "";

                    for (int j = i - (nGramm - 1); j < i; j++)
                    {
                        if (start == "")
                            start = sentence[j];
                        else
                            start += " " + sentence[j];
                    }

                    if (!allCombos.ContainsKey(start))
                    {
                        var toAdd = new List<string>();
                        toAdd.Add(sentence[i]);
                        allCombos.Add(start, toAdd);
                    }
                    else
                        allCombos[start].Add(sentence[i]);

                }
            }
        }
    }
}