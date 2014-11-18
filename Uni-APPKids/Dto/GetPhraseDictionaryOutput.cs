// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetPhraseDictionaryOutput.cs" company="Uni-app">
//   -
// </copyright>
// <summary>
//   -
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uni_APPKids.Dto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class GetPhraseDictionaryOutput 
    {
        [Required]
        public string DictionaryName { get; set; }

       public List<PhraseDto> Phrases { get; set; }  
    }
}
