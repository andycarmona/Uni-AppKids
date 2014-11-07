// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PhraseDictionaryDto.cs" company="uni-app">
//   -
// </copyright>
// <summary>
//   -
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uni_APPKids.Dto
{
    using System.Collections.Generic;

    public class PhraseDictionaryDto 
    {
        public string DictionaryName { get; set; }

        public List<PhraseDto> Phrases { get; set; }
    }
}
