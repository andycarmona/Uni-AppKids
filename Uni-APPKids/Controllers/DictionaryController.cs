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

    using Uni_APPKids.Dto;
    using Uni_APPKids.Services;

    public class DictionaryController : ApiController
    {
        private readonly DictionaryService dictionaryService = new DictionaryService();


        public List<PhraseDictionaryDto> Get(string userName)
        {
            var dictionaries = dictionaryService.GetUserPhraseDictionaries(userName);
            
            return dictionaries;
        }

        // GET api/dictionary/5
        public string Get(int userId)
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
