namespace Uni_AppKids.Core.Interface
{
    using System.Collections.Generic;

    using Uni_AppKids.Core.EntityModels;

    public interface IWordsRepository
    {
        List<Word> GetListOfOrderedWordsForAPhrase(string wordsId);

        void BulkInsertOfWords(List<Word> listOfWords);

        List<string> GetIdOfWordsInPhrase(List<Word> listOfWords);

        List<string> GetRepeatedWords(List<Word> listOfWords);

        IEnumerable<Word> GetAllWords();
    }
}
