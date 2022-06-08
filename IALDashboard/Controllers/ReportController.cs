using IALDashboard.DAL;
using System.Data;
using System.Web.Mvc;

namespace IALDashboard.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DailyStockReport()
        {
            DataTable dt = new Stock_DAL().StockTableTemp();

            ViewBag.stocklist = dt;
            return View();
        }
    }
}