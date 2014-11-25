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
    using System.Data.Linq;
    using System.Linq;
    using System.Transactions;

    using global::EntityFramework.BulkInsert.Extensions;

    using Uni_AppKids.Core.EntityModels;
    using Uni_AppKids.Core.Interface;
    using Uni_AppKids.Database.EntityFramework;

    public class WordRepository : GenericRepository<Word>, IWordsRepository
    {
        private readonly UniAppKidsDbContext context;

        public WordRepository(UniAppKidsDbContext uniAppKidsDbContext)
            : base(uniAppKidsDbContext)
        {
            context = uniAppKidsDbContext;
        }


        public void BulkInsertOfWords(List<Word> listOfWords)
        {
            using (var transactionScope = new TransactionScope())
            {
                try
                {
                context.BulkInsert(listOfWords);
                context.SaveChanges();
                }
                catch (DuplicateKeyException e)
                {
                }

                transactionScope.Complete();
            }
        }

        public string[] GetIdOfWordsInPhrase(List<Word> listOfWords)
        {
            List<string> listOfIds = new List<string>();

            return null;
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
