namespace UniAppKids.DNNControllers.Repository
{
    using System.Collections.Generic;
    using System.Linq;

    using DotNetNuke.Data;

    using UniAppKids.DNNControllers.Models;

    public class DictionaryRepository
    {
        public List<PhraseDictionary> GetDictionaries()
        {
            var dictionaries = new List<PhraseDictionary>();

            using (var db = DataContext.Instance())
            {
                var rep = db.GetRepository<PhraseDictionary>();
                dictionaries = rep.Get().Where(x => x.UserName == "andy").ToList();
            }

            return dictionaries;
        }
    }
}