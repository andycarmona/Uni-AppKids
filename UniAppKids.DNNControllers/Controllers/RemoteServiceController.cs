namespace UniAppKids.DNNControllers.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using System.Web.UI.WebControls;

    using DotNetNuke.Web.Api;

    using UniAppKids.DNNControllers.Helpers;

    using Uni_AppKids.Application.Dto;

    public class RemoteServiceController : ControllerBase
    {
        [DnnAuthorize]
        [AcceptVerbs("GET")]
          public async Task<HttpResponseMessage> GetListOfImageUrl(string wordToSearch)
        {
            try
            {
                List<WordDto> listOfUrl = await RemoteService.GetJsonDataFromImageSearch(wordToSearch);
              
  
                return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK , listOfUrl);
            }
            catch (Exception ex)
            {
                return this.ControllerContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    "Invalid parameters, Please check there is elements in array");
            }
        }

        [DnnAuthorize]
        [AcceptVerbs("POST")]
        public async Task<List<string>> PostSoundFileAsync()
        {

            if (this.Request.Content.IsMimeMultipartContent())
            {
                var uploadPath = HttpContext.Current.Server.MapPath("~/Uploads");

                var streamProvider = new MyStreamProvider(uploadPath);

                await this.Request.Content.ReadAsMultipartAsync(streamProvider);

                var messages = new List<string>();
                foreach (var file in streamProvider.FileData)
                {
                    FileInfo fi = new FileInfo(file.LocalFileName);
                    messages.Add("File uploaded as " + fi.FullName + " (" + fi.Length + " bytes)");
                }

                return messages;
            }

            var response = this.Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Request!");
            throw new HttpResponseException(response);
        }
    }
}