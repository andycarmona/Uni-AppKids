namespace Uni_AppKids.Application.Dto
{
    using System;
    using System.Collections.Generic;

    public class PhraseDto
    {
        public string PhraseText { get; set; }

        public DateTime CreationTime { get; set; }

        public List<WordDto> Words { get; set; }
    }
}
