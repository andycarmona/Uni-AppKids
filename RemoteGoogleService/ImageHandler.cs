namespace RemoteGoogleService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mime;

    using Newtonsoft;
    using Google.API.Search;

    public class ImageHandler
    {
       
        public string GetImageUrl()
        {
            var aUrl = "";
            var client = new GimageSearchClient("http://www.google.com");
          
            IList<IImageResult> results = client.Search("manzana", 5);
         
            if (results.Any())
            {    
                return string.Format("{0}--{1} x {2} - {3} -- {4}",
                                     results[0].Content,
                                     results[0].Width,
                                     results[0].Height,
                                     results[0].Title,
                                     results[0].VisibleUrl);
       
            }  
       
            return aUrl;
        }
    }
}
