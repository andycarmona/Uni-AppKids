// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Phrase.cs" company="uni-app">
//   -
// </copyright>
// <summary>
//   -
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uni_AppKids.Core.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Phrase
    {
     

        public Phrase()
        {
            this.CreationTime = DateTime.Now;
        }

        [Key]
        public int PhraseId { get; set; }

        [ForeignKey("AssignedDictionaryId")]
        public virtual PhraseDictionary AssignedToDictionary { get; set; }

        public int AssignedDictionaryId { get; set; }

        public string PhraseText { get; set; }

        public string UserName { get; set; }

        public DateTime CreationTime { get; set; }

        public string WordsIds { get; set; }
    }
}
