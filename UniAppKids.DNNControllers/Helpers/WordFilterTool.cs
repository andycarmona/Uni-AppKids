namespace UniAppKids.DNNControllers.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    using UniAppSpel.Helpers;

    using Uni_AppKids.Application.Dto;

    public static class WordFilterTool
    {
        public static string RemoveSpecialCharacters(string rawWord)
        {
            rawWord = rawWord.ToLower();
            var specialSymbolFilter = new Regex("(?:[^a-zöäåñ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            var strippedWord = specialSymbolFilter.Replace(rawWord, string.Empty);
            return strippedWord;
        }

        public static void GetWordsNotAccepted(List<WordDto> listNoRepeatedElements, string language, string pathToDictionary, out List<string> listOfNotAcceptedWords)
        {

            listOfNotAcceptedWords = new List<string>();

            foreach (var aWord in listNoRepeatedElements)
            {
                var strippedWord = RemoveSpecialCharacters(aWord.WordName);
                var result = CheckWordIsInDictionary(strippedWord, language, pathToDictionary);

                if (!result)
                {
                    listOfNotAcceptedWords.Add(aWord.WordName);
                }
            }

            if (listOfNotAcceptedWords != null && listOfNotAcceptedWords.Any())
                throw new FormatException();

        }

        public static bool CheckWordIsInDictionary(string word, string language, string path)
        {

            foreach (string line in File.ReadLines(path, Encoding.UTF8))
            {

                var match = Regex.Match(word, @"\b" + Regex.Escape(line) + @"\b", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                  return match.Success;
                }
            }

            return false;
        }

        public static List<WordDto> ListNoRepeatedElements(List<WordDto> wordList)
        {
            return wordList.Distinct(new DistinctItemComparer()).ToList();
        }
    }
}