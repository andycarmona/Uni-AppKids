namespace UniAppKids.DNNControllers.Controllers
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

    using UniAppKids.DNNControllers.Models;
    using UniAppKids.DNNControllers.Repository;
    using UniAppKids.DNNControllers.Services;

    using UniAppSpel.Helpers;

    using Uni_AppKids.Application.Dto;
    using Uni_AppKids.Application.Services;

    public class ExampleController : ControllerBase
    {
        private readonly DictionaryRepository aRepository = new DictionaryRepository();
        private readonly DictionaryService externalDictionaryService = new DictionaryService();
        private readonly WordService aWordService = new WordService();
        private readonly PhraseService aPhraseService = new PhraseService();

        [DnnAuthorize]
        [AcceptVerbs("POST")]
        public HttpResponseMessage AddPhrase(string listOfWords)
        {
            if (listOfWords.Length == 0)
            {
                return this.ControllerContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    "Invalid parameters, Please check there is elements in array");
            }

            var wordList = Json.Deserialize<List<WordDto>>(listOfWords);
            wordList.Select(c => { c.CreationTime = DateTime.Now; return c; }).Distinct(new DistinctItemComparer()).ToList();

            try
            {
                this.aWordService.BulkInsertOfWords(wordList);
                var listOfWordsId = this.aWordService.GetIdOfWords(wordList);
                return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                var listRepeatedWords = this.aWordService.GetRepeatedWords(wordList);
                var errorMessage = string.Format("Cannot insert duplicate words: {0}", string.Join(",", listRepeatedWords));
                return this.ControllerContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage checkUserAuthenticated()
        {
            var authenticated = HttpContext.Current.User.Identity.IsAuthenticated;
            if (authenticated)
            {
                return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK, UserController.GetCurrentUserInfo().Username);
            }

            return this.ControllerContext.Request.CreateResponse(
          HttpStatusCode.Unauthorized, "Not authorized");

        }

        [DnnAuthorize]
        [AcceptVerbs("GET")]
        public HttpResponseMessage GetAllWordsInDictionary()
        {
            List<WordDto> wordList = this.aWordService.GetAllWords();
        
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
        public List<WordDto> GetWordsList(int dictionaryId, int indexOfPhraseList)
        {
            string errorMessage;
            try
            {
                var listOfPhrase = this.aPhraseService.GetListOfPhrase(dictionaryId);
                var wordsId = listOfPhrase[indexOfPhraseList].WordsIds;
                var listOfWords = this.aWordService.GetListOfWordsForAPhrase(wordsId);
                return listOfWords;
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }

            var listDefault = new List<WordDto>();
            var aWord = new WordDto { WordName = errorMessage };
            listDefault.Add(aWord);
            return listDefault;

        }

        [DnnAuthorize]
        [AcceptVerbs("GET", "POST")]
        public List<PhraseDictionary> GetDictionary(string userName)
        {
            var dictionaries = this.aRepository.GetDictionaries();

            return dictionaries;
        }
        [DnnAuthorize()]
        [AcceptVerbs("GET", "POST")]
        public List<PhraseDictionaryDto> GetDictionaryFromEF(string userName)
        {
            var dictionaries = this.externalDictionaryService.GetUserPhraseDictionaries(userName);

            return dictionaries;
        }
        #region "Web Methods"
        [DnnAuthorize()]
        [HttpGet()]
        public HttpResponseMessage HelloWorld()
        {
            try
            {
                string helloWorld = "Hello World!";
                return this.Request.CreateResponse(HttpStatusCode.OK, helloWorld);
            }
            catch (Exception ex)
            {
                //Log to DotNetNuke and reply with Error
                Exceptions.LogException(ex);
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [DnnAuthorize()]
        [AcceptVerbs("GET", "POST")]
        public HttpResponseMessage GoodbyeWorld(bool data)
        {
            try
            {
                string helloWorld = "Hello World!";
                if (data)
                {
                    helloWorld = "Good-bye World!";
                }
                return this.Request.CreateResponse(HttpStatusCode.OK, helloWorld);
            }
            catch (Exception ex)
            {
                //Log to DotNetNuke and reply with Error
                Exceptions.LogException(ex);
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region "DTO Classes"
        public class DTOWorldDetails
        {
            public bool goodbye { get; set; }
        }
        #endregion
    }
}