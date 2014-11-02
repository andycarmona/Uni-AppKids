namespace Uni_AppKids.Core.Interface
{
    using System.Collections;
    using System.Collections.Generic;

    using Uni_AppKids.Core.EntityModels;

    public interface IWordsRepository
    {
        List<Word> GetListOfOrderedWordsForAPhrase(string wordsId);
    }
}
