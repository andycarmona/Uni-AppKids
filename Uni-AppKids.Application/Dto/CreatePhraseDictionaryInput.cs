// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreatePhraseDictionaryInput.cs" company="uni-app">
//   -
// </copyright>
// <summary>
//   -
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uni_AppKids.Application.Dto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreatePhraseDictionaryInput 
    {
        [Required]
        public string DictionaryName { get; set; }

        public int Id { get; set; }

        public List<PhraseDto> Phrases { get; set; }  
    }
}
