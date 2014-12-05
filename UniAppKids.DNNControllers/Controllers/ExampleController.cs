namespace UniAppKids.DNNControllers.Services
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Linq;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;

    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Entities.Users;
    using DotNetNuke.Web.Api;

    using UniAppKids.DNNControllers.Helpers;

    using Uni_AppKids.Application.Dto;
    using Uni_AppKids.Application.Services;

    public class ExampleController : ControllerBase
    {

        private readonly DictionaryService aDictionaryService = new DictionaryService();
        private readonly WordService aWordService = new WordService();
        private readonly PhraseService aPhraseService = new PhraseService();

        [DnnAuthorize]
        [AcceptVerbs("POST")]
        public async Task<List<string>> PostAsync()
        {
            if (Request.Content.IsMimeMultipartContent())
            {
                var uploadPath = HttpContext.Current.Server.MapPath("~/Uploads");

                var streamProvider = new MyStreamProvider(uploadPath);

                await Request.Content.ReadAsMultipartAsync(streamProvider);

                var messages = new List<string>();
                foreach (var file in streamProvider.FileData)
                {
                    FileInfo fi = new FileInfo(file.LocalFileName);
                    messages.Add("File uploaded as " + fi.FullName + " (" + fi.Length + " bytes)");
                }

                return messages;
            }

            HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Request!");
            throw new HttpResponseException(response);
        }


        [DnnAuthorize]
        [AcceptVerbs("POST")]
        public HttpResponseMessage AddPhrase(string listOfWords, int dictionaryId)
        {
            var errorMessage = new StringBuilder(string.Empty);
            var listNoRepeatedElements = new List<WordDto>();
            var listOfNotAcceptedWords = new List<string>();

            if (listOfWords.Length == 0)
            {
                return this.ControllerContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    "Invalid parameters, Please check there is elements in array");
            }

            var language = aDictionaryService.GetADictionary(dictionaryId).DictionaryName;
            
            var delimiter = " ";
            var wordList = Json.Deserialize<List<WordDto>>(listOfWords);
            wordList.Select(c => { c.CreationTime = DateTime.Now; return c; }).ToList();


            try
            {
                listNoRepeatedElements = WordFilterTool.ListNoRepeatedElements(wordList);
                var pathToDictionary = HttpContext.Current.Server.MapPath(string.Format("{0}{1}.txt", ConfigurationManager.AppSettings["Dictionary"], language));
                WordFilterTool.GetWordsNotAccepted(listNoRepeatedElements, language , pathToDictionary, out listOfNotAcceptedWords);
                this.aWordService.BulkInsertOfWords(listNoRepeatedElements);

                if (wordList.Count == 1)
                {
                    return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
                }

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
            catch (DuplicateKeyException e)
            {
                var listRepeatedWords = this.aWordService.GetRepeatedWords(wordList);
                errorMessage.Append(string.Format(
                    "Cannot insert duplicate words: {0}",
                    string.Join(",", listRepeatedWords)));

            }
            catch (FormatException e)
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