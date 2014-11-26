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
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Globalization;
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

        public List<string> GetIdOfWordsInPhrase(List<Word> listOfWords)
        {
           var wordIds = new List<string>();
  
            foreach (var word in listOfWords)
            {
                var aWord = context.Words.First(i => i.WordName == word.WordName);
                wordIds.Add(aWord.WordId.ToString(CultureInfo.InvariantCulture));    
            }

            return wordIds;
        }

        public List<string> GetRepeatedWords(List<Word> listOfWords)
        {
            var repeatedWords = new List<string>();

            foreach (var word in listOfWords)
            {
                var aWord = context.Words.FirstOrDefault(i => i.WordName == word.WordName);
                if (aWord != null)
                {
                    repeatedWords.Add(aWord.WordName);
                }
            }

            return repeatedWords;
        }

        public IEnumerable<Word> GetAllWords()
        {
            return this.GetAllData();
        }

        public List<Word> GetListOfOrderedWordsForAPhrase(string wordsId)
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
