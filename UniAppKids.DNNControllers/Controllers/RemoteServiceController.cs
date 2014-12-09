namespace UniAppKids.DNNControllers.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.UI.WebControls;

    using DotNetNuke.Web.Api;

    using UniAppKids.DNNControllers.Helpers;

    public class RemoteServiceController : ControllerBase
    {
        [DnnAuthorize]
        [AcceptVerbs("GET")]
          public async Task<HttpResponseMessage> GetListOfImageUrl(string wordToSearch)
        {
            try
            {
                List<string> listOfUrl = await RemoteService.GetJsonDataFromImageSearch(wordToSearch);
              
  
                return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK,listOfUrl);
            }
            catch (Exception ex)
            {
                return this.ControllerContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    "Invalid parameters, Please check there is elements in array");
            }
        }
    }
}