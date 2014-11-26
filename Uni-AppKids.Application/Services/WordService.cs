

namespace Uni_AppKids.Application.Services
{
    using System.Collections.Generic;
    using AutoMapper;
    using Uni_AppKids.Application.Dto;
    using Uni_AppKids.Core.EntityModels;

    public class WordService
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        public void BulkInsertOfWords(List<WordDto> listOfWords)
        {
            GetMappedEntities();
            var mappedListOfWords = Mapper.Map<List<WordDto>, List<Word>>(listOfWords);
            unitOfWork.GetCustomWordRepository().BulkInsertOfWords(mappedListOfWords);
        }

        public List<string> GetIdOfWords(List<WordDto> listOfWords)
        {
            GetMappedEntities();
            var mappedListOfWords = Mapper.Map<List<WordDto>, List<Word>>(listOfWords);
            var listOfId = unitOfWork.GetCustomWordRepository().GetIdOfWordsInPhrase(mappedListOfWords);
            return listOfId;
        }

        public List<string> GetRepeatedWords(List<WordDto> listOfWords)
        {
            GetMappedEntities();
            var mappedListOfWords = Mapper.Map<List<WordDto>, List<Word>>(listOfWords);
            var listOfRepeatedWords = unitOfWork.GetCustomWordRepository().GetRepeatedWords(mappedListOfWords);
            return listOfRepeatedWords;
        }

        public List<WordDto> GetListOfWordsForAPhrase(string wordsId)
        {
            GetMappedEntities();
            var listOfWords = unitOfWork.GetCustomWordRepository().GetListOfOrderedWordsForAPhrase(wordsId);
            var mappedListOfWords = Mapper.Map<List<Word>, List<WordDto>>(listOfWords);
            return mappedListOfWords;
        }

        public List<WordDto> GetAllWords()
        {
            GetMappedEntities();
            var listOfWords = unitOfWork.GetCustomWordRepository().GetAllWords();
            var mappedListOfWords = Mapper.Map<List<Word>, List<WordDto>>((List<Word>)listOfWords);
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
            Mapper.CreateMap<WordDto, Word>();
        }
    }
}
