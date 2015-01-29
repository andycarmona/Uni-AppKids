namespace UniAppKids.Database.Test.FakeEntityModels
{
    using System;
    using System.Collections.Generic;

    using Uni_AppKids.Core.EntityModels;

    public static class FakeWordModel
    {
        public static void GetListOfWordsToVerify(out List<Word> listOfWords)
        {
            listOfWords = new List<Word>()
                              {
                                   new Word()
                                      {
                                          CreationTime = DateTime.Now,
                                          Image = "No image",
                                          SoundFile = "Un sonido",
                                          WordDescription = "loco",
                                          WordId = 2,
                                          WordName = "coche"
                                      },
                                   new Word()
                                      {
                                          CreationTime = DateTime.Now,
                                          Image = "image.jpg",
                                          SoundFile = "Sound.wv",
                                          WordDescription = "loco",
                                          WordId = 2,
                                          WordName = "casa"
                                      },
                                  new Word()
                                      {
                                          CreationTime = DateTime.Now,
                                          Image = null,
                                          SoundFile = null,
                                          WordDescription = "loco",
                                          WordId = 2,
                                          WordName = "feo"
                                      },
                                      new Word()
                                      {
                                          CreationTime = DateTime.Now,
                                          Image = null,
                                          SoundFile = null,
                                          WordDescription = "loco",
                                          WordId = 2,
                                          WordName = "puta"
                                      },
                                      new Word()
                                      {
                                          CreationTime = DateTime.Now,
                                          Image = null,
                                          SoundFile = null,
                                          WordDescription = "loco",
                                          WordId = 2,
                                          WordName = "åöä"
                                      },
                                      new Word()
                                      {
                                          CreationTime = DateTime.Now,
                                          Image = null,
                                          SoundFile = null,
                                          WordDescription = "loco",
                                          WordId = 2,
                                          WordName = "ñllch"
                                      }
                              };
        }
    }
}
