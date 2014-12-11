using System;
using System.Web.Mvc;

namespace UniAppSpel.Controllers
{
    public class JasmineController : Controller
    {
        public ViewResult Run()
        {
            return View("SpecRunner");
        }
    }
}
