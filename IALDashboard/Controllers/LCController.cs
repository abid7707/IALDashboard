using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IALDashboard.DAL;

namespace IALDashboard.Controllers
{
    [Filters.AuthorizedUser]
    public class LCController : Controller
    {
        // GET: LC
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartLCEntry() {
            ViewBag.actionName = "Part LC Stock";
            DataTable dt = new Stock_DAL().GetPartLCStock();

            ViewBag.part_lc_stock=dt;

            return View();
        }


        [HttpPost]
        public ActionResult LCStockSave(string[] PART_NO, int[] LC_QTY) {

            new Stock_DAL().savePartLCStock(PART_NO, LC_QTY); // Save data

            DataTable dt = new Stock_DAL().GetPartLCStock();
            ViewBag.actionName = "Part LC Stock";
            ViewBag.part_lc_stock=dt;
            ViewBag.save_message = "Save Successfully";

            return View("PartLCEntry");

        }
    }
}