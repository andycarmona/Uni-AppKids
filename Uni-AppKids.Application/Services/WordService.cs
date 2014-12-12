namespace Uni_AppKids.Application.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;
    using Uni_AppKids.Application.Dto;
    using Uni_AppKids.Core.EntityModels;

    public class WordService
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        public WordService()
        {
            GetMappedEntities(); 
        }
     
        public void InsertWord(WordDto aWord)
        {
            var mappedWord = Mapper.Map<WordDto, Word>(aWord);
            unitOfWork.GetGenericWordRepository().Insert(mappedWord);
            unitOfWork.Save();
        }

        public async Task<List<string>> InsertWordProcess(List<WordDto> listOfWords)
        {
            await this.BulkInsertOfWords(listOfWords);
            return await this.GetIdOfWords(listOfWords);
        }
    

        public Task BulkInsertOfWords(List<WordDto> listOfWords)
        {
            var mappedListOfWords = Mapper.Map<List<WordDto>, List<Word>>(listOfWords);
           return Task.Run(() => unitOfWork.GetCustomWordRepository().BulkInsertOfWords(mappedListOfWords));
        }

        public Task<List<string>> GetIdOfWords(List<WordDto> listOfWords)
        {
            var mappedListOfWords = Mapper.Map<List<WordDto>, List<Word>>(listOfWords);
            return Task.Run(() => unitOfWork.GetCustomWordRepository().GetIdOfWordsInPhrase(mappedListOfWords));
        }

        public List<string> GetRepeatedWords(List<WordDto> listOfWords)
        {
           
            var mappedListOfWords = Mapper.Map<List<WordDto>, List<Word>>(listOfWords);
            var listOfRepeatedWords = unitOfWork.GetCustomWordRepository().GetRepeatedWords(mappedListOfWords);
            return listOfRepeatedWords;
        }

        public List<WordDto> GetListOfWordsForAPhrase(string wordsId)
        {
         
            var listOfWords = unitOfWork.GetCustomWordRepository().GetListOfOrderedWordsForAPhrase(wordsId);
            var mappedListOfWords = Mapper.Map<List<Word>, List<WordDto>>(listOfWords);
            return mappedListOfWords;
        }

        public List<WordDto> GetAllWords()
        {
           
            var listOfWords = unitOfWork.GetCustomWordRepository().GetAllWords();
            var mappedListOfWords = Mapper.Map<List<Word>, List<WordDto>>((List<Word>)listOfWords);
            return mappedListOfWords;
        }

        private static void GetMappedEntities()
        {
     
            Mapper.CreateMap<Phrase, PhraseDto>();
            Mapper.CreateMap<Word, WordDto>();
            Mapper.CreateMap<WordDto, Word>();
        }
    }
}
