using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniAppKids.Database.Test
{
    using System.Collections.Generic;

    using Uni_AppKids.Core.EntityModels;
    using Uni_AppKids.Database.EntityFramework;
    using Uni_AppKids.Database.Repositories;

    [TestClass]
    public class GameObjectRepositoryTest
    {
     

 
        public void Test_Adding_A_GameObject()
        {
            var context = new UniAppKidsDbContext();
            var aGenericRepository = new GenericRepository<GameObject>(context);
            GameObject aGameObject=new GameObject();
            aGameObject.Description = "Wordsoup";
            aGenericRepository.Insert(aGameObject);
            context.SaveChanges();
            
        }

    
        public void Test_Adding_A_GameList_With_ExistingID()
        {
            var context = new UniAppKidsDbContext();
            var aGenericRepository = new GenericRepository<GameList>(context);
            GameList aGameList = new GameList()
                                     {
                                         GameName =
                                             "http://en.educaplay.com/en/learningresources/1696028/html5/nombres_propios.htm#!",
                                         DifficultyLevel = 1,
                                         AssignedGameObjectId = 1
                                     };
           
            aGenericRepository.Insert(aGameList);
            context.SaveChanges();

        }
    }
}
