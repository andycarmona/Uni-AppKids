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
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
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

        private readonly GenericRepository<Word> aGenericRepository;

        public WordRepository(UniAppKidsDbContext uniAppKidsDbContext)
            : base(uniAppKidsDbContext)
        {
            context = uniAppKidsDbContext;
            aGenericRepository = new GenericRepository<Word>(context);
        }


        public void BulkInsertOfWords(List<Word> listOfWords)
        {
            List<Word> wordsToupdate;
            var nonRepeatedListOfWords = this.GetListOfNotExistingWords(listOfWords, out wordsToupdate);
            this.UpdatePropertiesOfRepeatedWords(wordsToupdate);
            if (!nonRepeatedListOfWords.Any())
            {
                return;
            }

            using (var transactionScope = new TransactionScope())
            {

                try
                {
                    this.context.BulkInsert(nonRepeatedListOfWords);
                    this.context.SaveChanges();
                }
                catch (DuplicateKeyException e)
                {
                    throw new DuplicateKeyException(e.Object);
                }

                transactionScope.Complete();
            }
        }

        public void UpdatePropertiesOfRepeatedWords(List<Word> wordListToUpdate)
        {
            foreach (var wordToUpdate in wordListToUpdate)
            {
                var aWord = this.context.Words.FirstOrDefault(rawWord => rawWord.WordName == wordToUpdate.WordName);
                if (aWord == null)
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(wordToUpdate.Image))
                {
                    aWord.Image = wordToUpdate.Image;
                }

                if (!string.IsNullOrEmpty(wordToUpdate.Image))
                {
                    aWord.WordDescription = wordToUpdate.WordDescription;
                }

                if (!string.IsNullOrEmpty(wordToUpdate.Image))
                {
                    aWord.SoundFile = wordToUpdate.SoundFile;
                }

                this.aGenericRepository.Update(aWord);
                this.context.SaveChanges();
            }
        }


        public List<Word> GetListOfNotExistingWords(List<Word> rawWordList, out List<Word> wordsToupdate)
        {

            wordsToupdate = new List<Word>();
            var notExistingWords = new List<Word>();
            foreach (var aWord in rawWordList)
            {
                Word foundWord = null;
                foundWord = this.context.Words.FirstOrDefault(rawWord => rawWord.WordName == aWord.WordName);
                if (foundWord == null)
                {
                    notExistingWords.Add(aWord);
                }
                else
                {
                    var comparationResult = aGenericRepository.Compare(foundWord, aWord);
                    if (!comparationResult)
                    {
                        wordsToupdate.Add(aWord);
                    }
                }
            }

            return notExistingWords;
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
