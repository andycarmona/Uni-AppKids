

namespace Uni_AppKids.Application.Services
{
    using System.Collections.Generic;
    using AutoMapper;
    using Uni_AppKids.Application.Dto;
    using Uni_AppKids.Core.EntityModels;

    public class PhraseService
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        public PhraseService()
        {
            GetMappedEntities();
        }

        public List<PhraseDto> GetListOfPhrase(int dictionaryId, int totalPages)
        {
            var listOfPhrases = unitOfWork.GetPhraseRepository().GetPhrasesInDictionary(dictionaryId,totalPages);
            var mappedListOfWords = Mapper.Map<List<Phrase>, List<PhraseDto>>(listOfPhrases);
            return mappedListOfWords;
        }

        public void DeletePhrase(int phraseId)
        {
            unitOfWork.GetGenericPhraseRepository().Delete(phraseId);
            unitOfWork.Save();
        }

        public void InsertPhrase(PhraseDto phrase)
        {
            var mappedPhrase = Mapper.Map<PhraseDto, Phrase>(phrase);
            unitOfWork.GetGenericPhraseRepository().Insert(mappedPhrase);
            unitOfWork.Save();
        }

        private static void GetMappedEntities()
        {
            Mapper.CreateMap<PhraseDto, Phrase>();
            Mapper.CreateMap<Phrase, PhraseDto>();
            Mapper.CreateMap<Word, WordDto>();
        }
    }
}
