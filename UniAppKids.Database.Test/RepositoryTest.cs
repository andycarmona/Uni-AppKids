using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniAppKids.Database.Test
{
    using System.Collections.Generic;

    using Uni_AppKids.Core.EntityModels;
    using Uni_AppKids.Database.EntityFramework;
    using Uni_AppKids.Database.Repositories;

    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void Test_If_List_Is_Holding_Wods_Not_Existing_In_DB()
        {
            var context = new UniAppKidsDbContext();
            var aWordRepository = new WordRepository(context);
            List<Word> listOfWords;
            FakeEntityModels.FakeWordModel.GetListOfWordsToVerify(out listOfWords);
            List<Word> wordsToupdate;
            var nonRepeatedList = aWordRepository.GetListOfNotExistingWords(listOfWords,out wordsToupdate);
            foreach (var aNonRepeatedWord in nonRepeatedList)
            {
                
                Assert.AreNotSame("casa",aNonRepeatedWord);
            }
         
        }

        [TestMethod]
        public void Test_If_Two_Entitities_Are_TheSame()
        {
            var context = new UniAppKidsDbContext();
            var aGenericRepository = new GenericRepository<Word>(context);
            List<Word> listOfWords;
            FakeEntityModels.FakeWordModel.GetListOfWordsToVerify(out listOfWords);
           var resultComparation= aGenericRepository.Compare(listOfWords[0], listOfWords[1]);
            Assert.IsFalse(resultComparation);
           //resultComparation = aGenericRepository.Compare(listOfWords[0], listOfWords[2]);
           //Assert.IsFalse(resultComparation);
        }

        [TestMethod]
        public void Test_If_can_Updat_Data_Of_list_of_words()
        {
            var context = new UniAppKidsDbContext();
            var aWordRepository = new WordRepository(context);
   
            List<Word> listOfWords;
            FakeEntityModels.FakeWordModel.GetListOfWordsToVerify(out listOfWords);
            aWordRepository.UpdatePropertiesOfRepeatedWords(listOfWords);
        }
        //[TestMethod]
        //public void Test_to_Delete_A_Phrase()
        //{
        //    var context = new UniAppKidsDbContext();
        //    var aGenericRepository = new GenericRepository<Phrase>(context);
        // aGenericRepository.Delete(23);
        //    context.SaveChanges();
        //}
    }
}
