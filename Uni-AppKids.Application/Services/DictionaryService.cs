using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uni_AppKids.Application.Services
{
    using AutoMapper;

    using Uni_AppKids.Application.Dto;
    using Uni_AppKids.Core.EntityModels;

    public class DictionaryService
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public PhraseDictionaryDto GetADictionary(int id)
        {
            GetMappedEntities();

            var aDictionary = unitOfWork.PhraseDictionaryRepository.GetByID(id);

            var aPDictionary = Mapper.Map<PhraseDictionary, PhraseDictionaryDto>(aDictionary);

            return aPDictionary;
        }



        public List<PhraseDictionaryDto> GetPhraseDictionaries()
        {
            GetMappedEntities();
            var phraseDictionaries = unitOfWork.SpecPhraseDictionaryRepository.GetDictionaries();
            //var phraseDictionaries = unitOfWork.PhraseDictionaryRepository.Get();

            var dictionaryList = Mapper.Map<List<PhraseDictionary>, List<PhraseDictionaryDto>>(phraseDictionaries.ToList());
            return dictionaryList;
        }

        public void UpdatePhraseDictionary(UpdatePhraseDictionaryInput input)
        {
            //Logger.Info("Updating a dictionary for input: " + input);

            var phraseDictionary = unitOfWork.PhraseDictionaryRepository.GetByID(input.DictionaryId);

            phraseDictionary.DictionaryName = input.DictionaryName;
            unitOfWork.PhraseDictionaryRepository.Update(phraseDictionary);
        }

        public void CreatePhraseDictionary(CreatePhraseDictionaryInput input)
        {
            //Logger.Info("Creating a new dictionary: " + input);
            GetMappedEntities();

            var dictionaryEntity = new PhraseDictionary();
            dictionaryEntity = Mapper.Map<CreatePhraseDictionaryInput, PhraseDictionary>(input);

            unitOfWork.PhraseDictionaryRepository.Insert(dictionaryEntity);
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
