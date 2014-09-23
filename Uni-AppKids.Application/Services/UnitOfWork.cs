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
 

        public GenericRepository<PhraseDictionary> PhraseDictionaryRepository
        {
            get
            {
                if (this.phraseDictionaryRepository == null)
                {
                    this.phraseDictionaryRepository = new GenericRepository<PhraseDictionary>(this.context);
                }
                return this.phraseDictionaryRepository;
            }
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

        private bool disposed = false;

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

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
