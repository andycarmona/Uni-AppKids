// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PhraseDictionary.cs" company="uni-app">
//   -
// </copyright>
// <summary>
//   -
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uni_AppKids.Core.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PhraseDictionary 
    {
        [Key]
        public int PhraseDictionaryId { get; set; }

        public string DictionaryName { get; set; }
        
        public virtual List<Phrase> Phrases { get; set; }  
    }
}
