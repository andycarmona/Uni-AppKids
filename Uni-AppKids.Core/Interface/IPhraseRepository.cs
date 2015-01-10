// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPhraseRepository.cs" company="uni-app">
//   -
// </copyright>
// <summary>
//   -
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uni_AppKids.Core.Interface
{
    using System.Collections.Generic;

    using Uni_AppKids.Core.EntityModels;

    public interface IPhraseRepository
    {
        List<Phrase> GetPhrasesInDictionary(int dictionaryId,int totalPages);
    }
}
