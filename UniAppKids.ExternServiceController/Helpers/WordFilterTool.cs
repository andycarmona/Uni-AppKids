namespace UniAppKids.ExternServiceController.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;

    using Uni_AppKids.Application.Dto;

    public static class WordFilterTool
    {
        public static List<WordDto> processListofWords(List<WordDto> rawWordList)
        {
            var processedWordList = new List<WordDto>();

            return processedWordList;
        }

        public static string RemoveSpecialCharacters(string rawWord)
        {
            rawWord = rawWord.ToLower();
            var specialSymbolFilter = new Regex("(?:[^a-zöäåñáéíóú]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            var strippedWord = specialSymbolFilter.Replace(rawWord, String.Empty);
            return strippedWord;
        }

        public static string CheckUrlIsAValidImageInWord(WordDto aWord)
        {
            using (var wc = new WebClient())
            {
                try
                {
                    wc.DownloadData(aWord.Image);
                }
                catch (Exception)
                {
                    aWord.Image =
                        "http://t1.gstatic.com/images?q=tbn:ANd9GcRI4C4XDT85bAGvjFK2x6BF5J12CxqFFWVz0JuLiJKSFySzdxD9kBGBl1pL";
                }
            }

            return aWord.Image;
        }

        public static void AddExtraInformationWordList(List<WordDto> verifiedWordList)
        {
           
            verifiedWordList.Select(
                aWord =>
                    {
                        aWord.CreationTime = DateTime.Now;
                        aWord.WordDescription = string.IsNullOrEmpty(aWord.WordDescription) ? "No description" : aWord.WordDescription;
                        aWord.Image = CheckUrlIsAValidImageInWord(aWord);
                        return aWord;
                    }).ToList();
        }

        public static List<WordDto> GetListWithValidWordName(List<WordDto> listToVerify)
        {
            return listToVerify.Where(aWord => aWord.WordName != null).ToList();
        }

        public static string RemoveAccentOnVowels(string rawWord)
        {
            var normalized = rawWord.Normalize(NormalizationForm.FormD);
            var builder = new StringBuilder();

            foreach (var ch in normalized.Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark))
            {
                builder.Append(ch);
            }

            return builder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static void GetWordsNotAccepted(List<WordDto> listNoRepeatedElements, string language, string pathToDictionary, out List<string> listOfNotAcceptedWords)
        {
            listOfNotAcceptedWords = (from aWord in listNoRepeatedElements let strippedWord = RemoveSpecialCharacters(aWord.WordName) let removedAccentWord = RemoveAccentOnVowels(strippedWord) let result = CheckWordIsInDictionary(removedAccentWord, language, pathToDictionary) where !result select aWord.WordName).ToList();

            if (listOfNotAcceptedWords != null && listOfNotAcceptedWords.Any())
            {
                throw new FormatException();
            }
        }

        public static bool CheckWordIsInDictionary(string word, string language, string path)
        {
            return (from line in File.ReadLines(path, Encoding.UTF8) select Regex.Match(word, @"\b" + Regex.Escape(line) + @"\b", RegexOptions.IgnoreCase) into match where match.Success select match.Success).FirstOrDefault();
        }

        public static List<WordDto> ListNoRepeatedElements(List<WordDto> wordList)
        {
            return wordList.Distinct(new DistinctItemComparer()).ToList();
        }
    }
}