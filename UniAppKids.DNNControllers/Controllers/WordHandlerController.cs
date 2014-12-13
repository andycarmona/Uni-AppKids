﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WordHandlerController.cs" company="Uni-app">
//   Uni-App E-learning company
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UniAppKids.DNNControllers.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Linq;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Web.Api;
    using UniAppKids.DNNControllers.Helpers;
    using Uni_AppKids.Application.Dto;
    using Uni_AppKids.Application.Services;

    public class WordHandlerController : ControllerBase
    {
        private readonly DictionaryService aDictionaryService = new DictionaryService();

        private readonly WordService aWordService = new WordService();
        private readonly PhraseService aPhraseService = new PhraseService();

        [DnnAuthorize]
        [AcceptVerbs("POST")]
        public async Task<HttpResponseMessage> AddPhrase(string listOfWords, int dictionaryId)
        {
            string[] wordArray = Regex.Split(listOfWords, @"\[(.*?)\]");
            var wordChosen = wordArray.Length - 2;
            var errorMessage = new StringBuilder(string.Empty);
            var listOfNotAcceptedWords = new List<string>();
            const string Delimiter = " ";
            if (wordArray[wordChosen].Length == 0)
            {
                return this.ControllerContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    "Invalid parameters, Please check there is elements in array");
            }

            var language = this.aDictionaryService.GetADictionary(dictionaryId).DictionaryName;
            var wordList = Json.Deserialize<List<WordDto>>("[" + wordArray[wordChosen] + "]");
            var verifiedWordList = WordFilterTool.GetListWithValidWordName(wordList);
            verifiedWordList.Select(c => { c.CreationTime = DateTime.Now; return c; }).ToList();

            try
            {
                var listNoRepeatedElements = WordFilterTool.ListNoRepeatedElements(verifiedWordList);
                var pathToDictionary = HttpContext.Current.Server.MapPath(string.Format("{0}{1}.txt", ConfigurationManager.AppSettings["Dictionary"], language));
                WordFilterTool.GetWordsNotAccepted(
                    listNoRepeatedElements,
                    language,
                    pathToDictionary,
                    out listOfNotAcceptedWords);

                await this.aWordService.BulkInsertOfWords(listNoRepeatedElements);

                if (verifiedWordList.Count == 1)
                {
                    return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
                }

                var sentence = verifiedWordList.Select(i => i.WordName).Aggregate((i, j) => i + Delimiter + j);
                var listOfWordsId = await this.aWordService.GetIdOfWords(verifiedWordList);

                var aPhrase = new PhraseDto
                                  {
                                      PhraseText = sentence,
                                      CreationTime = DateTime.Now,
                                      WordsIds = string.Join(",", listOfWordsId),
                                      AssignedDictionaryId = dictionaryId,
                                      UserName = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo().Username
                                  };

                this.aPhraseService.InsertPhrase(aPhrase);
                return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK, wordList);
            }
            catch (DuplicateKeyException)
            {
                var listRepeatedWords = this.aWordService.GetRepeatedWords(verifiedWordList);
                errorMessage.Append(string.Format(
                    "Cannot insert duplicate words: {0}",
                    string.Join(",", listRepeatedWords)));
            }
            catch (FormatException)
            {
                errorMessage.Append(string.Format(
                    "These words doesn't exist in the {0} dictionary: {1}",
                    language,
                    string.Join(",", listOfNotAcceptedWords)));
            }

            return this.ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, errorMessage.ToString());
        }

        [DnnAuthorize]
        [AcceptVerbs("GET")]
        public HttpResponseMessage GetAllWordsInDictionary()
        {
            var wordList = this.aWordService.GetAllWords();

            if (!wordList.Any())
            {
                return this.ControllerContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    "There is no words in dictionary");
            }

            return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK, wordList);
        }

        [DnnAuthorize]
        [AcceptVerbs("GET")]
        public HttpResponseMessage DeletePhrase(int phraseId)
        {
            try
            {
                aPhraseService.DeletePhrase(phraseId);

                return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return this.ControllerContext.Request.CreateResponse(
             HttpStatusCode.BadRequest,
             "Couldn't delete phrase");
            }
        }

        [DnnAuthorize]
        [AcceptVerbs("GET")]
        public HttpResponseMessage GetAllPhrasesInDictionary(int dictionaryId, int totalPages)
        {
            var phraseList = this.aPhraseService.GetListOfPhrase(dictionaryId, totalPages);

            if (!phraseList.Any())
            {
                return this.ControllerContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    "There is no phrases in dictionary");
            }

            return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK, phraseList);
        }

        [AllowAnonymous]
        [AcceptVerbs("GET")]
        public HttpResponseMessage GetWordsList(int dictionaryId, int indexOfPhraseList, int totalPages)
        {
            string errorMessage;
            try
            {
                var listOfPhrase = this.aPhraseService.GetListOfPhrase(dictionaryId, totalPages);
                foreach (var aPhrase in listOfPhrase)
                {
                    var wordsId = aPhrase.WordsIds;
                    var listOfWords = this.aWordService.GetListOfWordsForAPhrase(wordsId);
                    aPhrase.ListOfWords = listOfWords;
                }

                if (listOfPhrase.Any())
                {
                    return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK, listOfPhrase);
                }

                return this.ControllerContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    "Couldn't find any dictionary.");
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }

            return this.ControllerContext.Request.CreateResponse(
                      HttpStatusCode.BadRequest,
                      errorMessage);
        }

        [AllowAnonymous]
        [AcceptVerbs("GET")]
        public HttpResponseMessage GetDictionary(int dictionaryId)
        {
            var actualDictionary = this.aDictionaryService.GetADictionary(dictionaryId);

            if (actualDictionary != null)
            {
                return this.ControllerContext.Request.CreateResponse(
              HttpStatusCode.OK, actualDictionary);
            }

            return this.ControllerContext.Request.CreateResponse(
                HttpStatusCode.BadRequest,
                "Couldn't find any dictionary.");
        }
    }
}