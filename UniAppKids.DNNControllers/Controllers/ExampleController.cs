namespace UniAppKids.DNNControllers.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;

    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Entities.Users;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Web.Api;

    using UniAppSpel.Helpers;

    using Uni_AppKids.Application.Dto;
    using Uni_AppKids.Application.Services;

    public class ExampleController : ControllerBase
    {

        private readonly DictionaryService aDictionaryService = new DictionaryService();
        private readonly WordService aWordService = new WordService();
        private readonly PhraseService aPhraseService = new PhraseService();

        [DnnAuthorize]
        [AcceptVerbs("POST")]
        public HttpResponseMessage AddPhrase(string listOfWords , int dictionaryId)
        {
            if (listOfWords.Length == 0)
            {
                return this.ControllerContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    "Invalid parameters, Please check there is elements in array");
            }

            var wordList = Json.Deserialize<List<WordDto>>(listOfWords);
            wordList.Select(c => { c.CreationTime = DateTime.Now; return c; }).ToList();

            try
            {
                var listNoRepeatedElements = wordList.Distinct(new DistinctItemComparer()).ToList();
                this.aWordService.BulkInsertOfWords(listNoRepeatedElements);

                if (wordList.Count == 1)
                {
                    return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
                }
                var delimiter = " ";
         
               var sentence = wordList.Select(i => i.WordName).Aggregate((i, j) => i + delimiter + j);
                var listOfWordsId = this.aWordService.GetIdOfWords(wordList);
                var aPhrase = new PhraseDto
                                  {
                                      PhraseText = sentence,
                                      CreationTime = DateTime.Now,
                                      WordsIds = string.Join(",", listOfWordsId),
                                      AssignedDictionaryId = dictionaryId,
                                      UserName = UserController.GetCurrentUserInfo().Username
                                  };

                this.aPhraseService.InsertPhrase(aPhrase);
                return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                var listRepeatedWords = this.aWordService.GetRepeatedWords(wordList);
                var errorMessage = string.Format("Cannot insert duplicate words: {0}", string.Join(",", listRepeatedWords));
                return this.ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage checkUserAuthenticated()
        {
            var authenticated = HttpContext.Current.User.Identity.IsAuthenticated;
            if (authenticated)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, UserController.GetCurrentUserInfo().Username);
            }

            return ControllerContext.Request.CreateResponse(
          HttpStatusCode.Unauthorized, "Not authorized");

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
        public HttpResponseMessage GetWordsList(int dictionaryId, int indexOfPhraseList)
        {
            var errorMessage = string.Empty;
            try
            {
                var listOfPhrase = this.aPhraseService.GetListOfPhrase(dictionaryId);
                var wordsId = listOfPhrase[indexOfPhraseList].WordsIds;
                var listOfWords = this.aWordService.GetListOfWordsForAPhrase(wordsId);
                if (listOfWords.Any())
                {
                    return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK, listOfWords);
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

        public HttpResponseMessage GetDictionary(int dictionaryId)
        {
            var actualDictionary = aDictionaryService.GetADictionary(dictionaryId);

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