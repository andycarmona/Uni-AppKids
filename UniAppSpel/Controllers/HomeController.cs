namespace UniAppSpel.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Helpers;

    [OutputCache(Duration = 30, VaryByParam = "none")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        public JsonResult GetServiceUrl()
        {
            var configHandler = new ConfigSettingsHandler();
            var urlList = new List<string>
                              {
                                  configHandler.ReadSetting("ExternalServiceUrl"),
                                  configHandler.ReadSetting("WordServiceUrl")
                              };
            return this.Json(urlList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddPhrase()
        {
            return this.View();
        }

        public ActionResult SoundRecorder()
        {
            return this.View();
        }

        public ActionResult TestPage()
        {
            return this.View();
        }

    }
}
