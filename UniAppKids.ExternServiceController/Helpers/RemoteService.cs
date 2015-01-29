namespace UniAppKids.DNNControllers.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json.Linq;

    using Uni_AppKids.Application.Dto;

    public static class RemoteService
    {
        public static async Task<List<WordDto>> GetJsonDataFromImageSearch(string keyWord)
        {
            var urlRequest = "http://ajax.googleapis.com/ajax/services/search/images?start=0&q=" + keyWord + "&v=1.0";
            string jsonResult;
            var listUrl = new List<WordDto>();
            using (var httpClient = new HttpClient())
            {
                var jsonResponse = httpClient.GetStringAsync(urlRequest);
                await jsonResponse;
                jsonResult = jsonResponse.Result;
            }

            var aToken = JObject.Parse(jsonResult);
            var aValue = aToken.Children().Values();

            foreach (var result in aValue["results"])
            {
                foreach (var aProperty in result)
                {
                    if (string.IsNullOrEmpty(aProperty.SelectToken("tbUrl").ToString()))
                    {
                        continue;
                    }
                    var aWord = new WordDto
                                    {
                                        CreationTime = DateTime.Now,
                                        WordName = keyWord,
                                        Image = aProperty.SelectToken("tbUrl").ToString()
                                    };
                    listUrl.Add(aWord);
                }
               
                return listUrl;
            }

            return null;
        }  
    }
}