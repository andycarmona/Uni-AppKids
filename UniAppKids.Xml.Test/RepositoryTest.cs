using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniAppKids.Xml.Test
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    using Uni_AppKids.Core.EntityModels;
    using Uni_AppKids.Xml.Repositories;

    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void Test_Get_All_Phrases()
        {
            var aXMLRepository = new XmlPhraseRepository();
            var aListOfPhrases = aXMLRepository.GetAllPhrases();
            if (aListOfPhrases != null)
            {
                Assert.AreEqual("Una casa bonita", aListOfPhrases[0].PhraseText);
                Assert.AreEqual("Otra casa", aListOfPhrases[1].PhraseText);
            }
        }

        [TestMethod]
        public void Test_To_Add_New_Phrase()
        {
            var aXMLRepository = new XmlPhraseRepository();
            var aNewPhrase = new Phrase()
                                    {
                                        PhraseId = 2,
                                        PhraseText = "Otra casa",
                                        WordsIds = "1,2,3",
                                        AssignedDictionaryId = 1
                                    };
            aXMLRepository.AddNewPhrase(aNewPhrase);
          
        }
        [TestMethod]
        public void Test_Get_All_Phrases_Repository_Right_Values()
        {
            var aXMLRepository = new XmlPhraseRepository();

            var aListOfPhrases = aXMLRepository.GetPhrasesInDictionary(1, 10);
            if (aListOfPhrases != null)
            {
                Assert.AreEqual("Una casa bonita", aListOfPhrases[0].PhraseText);
          
            }
            aListOfPhrases=aXMLRepository.GetPhrasesInDictionary(2, 10);

            if (aListOfPhrases != null)
            {
            
                Assert.AreEqual("Otra casa", aListOfPhrases[0].PhraseText);
            }
        }
    }
}
