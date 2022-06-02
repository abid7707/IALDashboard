using IALDashboard.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace IALDashboard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            DataTable dt =  new Stock_DAL().DailyStockReport();


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