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

    using Uni_AppKids.Core.EntityModels;
    using Uni_AppKids.Database.Repositories;

    using Uni_AppKids.Database.EntityFramework;

    public class UnitOfWork : IDisposable
    {
        private readonly UniAppKidsDbContext context = new UniAppKidsDbContext();

        private GenericRepository<PhraseDictionary> phraseDictionaryRepository;

        private PhraseDictionaryRepository speciPhraseDictionaryRepo;

        private bool disposed = false;
 

        public GenericRepository<PhraseDictionary> PhraseDictionaryRepository
        {
            get
            {
                return this.phraseDictionaryRepository
                       ?? (this.phraseDictionaryRepository = new GenericRepository<PhraseDictionary>(this.context));
            }
        }

        public GenericRepository<PhraseDictionary> SpecPhraseDictionaryRepository
        {
            get
            {
                return this.phraseDictionaryRepository
                       ?? (this.phraseDictionaryRepository = new PhraseDictionaryRepository(this.context));
            }
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

       public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
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
