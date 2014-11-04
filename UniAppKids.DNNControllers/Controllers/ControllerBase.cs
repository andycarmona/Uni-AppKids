using System;

namespace UniAppKids.DNNControllers.Services
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Web.Api;

    public class ControllerBase : DnnApiController
    {
        #region "KeepAlive"
        [DnnAuthorize()]
        [HttpGet()]
        public HttpResponseMessage KeepAlive()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, "True");
            }
            catch (Exception ex)
            {
                //Log to DotNetNuke and reply with Error
                Exceptions.LogException(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}