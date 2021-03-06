﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PhraseDictionaryRepository.cs" company="uni-app">
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

    public class PhraseDictionaryRepository : GenericRepository<PhraseDictionary>, IPhraseDictionaryRepository
    {
        public PhraseDictionaryRepository(UniAppKidsDbContext context)
            : base(context)
        {
        }

        public PhraseDictionaryRepository()
        {
        }


        public IEnumerable<PhraseDictionary> GetUserDictionaries()
        {
            IEnumerable<PhraseDictionary> listOfDictionaries = this.Get();
            
            return listOfDictionaries;
        }
    }
}
