using System.Web.Mvc;

namespace IALDashboard.Controllers
{
    [Filters.AuthorizedUser]
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

    }
}