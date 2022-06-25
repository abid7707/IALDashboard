﻿using ClosedXML.Excel;
using IALDashboard.DAL;
using Newtonsoft.Json;
using System.Data;
using System.IO;
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

        //-------------------------------RO SHEET-------------------------------
        public ActionResult RoSheet()
        {
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
        public FileResult ExportROSheet(string from_date, string ro_code, string ro_name, string zone_name)
        {
            from_date = from_date + "-01";

            DataTable dt = new Collection_DAL().ROSheet(from_date, ro_code, zone_name);

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Stock Report");
                ws.Cell("A1").Value = "Monthly Target Sheet " + ro_name;
                ws.Range("A1:AO1").Merge().Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("A2").Value = "Month: " + from_date;
                ws.Range("A2:AO2").Merge().Style.Font.SetBold().Font.FontSize = 12;

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

                ws.Cell("G5").Value = "Target INST AMT";
                ws.Range("G5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("H5").Value = "NO of Overdue";
                ws.Range("H5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("I5").Value = "MR COLL";
                ws.Range("I5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("J5").Value = "DP+DC Due";
                ws.Range("J5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("K5").Value = "INST DC";
                ws.Range("K5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("L5").Value = "Overdue";
                ws.Range("L5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("M5").Value = "Monthly Coll";
                ws.Range("M5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("N5").Value = "Inst. Coll";
                ws.Range("N5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("O5").Value = "Atten V";
                ws.Range("O5").Style.Font.SetBold().Font.FontSize = 12;


                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ws.Cell("A" + (i + 6)).Value = i + 1;
                        ws.Cell("B" + (i + 6)).Value = dt.Rows[i]["ORDER_NO"];
                        ws.Cell("C" + (i + 6)).Value = dt.Rows[i]["CUSTOMER_NAME"];
                        ws.Cell("D" + (i + 6)).Value = dt.Rows[i]["REGNO"];
                        ws.Cell("E" + (i + 6)).Value = dt.Rows[i]["CATALOG_DESC"];
                        /*ws.Cell("F" + (i + 6)).Value = dt.Rows[i][""];*/
                        ws.Cell("G" + (i + 6)).Value = dt.Rows[i]["TAR_INST_AMT"];
                        ws.Cell("H" + (i + 6)).Value = dt.Rows[i]["NO_OF_OVERDUE"];
                        ws.Cell("I" + (i + 6)).Value = dt.Rows[i]["MR_COLL"];
                        /*ws.Cell("J" + (i + 6)).Value = dt.Rows[i][""];
                        ws.Cell("K" + (i + 6)).Value = dt.Rows[i]["NO_OF_OVERDUE"];*/
                        ws.Cell("L" + (i + 6)).Value = dt.Rows[i]["OVERDUE"];
                        ws.Cell("M" + (i + 6)).Value = dt.Rows[i]["MONTHLY_COLL"];
                        ws.Cell("N" + (i + 6)).Value = dt.Rows[i]["INST_COLL"];
                        ws.Cell("O" + (i + 6)).Value = dt.Rows[i]["ATTEN_V"];
                    }

                }

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }

            }


        }


        //-------------------------------RO SUMMARY-------------------------------


        public ActionResult RoSummary()
        {
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


        //-------------------------------DAILY STOCK REPORT-------------------------------
        public ActionResult DailyStockReport()
        {
            DataTable dt = new Stock_DAL().StockTableTemp();

            ViewBag.stocklist = dt;
            return View();
        }

        [HttpPost]
        public FileResult ExportCollectionReport(string from_date)
        {
            from_date = from_date + "-01";
            DataTable dt = new Collection_DAL().CollectionReport(from_date);
            var rows = dt.Rows.Count;
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

        [HttpPost]
        public FileResult ExportDailyStockReport()
        {

            DataTable dt = new Stock_DAL().StockTableTemp();

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Stock Report");
                ws.Cell("A1").Value = "Sl No";
                ws.Range("A1:A2").Merge().Style.Font.SetBold().Font.FontSize = 12;
                ws.Cell("B1").Value = "Segment";
                ws.Range("B1:B2").Merge().Style.Font.SetBold().Font.FontSize = 12;
                ws.Cell("C1").Value = "Model";
                ws.Range("C1:C2").Merge().Style.Font.SetBold().Font.FontSize = 12;
                ws.Cell("D1").Value = "Total Qty";
                ws.Range("D1:D2").Merge().Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("E1").Value = "Dhamrai CKD";
                ws.Range("E1:H1").Merge().Style.Font.SetBold().Font.FontSize = 12;
                ws.Cell("E2").Value = "RFD";
                ws.Cell("E2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("F2").Value = "DO ISSUED";
                ws.Cell("F2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("G2").Value = "Booked";
                ws.Cell("G2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("H2").Value = "PDI/PTS";
                ws.Cell("H2").Style.Font.SetBold().Font.FontSize = 11;

                ws.Cell("i1").Value = "Dhamrai CBU";
                ws.Range("i1:k1").Merge().Style.Font.Bold = true;
                ws.Cell("I2").Value = "RFD";
                ws.Cell("I2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("J2").Value = "DO ISSUED";
                ws.Cell("J2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("K2").Value = "Booked";
                ws.Cell("K2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("L2").Value = "PDI/PTS";
                ws.Cell("L2").Style.Font.SetBold().Font.FontSize = 11;

                ws.Cell("m1").Value = "Joydevpur";
                ws.Range("m1:p1").Merge().Style.Font.SetBold().Font.FontSize = 12;
                ws.Cell("M2").Value = "RFD";
                ws.Cell("M2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("N2").Value = "DO ISSUED";
                ws.Cell("N2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("O2").Value = "Booked";
                ws.Cell("O2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("P2").Value = "PDI/PTS";
                ws.Cell("P2").Style.Font.SetBold().Font.FontSize = 11;

                ws.Cell("Q1").Value = "Chattagram";
                ws.Range("Q1:T1").Merge().Style.Font.SetBold().Font.FontSize = 12;
                ws.Cell("Q2").Value = "RFD";
                ws.Cell("Q2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("R2").Value = "DO ISSUED";
                ws.Cell("R2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("S2").Value = "Booked";
                ws.Cell("S2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("T2").Value = "PDI/PTS";
                ws.Cell("T2").Style.Font.SetBold().Font.FontSize = 11;

                ws.Cell("U1").Value = "Cumilla";
                ws.Range("U1:X1").Merge().Style.Font.SetBold().Font.FontSize = 12;
                ws.Cell("U2").Value = "RFD";
                ws.Cell("U2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("V2").Value = "DO ISSUED";
                ws.Cell("V2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("W2").Value = "Booked";
                ws.Cell("W2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("X2").Value = "PDI/PTS";
                ws.Cell("X2").Style.Font.SetBold().Font.FontSize = 11;

                ws.Cell("Y1").Value = "Jashore";
                ws.Range("Y1:AB1").Merge().Style.Font.SetBold().Font.FontSize = 12;
                ws.Cell("Y2").Value = "RFD";
                ws.Cell("Y2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("Z2").Value = "DO ISSUED";
                ws.Cell("Z2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("AA2").Value = "Booked";
                ws.Cell("AA2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("AB2").Value = "PDI/PTS";
                ws.Cell("AB2").Style.Font.SetBold().Font.FontSize = 11;

                ws.Cell("AC1").Value = "Bogra";
                ws.Range("AC1:AF1").Merge().Style.Font.SetBold().Font.FontSize = 12;
                ws.Cell("AC2").Value = "RFD";
                ws.Cell("AC2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("AD2").Value = "DO ISSUED";
                ws.Cell("AD2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("AE2").Value = "Booked";
                ws.Cell("AE2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("AF2").Value = "PDI/PTS";
                ws.Cell("AF2").Style.Font.SetBold().Font.FontSize = 11;

                ws.Cell("AG1").Value = "Total";
                ws.Range("AG1:AJ1").Merge().Style.Font.SetBold().Font.FontSize = 12;
                ws.Cell("AG2").Value = "RFD";
                ws.Cell("AG2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("AH2").Value = "DO ISSUED";
                ws.Cell("AH2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("AI2").Value = "Booked";
                ws.Cell("AI2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("AJ2").Value = "PDI/PTS";
                ws.Cell("AJ2").Style.Font.SetBold().Font.FontSize = 11;

                ws.Cell("AK1").Value = "Fair And Demo";
                ws.Range("AK1:AK2").Merge().Style.Font.SetBold().Font.FontSize = 12;
                ws.Cell("AL1").Value = "Dealer Point Display";
                ws.Range("AL1:AL2").Merge().Style.Font.SetBold().Font.FontSize = 12;
                ws.Cell("AM1").Value = "Outside Body WS";
                ws.Range("AM1:AM2").Merge().Style.Font.SetBold().Font.FontSize = 12;
                ws.Cell("AN1").Value = "Bus Body";
                ws.Range("AN1:AN2").Merge().Style.Font.SetBold().Font.FontSize = 12;
                ws.Cell("AO1").Value = "Total OT Stock";
                ws.Range("AO1:AO2").Merge().Style.Font.SetBold().Font.FontSize = 12;
                ws.Cell("AP1").Value = "LC";
                ws.Range("AP1:AP2").Merge().Style.Font.SetBold().Font.FontSize = 12;

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ws.Cell("A" + (i + 3)).Value = i + 1;
                        ws.Cell("B" + (i + 3)).Value = dt.Rows[i]["PRODUCT_FAMILY"];
                        ws.Cell("C" + (i + 3)).Value = dt.Rows[i]["PDI_PTS"];
                        ws.Cell("D" + (i + 3)).Value = dt.Rows[i]["TOTAL_QTY"];
                        ws.Cell("E" + (i + 3)).Value = dt.Rows[i]["DHAMRAI_CKD#RFD_CKD"];
                        ws.Cell("F" + (i + 3)).Value = dt.Rows[i]["DHAMRAI_CKD#DO_ISSUED"];
                        ws.Cell("G" + (i + 3)).Value = dt.Rows[i]["DHAMRAI_CKD#BOOKED_DAP"];
                        ws.Cell("H" + (i + 3)).Value = dt.Rows[i]["DHAMRAI_CKD#PDI_PTS"];
                        ws.Cell("I" + (i + 3)).Value = dt.Rows[i]["DHAMRAI_CBU#RFD_CBU"];
                        ws.Cell("J" + (i + 3)).Value = dt.Rows[i]["DHAMRAI_CBU#DO_ISSUED"];
                        ws.Cell("K" + (i + 3)).Value = dt.Rows[i]["DHAMRAI_CBU#BOOKED_DAP"];
                        ws.Cell("L" + (i + 3)).Value = dt.Rows[i]["DHAMRAI_CBU#PDI_PTS"];
                        ws.Cell("M" + (i + 3)).Value = dt.Rows[i]["JOYDEBPUR#RFD"];
                        ws.Cell("N" + (i + 3)).Value = dt.Rows[i]["JOYDEBPUR#DO_ISSUED"];
                        ws.Cell("O" + (i + 3)).Value = dt.Rows[i]["JOYDEBPUR#BOOKED"];
                        ws.Cell("P" + (i + 3)).Value = dt.Rows[i]["JOYDEBPUR#PDI_PTS"];
                        ws.Cell("Q" + (i + 3)).Value = dt.Rows[i]["CTG#RFD"];
                        ws.Cell("R" + (i + 3)).Value = dt.Rows[i]["CTG#DO_ISSUED"];
                        ws.Cell("S" + (i + 3)).Value = dt.Rows[i]["CTG#BOOKED"];
                        ws.Cell("T" + (i + 3)).Value = dt.Rows[i]["CTG#PDI_PTS"];
                        ws.Cell("U" + (i + 3)).Value = dt.Rows[i]["CUMILLA#RFD"];
                        ws.Cell("V" + (i + 3)).Value = dt.Rows[i]["CUMILLA#DO_ISSUED"];
                        ws.Cell("W" + (i + 3)).Value = dt.Rows[i]["CUMILLA#BOOKED"];
                        ws.Cell("X" + (i + 3)).Value = dt.Rows[i]["CUMILLA#PDI_PTS"];
                        ws.Cell("Y" + (i + 3)).Value = dt.Rows[i]["JASHORE#RFD"];
                        ws.Cell("Z" + (i + 3)).Value = dt.Rows[i]["JASHORE#DO_ISSUED"];
                        ws.Cell("AA" + (i + 3)).Value = dt.Rows[i]["JASHORE#BOOKED"];
                        ws.Cell("AB" + (i + 3)).Value = dt.Rows[i]["JASHORE#PDI_PTS"];
                        ws.Cell("AC" + (i + 3)).Value = dt.Rows[i]["BOGRA#RFD"];
                        ws.Cell("AD" + (i + 3)).Value = dt.Rows[i]["BOGRA#DO_ISSUED"];
                        ws.Cell("AE" + (i + 3)).Value = dt.Rows[i]["BOGRA#BOOKED"];
                        ws.Cell("AF" + (i + 3)).Value = dt.Rows[i]["BOGRA#PDI_PTS"];
                        ws.Cell("AG" + (i + 3)).Value = dt.Rows[i]["RFD"];
                        ws.Cell("AH" + (i + 3)).Value = dt.Rows[i]["DO_ISSUED"];
                        ws.Cell("AI" + (i + 3)).Value = dt.Rows[i]["BOOKED"];
                        ws.Cell("AJ" + (i + 3)).Value = dt.Rows[i]["PDI_PTS"];
                        ws.Cell("AK" + (i + 3)).Value = dt.Rows[i]["FAIR_DEMO_QTY"];
                        ws.Cell("AL" + (i + 3)).Value = dt.Rows[i]["DEALER_PD_QTY"];
                        ws.Cell("AM" + (i + 3)).Value = dt.Rows[i]["OUTSIDE_BWS_QTY"];
                        ws.Cell("AN" + (i + 3)).Value = dt.Rows[i]["BUS_BODY_QTY"];
                        ws.Cell("AO" + (i + 3)).Value = dt.Rows[i]["TOTAL_OT_QTY"];
                        ws.Cell("AP" + (i + 3)).Value = dt.Rows[i]["LC_QTY"];
                    }

                }

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }

            }


        }
    }
}