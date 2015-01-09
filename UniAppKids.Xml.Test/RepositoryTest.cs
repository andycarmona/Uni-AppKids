using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniAppKids.Xml.Test
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    using UniAppKids.Xml.Repositories;

    using Uni_AppKids.Core.EntityModels;

    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void Test_Get_All_Phrases()
        {
            var aXMLRepository = new XmlPhraseRepository();
            var aListOfPhrases = aXMLRepository.GetAllPhrases(@"../../Templates/Phrase.xml");
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
            aXMLRepository.AddNewPhrase(aNewPhrase, @"../../Templates/Phrase.xml");
          
        }
    }
}
