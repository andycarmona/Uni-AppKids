// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PhraseRepository.cs" company="uni-app">
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

    public class PhraseRepository : IPhraseRepository
    {
        private readonly UniAppKidsDbContext context;

        public PhraseRepository(UniAppKidsDbContext uniAppKidsDbContext)
        {
            context = uniAppKidsDbContext;
        }

        public List<Phrase> GetPhrasesInDictionary(int dictionaryId,int totalPages)
        {
            var listOfPhrases = context.Phrases.Where(x => x.AssignedDictionaryId == dictionaryId).Take(totalPages).ToList();

            return listOfPhrases;
        }
    }
}
