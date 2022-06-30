using ClosedXML.Excel;
using IALDashboard.DAL;
using Newtonsoft.Json;
using System;
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

            DataTable ro_list = new Collection_DAL().ROListByZone(from_date, zone_name);

            DataTable dt = new Collection_DAL().ROSheet(from_date, ro_code, zone_name);



            /*DataTable dt_ro = new Collection_DAL().getROByCODE(ro_code);
            DataRow ro_row = dt_ro.Rows[0];*/
            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (DataRow row in ro_list.Rows)
                {
                    var ws = wb.Worksheets.Add(row["RO_NAME"].ToString());

                    ws.Column("A").Width = 5;
                    ws.Column("B").Width = 8;
                    ws.Column("C").Width = 12;
                    ws.Column("E").Width = 16;
                    ws.Column("F").Width = 10;
                    ws.Column("J").Width = 12;
                    ws.Column("M").Width = 12;

                    ws.Range("A1:P5").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Cell("A1").Value = "Ifad Autos Ltd (Monthly Target Sheet)";
                    ws.Range("A1:O1").Merge().Style.Font.SetBold().Font.FontSize = 14;

                    ws.Cell("A2").Value = "Month: " + from_date;
                    ws.Range("A2:O2").Merge().Style.Font.SetBold().Font.FontSize = 12;

                    ws.Cell("A3").Value = "RO CODE: " + row["RO_CODE"].ToString() + "RO Name: " + row["RO_NAME"].ToString() + " ZONE: " + zone_name;
                    ws.Range("A3:O3").Merge().Style.Font.SetBold().Font.FontSize = 12;

                    ws.Cell("A5").Value = "SL NO";
                    ws.Range("A5").Style.Font.SetBold().Font.FontSize = 12;

                    ws.Cell("B5").Value = "Order No";
                    ws.Range("B5").Style.Font.SetBold().Font.FontSize = 12;

                    ws.Cell("C5").Value = "Customer Name";
                    ws.Range("C5").Style.Font.SetBold().Font.FontSize = 12;

                    ws.Cell("D5").Value = "Registration No";
                    ws.Range("D5").Style.Font.SetBold().Font.FontSize = 12;

                    ws.Cell("E5").Value = "Model Name";
                    ws.Range("E5").Style.Font.SetBold().Font.FontSize = 12;

                    ws.Cell("F5").Value = "1st Date";
                    ws.Range("F5").Style.Font.SetBold().Font.FontSize = 12;

                    ws.Cell("G5").Value = "EMI AMT.";
                    ws.Range("G5").Style.Font.SetBold().Font.FontSize = 12;

                    ws.Cell("H5").Value = "Tar Inst Amt.";
                    ws.Range("H5").Style.Font.SetBold().Font.FontSize = 12;

                    ws.Cell("I5").Value = "NO.Ovd";
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

                    ws.Cell("O5").Value = "Atten V";
                    ws.Range("O5").Style.Font.SetBold().Font.FontSize = 12;

                    DataRow[] rodt = dt.Select("RO_CODE =" + row["RO_CODE"]);
                    int i = 0;
                    double SUB_EMI_AMOUNT = 0;
                    double SUB_TAR_INST_AMT = 0;
                    double SUB_NO_OF_OVERDUE = 0;
                    double SUB_MR_COLL = 0;
                    double SUB_DUE_DP_AND_DC = 0;
                    double SUB_INS_DC_PAYMENT = 0;
                    double SUB_OVERDUE = 0;
                    double SUB_MONTHLY_COLL = 0;
                    double SUB_ATTEN_V = 0;

                    foreach (DataRow record in rodt)
                    {
                        SUB_EMI_AMOUNT += Convert.ToDouble(record["EMI_AMOUNT"]);
                        SUB_TAR_INST_AMT += Convert.ToDouble(record["TAR_INST_AMT"]);
                        SUB_NO_OF_OVERDUE += Convert.ToDouble(record["NO_OF_OVERDUE"]);
                        SUB_MR_COLL += Convert.ToDouble(record["MR_COLL"]);
                        SUB_DUE_DP_AND_DC += Convert.ToDouble(record["DUE_DP_AND_DC"]);
                        SUB_INS_DC_PAYMENT += Convert.ToDouble(record["INS_DC_PAYMENT"]);
                        SUB_OVERDUE += Convert.ToDouble(record["OVERDUE"]);
                        SUB_MONTHLY_COLL += Convert.ToDouble(record["MONTHLY_COLL"]);
                        SUB_ATTEN_V += Convert.ToDouble(record["ATTEN_V"]);

                        ws.Cell("A" + (i + 6)).Value = i + 1;
                        ws.Cell("B" + (i + 6)).Value = record["ORDER_NO"].ToString();
                        ws.Cell("C" + (i + 6)).Value = record["CUSTOMER_NAME"].ToString();
                        ws.Cell("D" + (i + 6)).Value = record["REGNO"].ToString();
                        ws.Cell("E" + (i + 6)).Value = record["CATALOG_DESC"].ToString();
                        ws.Cell("F" + (i + 6)).Value = record["FSTINSAL_DATE"].ToString();
                        ws.Cell("G" + (i + 6)).Value = record["EMI_AMOUNT"].ToString();
                        ws.Cell("H" + (i + 6)).Value = record["TAR_INST_AMT"].ToString();
                        ws.Cell("I" + (i + 6)).Value = record["NO_OF_OVERDUE"].ToString();
                        ws.Cell("J" + (i + 6)).Value = record["MR_COLL"].ToString();
                        ws.Cell("K" + (i + 6)).Value = record["DUE_DP_AND_DC"].ToString();
                        ws.Cell("L" + (i + 6)).Value = record["INS_DC_PAYMENT"].ToString();
                        ws.Cell("M" + (i + 6)).Value = record["OVERDUE"].ToString();
                        ws.Cell("N" + (i + 6)).Value = record["MONTHLY_COLL"].ToString();
                        ws.Cell("O" + (i + 6)).Value = record["ATTEN_V"].ToString();
                        i++;
                    }
                    ws.Cell("F" + (i + 6)).Value = "Sub Total";
                    ws.Cell("G" + (i + 6)).Value = SUB_EMI_AMOUNT.ToString();
                    ws.Cell("H" + (i + 6)).Value = SUB_TAR_INST_AMT.ToString();
                    ws.Cell("I" + (i + 6)).Value = SUB_NO_OF_OVERDUE.ToString();
                    ws.Cell("J" + (i + 6)).Value = SUB_MR_COLL.ToString();
                    ws.Cell("K" + (i + 6)).Value = SUB_DUE_DP_AND_DC.ToString();
                    ws.Cell("L" + (i + 6)).Value = SUB_INS_DC_PAYMENT.ToString();
                    ws.Cell("M" + (i + 6)).Value = SUB_OVERDUE.ToString();
                    ws.Cell("N" + (i + 6)).Value = SUB_MONTHLY_COLL.ToString();
                    ws.Cell("O" + (i + 6)).Value = SUB_ATTEN_V.ToString();

                    ws.Range("F" + (i + 6) + ":O" + (i + 6)).Style.Font.FontSize = 12;

                    ws.Range("A5" + ":O" + (i + 6)).Style.Alignment.SetWrapText(true);

                    ws.Range("A5" + ":O" + (i + 6)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Range("A5" + ":O" + (i + 6)).Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    ws.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                    ws.PageSetup.AdjustTo(75);
                    ws.PageSetup.SetRowsToRepeatAtTop(1, 5);

                }
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ROSheet-" + zone_name + ".xlsx");
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
                var ws = wb.Worksheets.Add(dt);
                ws.SetAutoFilter(false);
                /*wb.Worksheets.Add(dt);*/

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Collection_" + from_date + ".xlsx");
                }
            }
        }











    }
}