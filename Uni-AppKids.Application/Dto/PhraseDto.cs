// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PhraseDto.cs" company="Uni-app">
//   -
// </copyright>
// <summary>
//   -
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uni_AppKids.Application.Dto
{
    using System;
   

    public class PhraseDto
    {
        public int PhraseId { get; set; }

        public int AssignedDictionaryId { get; set; }

        public string PhraseText { get; set; }

        public string UserName { get; set; }

        public DateTime CreationTime { get; set; }

        public string WordsIds { get; set; }
    }
}
