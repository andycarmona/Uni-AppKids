// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlPhraseRepository.cs" company="-">
//   -
// </copyright>
// <summary>
//   -
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uni_AppKids.Xml.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Xml.Linq;

    using Uni_AppKids.Core.EntityModels;
    using Uni_AppKids.Core.Interface;

    public class XmlPhraseRepository : IPhraseRepository
    {
        private const string ActualXmlFile = @"../../Templates/Phrase.xml";

        public List<Phrase> GetAllPhrases()
        {
            var xelement = XElement.Load(ActualXmlFile);
            var phrases = xelement.Elements();
            return phrases.Select(phrase => new Phrase() { PhraseId = Convert.ToInt32(phrase.Element("PhraseId").Value), PhraseText = phrase.Element("PhraseText").Value }).ToList();
        }

        public void AddNewPhrase(Phrase aNewPhrase)
        {
            var xelement = XElement.Load(ActualXmlFile);
            xelement.Add(
                new XElement("Phrase",
                    new XElement("PhraseId", aNewPhrase.PhraseId),
                    new XElement("PhraseText", aNewPhrase.PhraseText),
                    new XElement("AssignedDictionary", aNewPhrase.AssignedDictionaryId)));
            xelement.Save(ActualXmlFile);
        }

        public List<Phrase> GetPhrasesInDictionary(int dictionaryId, int totalPages)
        {
            var xelement = XElement.Load(HttpContext.Current.Server.MapPath(ActualXmlFile));
            var phrases = xelement.Elements();
            var phrasesResult = (from aPhrase in phrases
                                 where (int)aPhrase.Element("AssignedDictionary") == dictionaryId
                                 let aPhraseId = aPhrase.Element("PhraseId")
                                 where aPhraseId != null
                                 let aPhraseText = aPhrase.Element("PhraseText")
                                 where aPhraseText != null
                                 let aPhraseWordsIds = aPhrase.Element("WordsIds")
                                 where aPhraseWordsIds != null
                                 let aPhraseCreationTime = aPhrase.Element("CreationTime")
                                 where aPhraseCreationTime != null
                                 select (new Phrase()
                                          {
                                              PhraseId = Convert.ToInt32(aPhraseId.Value),
                                              PhraseText = aPhraseText.Value,
                                              WordsIds = aPhraseWordsIds.Value,
                                              CreationTime = Convert.ToDateTime(aPhraseCreationTime.Value) 
                                          })).ToList();
            return phrasesResult;
        }
    }
}
