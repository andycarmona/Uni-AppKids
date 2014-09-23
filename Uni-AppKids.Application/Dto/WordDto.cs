namespace Uni_AppKids.Application.Dto
{
    using System;

    public class WordDto 
    {
        public string WordName { get; set; }

        public string Image { get; set; }

        public string SoundFile { get; set; }

        public DateTime CreationTime { get; set; }

       
        public PhraseDto AssignedToPhrase { get; set; }

        public int AssignedPhraseId { get; set; }
    }
}
