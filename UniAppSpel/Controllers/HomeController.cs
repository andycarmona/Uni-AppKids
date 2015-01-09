namespace UniAppSpel.Controllers
{
    using System.Web.Mvc;

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
