// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WordHandlerController.cs" company="Uni-app">
//   Uni-App E-learning company
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UniAppKids.ExternServiceController.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Linq;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using Newtonsoft.Json.Linq;

    using UniAppKids.DNNControllers.Helpers;
    using UniAppKids.ExternServiceController.Helpers;

    using Uni_AppKids.Application.Dto;
    using Uni_AppKids.Application.Services;

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/WordHandler")]
    public class WordHandlerController : ApiController
    {
        private readonly DictionaryService aDictionaryService = new DictionaryService();
        private readonly WordService aWordService = new WordService();
        private readonly PhraseService aGenericPhraseService = new PhraseService();

        [AcceptVerbs("POST")]
        [Route("AddPhrase")]
        public async Task<HttpResponseMessage> AddPhrase(string listOfWords, int dictionaryId)
        {
            var errorMessage = new StringBuilder(string.Empty);
            var listOfNotAcceptedWords = new List<string>();
            const string Delimiter = " ";

            if (listOfWords.Length == 0)
            {
                return this.ControllerContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    "Invalid parameters, Please check there is elements in array");
            }

            var language = this.aDictionaryService.GetADictionary(dictionaryId).DictionaryName;
            var wordList = (JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(listOfWords, typeof(List<WordDto>));
            var deserializeWordList = wordList.ToObject<List<WordDto>>();
            var verifiedWordList = WordFilterTool.GetListWithValidWordName(deserializeWordList);

            try
            {
                var listNoRepeatedElements = WordFilterTool.ListNoRepeatedElements(verifiedWordList);
                var pathToDictionary = HttpContext.Current.Server.MapPath(string.Format("{0}{1}.txt", ConfigurationManager.AppSettings["Dictionary"], language));
                WordFilterTool.GetWordsNotAccepted(
                    listNoRepeatedElements,
                    language,
                    pathToDictionary,
                    out listOfNotAcceptedWords);

                WordFilterTool.AddExtraInformationWordList(listNoRepeatedElements);

                await this.aWordService.BulkInsertOfWords(listNoRepeatedElements);

                if (verifiedWordList.Count == 1)
                {
                    return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
                }

                var aPhrase = this.PreparePhraseToAdd(dictionaryId, verifiedWordList, Delimiter);
                this.aGenericPhraseService.InsertPhrase(aPhrase);
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
                foreach (var aWord in deserializeWordList)
                {
                    foreach (var notAcceptedWord in listOfNotAcceptedWords)
                    {
                        if (aWord.WordName == notAcceptedWord)
                        {
                            aWord.Repeated = true;
                        }
                    }
                }

                return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK, wordList);
            }

            return this.ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, errorMessage.ToString());
        }

        [AcceptVerbs("GET")]
        [Route("GetAllWordsInDictionary")]
        public HttpResponseMessage GetAllWordsInDictionary()
        {
            var wordList = this.aWordService.GetAllWords();

            if (!wordList.Any())
            {
                return this.ControllerContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    "There is no words in dictionary");
            }

            foreach (var aWord in wordList)
            {
                aWord.Image = WordFilterTool.CheckUrlIsAValidImageInWord(aWord);
            }

            return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK, wordList);
        }

        [AcceptVerbs("GET")]
        [Route("DeletePhrase")]
        public HttpResponseMessage DeletePhrase(int phraseId)
        {
            try
            {
                this.aGenericPhraseService.DeletePhrase(phraseId);

                return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return this.ControllerContext.Request.CreateResponse(
             HttpStatusCode.BadRequest,
             "Couldn't delete phrase");
            }
        }

        [AcceptVerbs("GET")]
        [Route("GetAllPhrasesInDictionary")]
        public HttpResponseMessage GetAllPhrasesInDictionary(int dictionaryId, int totalPages)
        {
            var aPhraseService = new PhraseService();
            aPhraseService.SetSqlPhraseRepository();
            var phraseList = aPhraseService.GetListOfPhrase(dictionaryId, totalPages);

            if (!phraseList.Any())
            {
                return this.ControllerContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    "There is no phrases in dictionary");
            }

            return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK, phraseList);
        }

        [AcceptVerbs("GET")]
        [Route("GetWordsList")]
        public HttpResponseMessage GetWordsList(int dictionaryId, int indexOfPhraseList, int totalPages)
        {
            var aPhraseService = new PhraseService();
            string errorMessage;
            try
            {
                aPhraseService.SetSqlPhraseRepository();
                var listOfPhrase = aPhraseService.GetListOfPhrase(dictionaryId, totalPages);
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

                throw new Exception("No words in dictionary");
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }

            return this.ControllerContext.Request.CreateResponse(
                      HttpStatusCode.BadRequest,
                      errorMessage);
        }

        [AcceptVerbs("GET")]
        [Route("GetDictionary")]
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

        private PhraseDto PreparePhraseToAdd(int dictionaryId, List<WordDto> verifiedWordList, string delimiter)
        {
            var sentence = verifiedWordList.Select(i => i.WordName).Aggregate((i, j) => i + delimiter + j);
            var listOfWordsId = this.aWordService.GetIdOfWords(verifiedWordList);

            var aPhrase = new PhraseDto
            {
                PhraseText = sentence,
                CreationTime = DateTime.Now,
                WordsIds = string.Join(",", listOfWordsId),
                AssignedDictionaryId = dictionaryId,
                UserName = "Anonymous"
            };
            return aPhrase;
        }

    }
}