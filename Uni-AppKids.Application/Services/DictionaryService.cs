// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DictionaryService.cs" company="Uni-App">
//   -
// </copyright>
// <summary>
//   -
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uni_AppKids.Application.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using Uni_AppKids.Application.Dto;
    using Uni_AppKids.Core.EntityModels;

    public class DictionaryService
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        public PhraseDictionaryDto GetADictionary(int id)
        {
            GetMappedEntities();

            var aDictionary = unitOfWork.GetGenericPhraseDictionaryRepository().GetByID(id);

            var aPDictionary = Mapper.Map<PhraseDictionary, PhraseDictionaryDto>(aDictionary);

            return aPDictionary;
        }



        public List<PhraseDictionaryDto> GetUserPhraseDictionaries(string userName)
        {
            GetMappedEntities();
            var phraseDictionaries = unitOfWork.GetCustomPhraseDictionaryRepository().GetUserDictionaries(userName);
         
            var dictionaryList = Mapper.Map<List<PhraseDictionary>, List<PhraseDictionaryDto>>(phraseDictionaries.ToList());
            return dictionaryList;
        }

        public void UpdatePhraseDictionary(UpdatePhraseDictionaryInput input)
        {
            //Logger.Info("Updating a dictionary for input: " + input);

            var phraseDictionary = unitOfWork.GetGenericPhraseDictionaryRepository().GetByID(input.DictionaryId);

            phraseDictionary.DictionaryName = input.DictionaryName;
            unitOfWork.GetGenericPhraseDictionaryRepository().Update(phraseDictionary);
        }

        public void CreatePhraseDictionary(CreatePhraseDictionaryInput input)
        {
            //Logger.Info("Creating a new dictionary: " + input);
            GetMappedEntities();

            var dictionaryEntity = new PhraseDictionary();
            dictionaryEntity = Mapper.Map<CreatePhraseDictionaryInput, PhraseDictionary>(input);

            unitOfWork.GetGenericPhraseDictionaryRepository().Insert(dictionaryEntity);
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
