namespace UniAppKids.ExternServiceController.Controllers
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
    using System.Web.Http.Cors;

    using UniAppKids.DNNControllers.Helpers;
    using UniAppKids.ExternServiceController.Helpers;

    using Uni_AppKids.Application.Dto;

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/ExternalDataController")]
    public class RemoteServiceController : ApiController
    {
        [AcceptVerbs("GET")]
        [Route("CheckIfFileExists")]
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

        [AcceptVerbs("GET")]
        [Route("GetWordDescriptionFromWiki")]
        public HttpResponseMessage GetWordDescriptionFromWiki(string keyWord)
        {
            try
            {
                
                var strippedWord = WordFilterTool.RemoveSpecialCharacters(keyWord);
                var cleanWordResult = WordFilterTool.RemoveAccentOnVowels(strippedWord);
                var urlToSearch =
                    string.Format(
                        "http://es.wikipedia.org/w/index.php?action=render&title={0}&prop=revisions&rvprop=content",
                        cleanWordResult);

                string encodedJsonResult;
                using (var webClient = new WebClient())
                {
                    var jsonResult = webClient.DownloadString(urlToSearch);
                    byte[] bytes = Encoding.Default.GetBytes(jsonResult);
                    encodedJsonResult = Encoding.UTF8.GetString(bytes);
                }

                return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK, encodedJsonResult);
            }
            catch (Exception)
            {
                return this.ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, "<p>No encontre resultados para esta palabra</p> ");
            }
        }

        [AcceptVerbs("GET")]
        [Route("GetWordDescriptionFromRae")]
        public HttpResponseMessage GetWordDescriptionFromRae(string keyWord)
        {
            try
            {
                var strippedWord = WordFilterTool.RemoveSpecialCharacters(keyWord);
                var cleanWordResult = WordFilterTool.RemoveAccentOnVowels(strippedWord);
                var urlToSearch = string.Format("http://lema.rae.es/drae/srv/search?val={0}", cleanWordResult);

                return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK, urlToSearch);
            }
            catch (Exception)
            {
                return this.ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid words");
            }
        }

        [AcceptVerbs("GET")]
        [Route("GetListOfImageUrl")]
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

        [AcceptVerbs("POST")]
        [Route("PostSoundFileAsync")]
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