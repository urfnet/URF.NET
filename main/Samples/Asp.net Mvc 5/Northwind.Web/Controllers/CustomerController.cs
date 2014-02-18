#region

using System.Web.Mvc;

#endregion

namespace Northwind.Web.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }
    }
}