// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WordsController.cs" company="Uni-app">
//   -
// </copyright>
// <summary>
//   -
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uni_APPKids.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;
    using System.Web.Routing;

    using Uni_AppKids.Application.Dto;
    using Uni_AppKids.Application.Services;

    public class WordsController : ApiController
    {
        private readonly WordService aWordService = new WordService();
        private readonly PhraseService aPhraseService = new PhraseService();

        [HttpGet]
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

        // GET api/words/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/words
        public void Post([FromBody]string value)
        {
        }

        // PUT api/words/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/words/5
        public void Delete(int id)
        {
        }
    }
}
