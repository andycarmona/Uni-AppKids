

namespace Uni_APPKids.Services
{
    using System.Collections.Generic;

    using AutoMapper;

    using Uni_AppKids.Core.EntityModels;

    using Uni_APPKids.Dto;

    public class PhraseService
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        public List<PhraseDto> GetListOfPhrase(int dictionaryId)
        {
            GetMappedEntities();
            List<Phrase> listOfPhrases = this.unitOfWork.GetPhraseRepository().GetPhrasesInDictionary(dictionaryId);
            var mappedListOfWords = Mapper.Map<List<Phrase>, List<PhraseDto>>(listOfPhrases);
            return mappedListOfWords;
     
        }

        private static void GetMappedEntities()
        {
            Mapper.CreateMap<CreatePhraseDictionaryInput, PhraseDictionary>()
         .ForMember(c => c.Phrases, option => option.MapFrom(src => src.Phrases));
            Mapper.CreateMap<PhraseDictionary, PhraseDictionaryDto>()
                .ForMember(c => c.Phrases, option => option.MapFrom(src => src.Phrases));
            Mapper.CreateMap<Phrase, PhraseDto>();
            Mapper.CreateMap<Word, WordDto>();
          
        }
    }
}
