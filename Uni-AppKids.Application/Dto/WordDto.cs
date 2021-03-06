﻿namespace Uni_AppKids.Application.Dto
{
    using System;

    public class WordDto 
    {
        public int WordId { get; set; }

        public string WordName { get; set; }

        public string Image { get; set; }

        public string SoundFile { get; set; }

        public string WordDescription { get; set; }

        public DateTime CreationTime { get; set; }

        public bool Repeated { get; set; }
    }
}
