using System.Web.Mvc;

namespace IALDashboard.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            ViewBag.actionName = "Dashboard";
            return View();
        }
    }
}