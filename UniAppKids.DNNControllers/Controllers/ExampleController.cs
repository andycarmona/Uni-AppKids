namespace UniAppKids.DNNControllers.Services
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Web.Api;

    public class ExampleController : ControllerBase
    {
        #region "Web Methods"
        [DnnAuthorize()]
        [HttpGet()]
        public HttpResponseMessage HelloWorld()
        {
            try
            {
                string helloWorld = "Hello World!";
                return Request.CreateResponse(HttpStatusCode.OK, helloWorld);
            }
            catch (System.Exception ex)
            {
                //Log to DotNetNuke and reply with Error
                Exceptions.LogException(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [DnnAuthorize()]
        [AcceptVerbs("GET", "POST")]
        public HttpResponseMessage GoodbyeWorld(bool data)
        {
            try
            {
                string helloWorld = "Hello World!";
                if (data)
                {
                    helloWorld = "Good-bye World!";
                }
                return Request.CreateResponse(HttpStatusCode.OK, helloWorld);
            }
            catch (System.Exception ex)
            {
                //Log to DotNetNuke and reply with Error
                Exceptions.LogException(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region "DTO Classes"
        public class DTOWorldDetails
        {
            public bool goodbye { get; set; }
        }
        #endregion
    }
}