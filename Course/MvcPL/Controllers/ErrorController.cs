using System.Web.Mvc;

namespace MvcPL.Controllers
{
    public class ErrorController : Controller
    { 
        public ActionResult NotFound()
        {
            return View();
        }
    }
}