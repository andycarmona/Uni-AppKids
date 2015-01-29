namespace UniAppKids.Application.Test.Services
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rhino.Mocks;
    using Rhino.Mocks.Constraints;

    using Uni_AppKids.Application.Services;
    using Uni_AppKids.Core.EntityModels;
    using Uni_AppKids.Database.EntityFramework;
    using Uni_AppKids.Database.Repositories;

    [TestClass]
    public class WordServiceTest
    {
        [TestMethod]
        public void Test_Get_All_Words_In_Dictionary()
        {
            List<Word> listOfWords;
            UniAppKids.Database.Test.FakeEntityModels.FakeWordModel.GetListOfWordsToVerify(out listOfWords);
            var dataContext = MockRepository.GenerateMock<UniAppKidsDbContext>();
            var wordRepository = MockRepository.GenerateStub<WordRepository>(dataContext);
            List<Word> wordsToupdate = new List<Word>();
            wordRepository.GetListOfNotExistingWords(listOfWords, out wordsToupdate);

            //var unitOfWork = new UnitOfWork(dataContext);
            //WordService aWordsService=new WordService();
            //aWordsService.GetAllWords();
            //unitOfWork.GetCustomWordRepository().GetAllWords();
        }



    }
}