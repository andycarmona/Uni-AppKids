// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Word.cs" company="Uni-App">
//   -
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Uni_AppKids.Core.EntityModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Word
    { 
        public Word()
        {
            this.CreationTime = DateTime.Now;
        }


        [Key]
        public int WordId { get; set; }

        public string WordName { get; set; }

        public string Image { get; set; }

        public string SoundFile { get; set; }

        public DateTime CreationTime { get; set; }

        public string UserName { get; set; }

        public string WordDescription { get; set; }

        [ForeignKey("AssignedPhraseId")]
        public virtual Phrase AssignedToPhrase { get; set; }

        public int AssignedPhraseId { get; set; }
    }
}