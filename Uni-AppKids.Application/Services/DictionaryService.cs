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

    using Microsoft.Build.Utilities;

    using Uni_AppKids.Application.Dto;
    using Uni_AppKids.Core.EntityModels;
    using Uni_AppKids.Database.EntityFramework;

    public class DictionaryService
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new UniAppKidsDbContext());

        public PhraseDictionaryDto GetADictionary(int id)
        {
            GetMappedEntities();

            var aDictionary = unitOfWork.GetGenericPhraseDictionaryRepository().GetByID(id);

            var aPDictionary = Mapper.Map<PhraseDictionary, PhraseDictionaryDto>(aDictionary);

            return aPDictionary;
        }



        //public List<PhraseDictionaryDto> GetUserPhraseDictionaries(string userName)
        //{
        //    GetMappedEntities();
        //    var phraseDictionaries = unitOfWork.GetCustomPhraseDictionaryRepository().GetUserDictionaries();
         
        //    var dictionaryList = Mapper.Map<List<PhraseDictionary>, List<PhraseDictionaryDto>>(phraseDictionaries.ToList());
        //    return dictionaryList;
        //}

        public void UpdatePhraseDictionary(UpdatePhraseDictionaryInput input)
        {
            //Logger.Info("Updating a dictionary for input: " + input);

            var phraseDictionary = unitOfWork.GetGenericPhraseDictionaryRepository().GetByID(input.DictionaryId);

            phraseDictionary.DictionaryName = input.DictionaryName;
            unitOfWork.GetGenericPhraseDictionaryRepository().Update(phraseDictionary);
        }

        public void CreatePhraseDictionary(CreatePhraseDictionaryInput input)
        {
          
            GetMappedEntities();

            var dictionaryEntity = new PhraseDictionary();
            dictionaryEntity = Mapper.Map<CreatePhraseDictionaryInput, PhraseDictionary>(input);

            unitOfWork.GetGenericPhraseDictionaryRepository().Insert(dictionaryEntity);
        }

        private static void GetMappedEntities()
        {
            Mapper.CreateMap<PhraseDictionary, PhraseDictionaryDto>(); 
            Mapper.CreateMap<Phrase, PhraseDto>();
            Mapper.CreateMap<Word, WordDto>();
        }
    }
}
