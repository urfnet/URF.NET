#region

using System.Web.Mvc;

#endregion

namespace Northwind.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Spa()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}