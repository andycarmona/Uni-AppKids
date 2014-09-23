// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetPhraseDictionaryOutput.cs" company="Uni-app">
//   -
// </copyright>
// <summary>
//   -
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uni_AppKids.Application.Dto
{
    using System.Collections.Generic;

    using Microsoft.Build.Framework;

    public class GetPhraseDictionaryOutput 
    {
        [Required]
        public string DictionaryName { get; set; }

       public List<PhraseDto> Phrases { get; set; }  
    }
}
