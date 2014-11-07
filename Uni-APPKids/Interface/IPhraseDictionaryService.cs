// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPhraseDictionaryService.cs" company="ui-app">
//   -
// </copyright>
// <summary>
//   -
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uni_APPKids.Interface
{
    using System.Collections.Generic;

    using Uni_APPKids.Dto;

    public interface IPhraseDictionaryService 
    {

        PhraseDictionaryDto GetADictionary(int id);
       
        List<PhraseDictionaryDto> GetPhraseDictionaries();

        void UpdatePhraseDictionary(UpdatePhraseDictionaryInput input);

        void CreatePhraseDictionary(CreatePhraseDictionaryInput input);
    }
}
