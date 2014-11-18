// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfWork.cs" company="uni-app">
//   -
// </copyright>
// <summary>
//   -
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uni_AppKids.Application.Services
{
    using System;

    using Uni_AppKids.Application.Interface;
    using Uni_AppKids.Core.EntityModels;
    using Uni_AppKids.Database.Repositories;

    using Uni_AppKids.Database.EntityFramework;

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly UniAppKidsDbContext context = new UniAppKidsDbContext();

        private GenericRepository<PhraseDictionary> genericPhraseDictionaryRepository;

        private GenericRepository<Word> genericWordRepository;

        private PhraseDictionaryRepository customPhraseDictionaryRepository;

        private WordRepository customWordRepository;

        private PhraseRepository phraseRepository;


        private bool disposed = false;

        public void Save()
        {
            this.context.SaveChanges();
        }

       public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public PhraseDictionaryRepository GetCustomPhraseDictionaryRepository()
        {
            return this.customPhraseDictionaryRepository ?? 
                (this.customPhraseDictionaryRepository = new PhraseDictionaryRepository(this.context));
        }

        public WordRepository GetCustomWordRepository()
        {
            return this.customWordRepository ?? (this.customWordRepository = new WordRepository(this.context));
        }

        public PhraseRepository GetPhraseRepository()
        {
            return this.phraseRepository ?? (this.phraseRepository = new PhraseRepository(this.context));
        }


        public GenericRepository<PhraseDictionary> GetGenericPhraseDictionaryRepository()
        {
           
                return this.genericPhraseDictionaryRepository
                       ?? (this.genericPhraseDictionaryRepository = new GenericRepository<PhraseDictionary>(this.context));
        }

        public GenericRepository<Word> GetGenericWordRepository()
        {
            return this.genericWordRepository
                  ?? (this.genericWordRepository = new GenericRepository<Word>(this.context));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}
