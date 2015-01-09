namespace UniAppKids.Xml.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    using Uni_AppKids.Core.EntityModels;

    public class XmlPhraseRepository
    {
        public List<Phrase> GetAllPhrases(string actualXmlFile)
        {
            var xelement = XElement.Load(actualXmlFile);
            var phrases = xelement.Elements();
            return phrases.Select(phrase => new Phrase() { PhraseId = Convert.ToInt32(phrase.Element("PhraseId").Value), PhraseText = phrase.Element("PhraseText").Value }).ToList();
        }

        public void AddNewPhrase(Phrase aNewPhrase, string actualXmlFile)
        {
            var xelement = XElement.Load(actualXmlFile);
            xelement.Add(
                new XElement("Phrase",
                    new XElement("PhraseId", aNewPhrase.PhraseId),
                    new XElement("PhraseText", aNewPhrase.PhraseText),
                    new XElement("AssignedDictionary", aNewPhrase.AssignedDictionaryId)));
            xelement.Save(actualXmlFile);
        }
    }
}
