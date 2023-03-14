using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Xml.Linq;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            var text = SentencesParserTask.ParseSentences(phraseBeginning);

            if (text.Count == 0)
                return phraseBeginning;

            var words = text[text.Count - 1];

            for (int i = 0; i < wordsCount; i++)
            {
                AddWord(words, nextWords);
            }

            phraseBeginning = string.Join(" ", words.ToArray());
            return phraseBeginning;
        }

        public static void AddWord(List<string> words, Dictionary<string, string> nextWords)
        {
            var start = new StringBuilder();
            int state = 1;
            int maxNgramm = 3;

            while (state > 0)
            {   
                for (int shift = -maxNgramm; shift < 0; shift++)
                {
                    if (words.Count + shift >= 0)
                    {
                        if (shift == -maxNgramm)
                            start.Append(words[words.Count + shift]);
                        else
                        {
                            start.Append(" ");
                            start.Append(words[words.Count + shift]);
                        }
                    }
                    else
                        break;
                }
                var strStart = start.ToString();

                if (nextWords.ContainsKey(strStart))
                {
                    words.Add(nextWords[strStart]);
                    state = 0;
                }
                else
                {
                    maxNgramm--;
                    start.Clear();
                }

                if (maxNgramm <= 0)
                    state = 0;

            }
            
        }
    }
}