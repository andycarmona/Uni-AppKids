namespace UniAppSpel.Controllers
{
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
            var configHandler = new ConfigSettingsHandler();
            ViewBag.urlWorldHandler = configHandler.ReadSetting("WordServiceUrl");
            return View();
        }

        public JsonResult GetWordServiceUrl()
        {
            var configHandler = new ConfigSettingsHandler();
            return this.Json(configHandler.ReadSetting("WordServiceUrl"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetExternalWordServiceUrl()
        {
            var configHandler = new ConfigSettingsHandler();
            return this.Json(configHandler.ReadSetting("ExternalServiceUrl"), JsonRequestBehavior.AllowGet);
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
