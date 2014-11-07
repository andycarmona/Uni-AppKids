// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PhraseDto.cs" company="Uni-app">
//   -
// </copyright>
// <summary>
//   -
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uni_APPKids.Dto
{
    using System;

    public class PhraseDto
    {
        public int PhraseId { get; set; }

        public string PhraseText { get; set; }

        public DateTime CreationTime { get; set; }

        public string WordsIds { get; set; }
    }
}
