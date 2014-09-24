// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPhraseDictionaryRepository.cs" company="uni-app">
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

    public interface IPhraseDictionaryRepository 
    {
        IEnumerable<PhraseDictionary> GetDictionaries();
    }
}
