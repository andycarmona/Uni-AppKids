// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DictionaryController.cs" company="Uni-app">
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

    using Uni_AppKids.Application.Services;

    public class DictionaryController : ApiController
    {
        private readonly DictionaryService dictionaryService = new DictionaryService();

        // GET api/dictionary
        public IEnumerable<string> Get()
        {
            var dictionaries = dictionaryService.GetPhraseDictionaries();
            var name = dictionaries[0].DictionaryName;
            return new string[] { "value1", name };
        }

        // GET api/dictionary/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/dictionary
        public void Post([FromBody]string value)
        {
        }

        // PUT api/dictionary/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/dictionary/5
        public void Delete(int id)
        {
        }
    }
}
