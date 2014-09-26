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
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Routing;

    using Uni_AppKids.Application.Dto;
    using Uni_AppKids.Application.Services;

    public class WordsController : ApiController
    {
        private readonly WordService aWordService = new WordService();

    
        
        [HttpGet]
        public List<WordDto> GetWordsList(string wordsId)
        {
            var listOfWords = this.aWordService.GetListOfWordsForAPhrase(wordsId);
            return listOfWords;
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
