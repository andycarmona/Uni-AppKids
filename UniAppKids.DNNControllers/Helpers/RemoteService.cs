namespace UniAppKids.DNNControllers.Helpers
{
    using System.Collections.Generic;
    using System.IO;

    using System.Net.Http;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.UI.WebControls;

    using DotNetNuke.Web.UI.WebControls;

    using Newtonsoft.Json.Linq;

    public static class RemoteService
    {
        public static async Task<List<string>> GetJsonDataFromImageSearch(string keyWord)
        {
            string urlRequest = "http://ajax.googleapis.com/ajax/services/search/images?start=0&q=" + keyWord + "&v=1.0";
            string jsonResult;
            var listUrl = new List<string>();
            using (var httpClient = new HttpClient())
            {
                Task<string> jsonResponse = httpClient.GetStringAsync(urlRequest);
                await jsonResponse;
                jsonResult = jsonResponse.Result;
            }

            JObject aToken = JObject.Parse(jsonResult);
            IJEnumerable<JToken> aValue = aToken.Children().Values();

            foreach (var result in aValue["results"])
            {
                foreach (var aProperty in result)
                {
                    if (!string.IsNullOrEmpty(aProperty.SelectToken("tbUrl").ToString()))
                    {
                        listUrl.Add(aProperty.SelectToken("tbUrl").ToString());
                    }
                }

                return listUrl;
            }

            return null;
        }  
    }
}