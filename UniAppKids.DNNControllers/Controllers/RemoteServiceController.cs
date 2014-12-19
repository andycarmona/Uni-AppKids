namespace UniAppKids.DNNControllers.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text;
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
        public HttpResponseMessage CheckIfFileExists(string path)
        {
            try
            {
                var relativePath = HttpContext.Current.Server.MapPath("~/" + path);
                File.Open(relativePath, FileMode.Open);
                return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (FileNotFoundException ex)
            {
                return this.ControllerContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    "Invalid parameters, Please check there is elements in array");
            }
        }

        [AllowAnonymous]
        [AcceptVerbs("GET")]
        public HttpResponseMessage GetWordDescriptionFromWiki(string keyWord)
        {
            var urlToSearch = string.Format(
                    "http://es.wikipedia.org/w/index.php?action=render&title={0}&prop=revisions&rvprop=content",
                    keyWord);

            string encodedJsonResult;
            using (var webClient = new WebClient())
            {
                var jsonResult = webClient.DownloadString(urlToSearch);
                byte[] bytes = Encoding.Default.GetBytes(jsonResult);
                encodedJsonResult = Encoding.UTF8.GetString(bytes);
            }
            return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK, encodedJsonResult);
        }

        [AllowAnonymous]
        [AcceptVerbs("GET")]
        public HttpResponseMessage GetWordDescriptionFromRae(string keyWord)
        {
            var urlToSearch = string.Format(
                    "http://buscon.rae.es/drae/srv/search?val={0}",
                    keyWord);

            string encodedJsonResult;
            using (var webClient = new WebClient())
            {
                var jsonResult = webClient.DownloadString(urlToSearch);
                byte[] bytes = Encoding.Default.GetBytes(jsonResult);
                encodedJsonResult = Encoding.UTF8.GetString(bytes);
            }
            return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK, encodedJsonResult);
        }

        [DnnAuthorize]
        [AcceptVerbs("GET")]
        public async Task<HttpResponseMessage> GetListOfImageUrl(string wordToSearch)
        {
            try
            {
                List<WordDto> listOfUrl = await RemoteService.GetJsonDataFromImageSearch(wordToSearch);


                return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK, listOfUrl);
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
                    var fi = new FileInfo(file.LocalFileName);
                    messages.Add("File uploaded as " + fi.FullName + " (" + fi.Length + " bytes)");
                }

                return messages;
            }

            var response = this.Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Request!");
            throw new HttpResponseException(response);
        }
    }
}