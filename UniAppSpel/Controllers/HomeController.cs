namespace UniAppSpel.Controllers
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Web.Mvc;

    using Google.API.Search;

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
