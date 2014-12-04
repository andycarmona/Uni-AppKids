using System.Web.Mvc;

namespace UniAppSpel.Controllers
{
    using Telerik.Web.UI.Calendar.Persistence;

    using Uni_AppKids.Application.Dto;

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

        
    }
}
