namespace Uni_APPKids.Interface
{
    using Uni_AppKids.Core.EntityModels;
    using Uni_AppKids.Database.Repositories;

    public interface IUnitOfWork
    {
        GenericRepository<PhraseDictionary> GetGenericPhraseDictionaryRepository();

        PhraseDictionaryRepository GetCustomPhraseDictionaryRepository();

        GenericRepository<Word> GetGenericWordRepository();
    }
}
