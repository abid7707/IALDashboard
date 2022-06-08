using ClosedXML.Excel;
using IALDashboard.DAL;
using System.Data;
using System.IO;
using System.Web.Mvc;


namespace IALDashboard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            // DataTable dt = new Stock_DAL().DailyStockReport();


            return View();
        }



        [HttpPost]
        public FileResult Export()
        {

            DataTable dt = new Stock_DAL().DailyStockReport();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
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