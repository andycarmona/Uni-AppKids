// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WordRepository.cs" company="uni-app">
//   -
// </copyright>
// <summary>
//   -
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uni_AppKids.Database.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Uni_AppKids.Core.EntityModels;
    using Uni_AppKids.Core.Interface;
    using Uni_AppKids.Database.EntityFramework;

    public class WordRepository : GenericRepository<Word>, IWordsRepository
    {
        public WordRepository(UniAppKidsDbContext context)
            : base(context)
        {  
        }

        public override List<Word> GetListOfWordsForAPhrase(string wordsId)
        {
            var numbersId = wordsId.Split(',').Select(int.Parse).ToList();
            var listOfOrderedWords = this.Get().Where(x => numbersId.Contains(x.WordId));
            return listOfOrderedWords.ToList();
        }
    }
}
