using ClosedXML.Excel;
using IALDashboard.DAL;
using Newtonsoft.Json;
using System.Data;
using System.IO;
using System.Web.Mvc;

namespace IALDashboard.Controllers
{
    [Filters.AuthorizedUser]
    public class RecoveryController : Controller
    {
        // GET: Recovery
        public ActionResult CollectionReport()
        {
            return View();
        }

        //-------------------------------RO SHEET-------------------------------
        public ActionResult RoSheet()
        {
            ViewBag.actionName = "RO Sheet";
            string zone_name = "";
            DataTable dt_ro = new Collection_DAL().ROInfo(zone_name);
            DataTable dt_zone = new Collection_DAL().ZoneInfo();
            ViewBag.rolist = dt_ro;
            ViewBag.zonelist = dt_zone;
            return View();
        }


        [HttpPost]
        public string GetJsonROList(string from_date, string ro_code, string zone_name)
        {
            from_date = from_date + "-01";

            DataTable dt = new Collection_DAL().ROSheet(from_date, ro_code, zone_name);

            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }

        [HttpPost]
        public string GetJsonROListByZone(string from_date, string zone_name)
        {
            from_date = from_date + "-01";

            DataTable dt = new Collection_DAL().ROListByZone(from_date, zone_name);

            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }


        [HttpGet]
        public ActionResult ExportROSheet(string from_date, string ro_code, string zone_name)
        {
            from_date = from_date + "-01";

            DataTable dt = new Collection_DAL().ROSheet(from_date, ro_code, zone_name);
            DataTable dt_ro = new Collection_DAL().getROByCODE(ro_code);
            DataRow ro_row = dt_ro.Rows[0];

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Stock Report");

                ws.Range("A1:P5").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                ws.Cell("A1").Value = "Monthly Target Sheet ";
                ws.Range("A1:P1").Merge().Style.Font.SetBold().Font.FontSize = 14;

                ws.Cell("A2").Value = "Month: " + from_date;
                ws.Range("A2:P2").Merge().Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("A3").Value = "RO Name: " + ro_row["RO_NAME"];
                ws.Range("A3:P3").Merge().Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("A5").Value = "Sl NO";
                ws.Range("A5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("B5").Value = "Order No";
                ws.Range("B5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("C5").Value = "Customer Name";
                ws.Range("C5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("D5").Value = "Registration No";
                ws.Range("D5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("E5").Value = "Model Name";
                ws.Range("E5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("F5").Value = "1st EMI DATE";
                ws.Range("F5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("G5").Value = "EMI AMT.";
                ws.Range("G5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("H5").Value = "Target INST AMT.";
                ws.Range("H5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("I5").Value = "NO of Overdue";
                ws.Range("I5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("J5").Value = "MR COLL";
                ws.Range("J5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("K5").Value = "DP+DC Due";
                ws.Range("K5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("L5").Value = "INST DC";
                ws.Range("L5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("M5").Value = "Overdue";
                ws.Range("M5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("N5").Value = "Monthly Coll";
                ws.Range("N5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("O5").Value = "Inst. Coll";
                ws.Range("O5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("P5").Value = "Atten V";
                ws.Range("P5").Style.Font.SetBold().Font.FontSize = 12;


                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ws.Cell("A" + (i + 6)).Value = i + 1;
                        ws.Cell("B" + (i + 6)).Value = dt.Rows[i]["ORDER_NO"];
                        ws.Cell("C" + (i + 6)).Value = dt.Rows[i]["CUSTOMER_NAME"];
                        ws.Cell("D" + (i + 6)).Value = dt.Rows[i]["REGNO"];
                        ws.Cell("E" + (i + 6)).Value = dt.Rows[i]["CATALOG_DESC"];
                        ws.Cell("F" + (i + 6)).Value = dt.Rows[i]["FSTINSAL_DATE"];
                        ws.Cell("G" + (i + 6)).Value = dt.Rows[i]["EMI_AMOUNT"];
                        ws.Cell("H" + (i + 6)).Value = dt.Rows[i]["TAR_INST_AMT"];
                        ws.Cell("I" + (i + 6)).Value = dt.Rows[i]["NO_OF_OVERDUE"];
                        ws.Cell("J" + (i + 6)).Value = dt.Rows[i]["MR_COLL"];
                        ws.Cell("K" + (i + 6)).Value = dt.Rows[i]["DP_DC_PAYMENT"];
                        ws.Cell("L" + (i + 6)).Value = dt.Rows[i]["INS_DC_PAYMENT"];
                        ws.Cell("M" + (i + 6)).Value = dt.Rows[i]["OVERDUE"];
                        ws.Cell("N" + (i + 6)).Value = dt.Rows[i]["MONTHLY_COLL"];
                        ws.Cell("O" + (i + 6)).Value = dt.Rows[i]["INST_COLL"];
                        ws.Cell("P" + (i + 6)).Value = dt.Rows[i]["ATTEN_V"];
                    }

                }

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ROSheet.xlsx");
                }

            }


        }

        //-------------------------------RO SUMMARY-------------------------------


        public ActionResult RoSummary()
        {
            ViewBag.actionName = "RO Summary";
            DataTable dt_zone = new Collection_DAL().ZoneInfo();
            ViewBag.zonelist = dt_zone;
            return View();
        }


        [HttpPost]
        public string GetJsonROSummary(string from_date, string zone_name)
        {
            from_date = from_date + "-01";

            DataTable dt = new Collection_DAL().ROSummary(from_date, zone_name);

            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }

        //-------------------------------RO ZONE WISE SUMMARY-------------------------------


        public ActionResult RoZoneWiseSummary()
        {
            ViewBag.actionName = "RO Zone Wise Summary";
            return View();
        }


        [HttpPost]
        public string GetJsonRoZoneWiseSummary(string from_date)
        {
            from_date = from_date + "-01";

            string[] new_from_date = from_date.Split('-');

            string year = new_from_date[0];
            string month = new_from_date[1];

            DataTable dt = new Collection_DAL().RoZoneWiseSummary(from_date);

            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }



        //-------------------------------Collection Report-------------------------------
        [HttpPost]
        public FileResult ExportCollectionReport(string from_date)
        {

            string date = from_date + "-01";
            DataTable dt = new Collection_DAL().CollectionReport(date);
            var rows = dt.Rows.Count;
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Collection_" + from_date + ".xlsx");
                }
            }
        }











    }
}