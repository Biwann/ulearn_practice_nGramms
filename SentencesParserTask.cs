using System.CodeDom;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection.Configuration;
using System;
using NUnit.Framework;
using System.Linq;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            //var sentenceSeps = new char[]; //{ '.', '!', '?' }; // ';', ':', '(', ')' };
            var listWordSeps = new List<char>();

            text = text.ToLower();
            foreach (char c in text)
            {
                if (!char.IsLetter(c) && c != '\'' /*&& !sentenceSeps.Contains(c)*/ && c != '-')
                {
                    if (!listWordSeps.Contains(c))
                        listWordSeps.Add(c);
                }
            }

            //foreach (var sent in text.Split(sentenceSeps, StringSplitOptions.RemoveEmptyEntries))
            //{
                //if (!ContainsWords(sent))
                //    continue;
                var sentence = new List<string>();

                var arrayWordSeps = listWordSeps.ToArray();
                foreach (var word in text.Split(arrayWordSeps, StringSplitOptions.RemoveEmptyEntries))
                {
                    sentence.Add(word);
                }
                sentencesList.Add(sentence);
            //}
            return sentencesList;
        }

        public static bool ContainsWords(string sent)
        {
            foreach(var c in sent)
            {
                if (Char.IsLetter(c))
                    return true;
            }
            return false;
        }
    }
}