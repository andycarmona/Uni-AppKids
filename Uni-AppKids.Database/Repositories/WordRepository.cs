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

        public override List<Word> GetListOfOrderedWordsForAPhrase(string wordsId)
        {
            var numbersId = wordsId.Split(',').Select(int.Parse).ToList();
            var rawListWord = this.Get().AsEnumerable();
            var listOfOrderedWords = from np in rawListWord
                            let index = numbersId.IndexOf(np.WordId) 
                            where index >= 0
                            orderby index ascending
                            select np;
     
            return listOfOrderedWords.ToList();
        }
    }
}
