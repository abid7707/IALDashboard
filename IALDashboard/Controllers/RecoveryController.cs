using ClosedXML.Excel;
using IALDashboard.DAL;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Globalization;
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

            DataTable ro_summary = new DataTable();


            DataTable dt = new Collection_DAL().ROSheet(from_date, ro_code, zone_name);

            DateTime formatted_date = DateTime.ParseExact(from_date, "yyyy-MM-dd", null);

            String formatted_from_date = formatted_date.ToString("MMMM, yyyy");


            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (DataRow row in ro_list.Rows)
                {
                    DataRow[] rodt = dt.Select("RO_CODE ='" + row["RO_CODE"] + "'");

                    if (rodt.Length != 0)
                    {
                        ro_summary = new Collection_DAL().ROSummaryByROCode(from_date, row["RO_CODE"].ToString());

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
                        ws.Range("A1:P1").Merge().Style.Font.SetBold().Font.FontSize = 14;

                        ws.Cell("A2").Value = "Month: " + formatted_from_date;
                        ws.Range("A2:P2").Merge().Style.Font.SetBold().Font.FontSize = 12;

                        ws.Cell("A3").Value = "RO CODE: " + row["RO_CODE"].ToString() + "RO Name: " + row["RO_NAME"].ToString() + " ZONE: " + zone_name;
                        ws.Range("A3:P3").Merge().Style.Font.SetBold().Font.FontSize = 12;

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

                        ws.Cell("H5").Value = "OP. Tar Inst Amt.";
                        ws.Range("H5").Style.Font.SetBold().Font.FontSize = 12;

                        ws.Cell("I5").Value = "NO.Ovd";
                        ws.Range("I5").Style.Font.SetBold().Font.FontSize = 12;

                        ws.Cell("J5").Value = "MR COLL";
                        ws.Range("J5").Style.Font.SetBold().Font.FontSize = 12;

                        ws.Cell("K5").Value = "DP+DC Due";
                        ws.Range("K5").Style.Font.SetBold().Font.FontSize = 12;

                        ws.Cell("L5").Value = "INST DC";
                        ws.Range("L5").Style.Font.SetBold().Font.FontSize = 12;

                        ws.Cell("M5").Value = "Op. Ovd";
                        ws.Range("M5").Style.Font.SetBold().Font.FontSize = 12;

                        ws.Cell("N5").Value = "Overdue";
                        ws.Range("N5").Style.Font.SetBold().Font.FontSize = 12;

                        ws.Cell("O5").Value = "Monthly Coll";
                        ws.Range("O5").Style.Font.SetBold().Font.FontSize = 12;

                        ws.Cell("P5").Value = "Atten V";
                        ws.Range("P5").Style.Font.SetBold().Font.FontSize = 12;


                        int i = 0;
                        double SUB_EMI_AMOUNT = 0;
                        double SUB_TAR_INST_AMT = 0;
                        double SUB_NO_OF_OVERDUE = 0;
                        double SUB_MR_COLL = 0;
                        double SUB_DUE_DP_AND_DC = 0;
                        double SUB_INS_DC = 0;
                        double SUB_OVERDUE = 0;
                        double SUB_MONTHLY_COLL = 0;
                        double SUB_ATTEN_V = 0;
                        double SUB_OPENING_OVERDUE = 0;

                        foreach (DataRow record in rodt)
                        {
                            SUB_EMI_AMOUNT += Convert.ToDouble(record["EMI_AMOUNT"]);
                            SUB_TAR_INST_AMT += Convert.ToDouble(record["OP_TAR_INST_AMT"]);
                            SUB_NO_OF_OVERDUE += Convert.ToDouble(record["NO_OF_OVERDUE"]);
                            SUB_MR_COLL += Convert.ToDouble(record["MR_COLL"]);
                            SUB_DUE_DP_AND_DC += Convert.ToDouble(record["DUE_DP_AND_DC"]);
                            SUB_INS_DC += Convert.ToDouble(record["INS_DC"]);
                            SUB_OVERDUE += Convert.ToDouble(record["OVERDUE"]);
                            SUB_MONTHLY_COLL += Convert.ToDouble(record["MONTHLY_COLL"]);
                            SUB_ATTEN_V += Convert.ToDouble(record["ATTEN_V"]);
                            SUB_OPENING_OVERDUE += Convert.ToDouble(record["OPENING_OVERDUE"]);

                            ws.Cell("A" + (i + 6)).Value = i + 1;
                            ws.Cell("B" + (i + 6)).Value = record["ORDER_NO"].ToString();
                            ws.Cell("C" + (i + 6)).Value = record["CUSTOMER_NAME"].ToString();
                            ws.Cell("D" + (i + 6)).Value = record["REGNO"].ToString();
                            ws.Cell("E" + (i + 6)).Value = record["CATALOG_DESC"].ToString();
                            ws.Cell("F" + (i + 6)).Value = record["FSTINSAL_DATE"].ToString();
                            ws.Cell("G" + (i + 6)).Value = record["EMI_AMOUNT"].ToString();
                            ws.Cell("H" + (i + 6)).Value = record["OP_TAR_INST_AMT"].ToString();
                            ws.Cell("I" + (i + 6)).Value = record["NO_OF_OVERDUE"].ToString();
                            ws.Cell("J" + (i + 6)).Value = record["MR_COLL"].ToString();
                            ws.Cell("K" + (i + 6)).Value = record["DUE_DP_AND_DC"].ToString();
                            ws.Cell("L" + (i + 6)).Value = record["INS_DC"].ToString();
                            ws.Cell("M" + (i + 6)).Value = record["OPENING_OVERDUE"].ToString();
                            ws.Cell("N" + (i + 6)).Value = record["OVERDUE"].ToString();
                            ws.Cell("O" + (i + 6)).Value = record["MONTHLY_COLL"].ToString();
                            ws.Cell("P" + (i + 6)).Value = record["ATTEN_V"].ToString();
                            i++;
                        }
                        ws.Cell("F" + (i + 6)).Value = "Sub Total";
                        ws.Cell("G" + (i + 6)).Value = SUB_EMI_AMOUNT.ToString();
                        ws.Cell("H" + (i + 6)).Value = SUB_TAR_INST_AMT.ToString();
                        ws.Cell("I" + (i + 6)).Value = SUB_NO_OF_OVERDUE.ToString();
                        ws.Cell("J" + (i + 6)).Value = SUB_MR_COLL.ToString();
                        ws.Cell("K" + (i + 6)).Value = SUB_DUE_DP_AND_DC.ToString();
                        ws.Cell("L" + (i + 6)).Value = SUB_INS_DC.ToString();
                        ws.Cell("M" + (i + 6)).Value = SUB_OPENING_OVERDUE.ToString();
                        ws.Cell("N" + (i + 6)).Value = SUB_OVERDUE.ToString();
                        ws.Cell("O" + (i + 6)).Value = SUB_MONTHLY_COLL.ToString();
                        ws.Cell("P" + (i + 6)).Value = SUB_ATTEN_V.ToString();


                        ws.Range("F" + (i + 6) + ":P" + (i + 6)).Style.Font.SetBold().Font.FontSize = 11;

                        ws.Range("F" + (i + 9) + ":P" + (i + 9)).Style.Font.SetBold().Font.FontSize = 11;

                        ws.Cell("G" + (i + 9)).Value = "Inst Coll";
                        ws.Cell("H" + (i + 9)).Value = "Ovd. COll";
                        ws.Cell("I" + (i + 9)).Value = "Excess Coll";
                        ws.Cell("J" + (i + 9)).Value = "No. Veh";
                        ws.Cell("K" + (i + 9)).Value = "Atten. Veh";
                        ws.Cell("L" + (i + 9)).Value = "T. Coll%";
                        ws.Cell("M" + (i + 9)).Value = "Inst. Coll";
                        ws.Cell("N" + (i + 9)).Value = "Ovd Coll%";
                        ws.Cell("O" + (i + 9)).Value = "Excess Coll%";
                        ws.Cell("P" + (i + 9)).Value = "Atten%";

                        ws.Cell("G" + (i + 10)).Value = ro_summary.Rows[0]["INST_COLL"];
                        ws.Cell("H" + (i + 10)).Value = ro_summary.Rows[0]["OD_COLECTION"];
                        ws.Cell("I" + (i + 10)).Value = ro_summary.Rows[0]["EXCESS_COLLECTION"];
                        ws.Cell("J" + (i + 10)).Value = ro_summary.Rows[0]["NO_OF_VEHICLE"];
                        ws.Cell("K" + (i + 10)).Value = ro_summary.Rows[0]["ATTEN_V"];
                        ws.Cell("L" + (i + 10)).Value = ro_summary.Rows[0]["TAR_COLL_PERCENT"];
                        ws.Cell("M" + (i + 10)).Value = ro_summary.Rows[0]["INST_COLL_PERCENT"];
                        ws.Cell("N" + (i + 10)).Value = ro_summary.Rows[0]["OVERDUE_COLL_PERCENT"];
                        ws.Cell("O" + (i + 10)).Value = ro_summary.Rows[0]["EXCESS_COLLECTION_PERCENT"];
                        ws.Cell("P" + (i + 10)).Value = ro_summary.Rows[0]["ATTEN_V_PERCENT"];


                        ws.Range("A5" + ":P" + (i + 6)).Style.Alignment.SetWrapText(true);

                        ws.Range("A5" + ":P" + (i + 6)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        ws.Range("A5" + ":P" + (i + 6)).Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                        ws.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                        ws.PageSetup.AdjustTo(75);
                        ws.PageSetup.SetRowsToRepeatAtTop(1, 5);
                    }/*if (rodt.Length != 0)*/

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




        [HttpPost]
        public ActionResult ExportROSummary(string from_date, string zone_name)
        {
            string date = from_date + "-01";

            DateTime formatted_date = DateTime.ParseExact(date, "yyyy-MM-dd", null);

            String formatted_from_date = formatted_date.ToString("MMMM, yyyy");

            DataTable dt = new Collection_DAL().ROSummary(date, zone_name);


            using (XLWorkbook wb = new XLWorkbook())
            {

                var ws = wb.Worksheets.Add(from_date);

                ws.Column("A").Width = 5;
                ws.Column("B").Width = 8;
                ws.Column("C").Width = 12;
                ws.Column("E").Width = 16;
                ws.Column("F").Width = 10;
                ws.Column("J").Width = 12;
                ws.Column("M").Width = 12;

                ws.Range("A1:Q5").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                ws.Cell("A1").Value = "Ifad Autos Ltd (RO Summary)";
                ws.Range("A1:Q1").Merge().Style.Font.SetBold().Font.FontSize = 14;

                ws.Cell("A2").Value = "Collection for the Month of " + formatted_from_date;
                ws.Range("A2:Q2").Merge().Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("A5").Value = "SL NO";
                ws.Range("A5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("B5").Value = "Zone";
                ws.Range("B5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("C5").Value = "RO Code";
                ws.Range("C5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("D5").Value = "RO Name";
                ws.Range("D5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("E5").Value = "OP.Tar Inst Amt.";
                ws.Range("E5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("F5").Value = "A. OP.OverDue";
                ws.Range("F5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("G5").Value = "M. Collection";
                ws.Range("G5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("H5").Value = "Inst. Coll.";
                ws.Range("H5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("I5").Value = "Ovd.COll.";
                ws.Range("I5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("J5").Value = "Excess Coll.";
                ws.Range("J5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("K5").Value = "No. Veh.";
                ws.Range("K5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("L5").Value = "Att. Veh.";
                ws.Range("L5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("M5").Value = "T. Coll%";
                ws.Range("M5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("N5").Value = "Inst. Coll%";
                ws.Range("N5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("O5").Value = "Ovd. Coll%";
                ws.Range("O5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("P5").Value = "Excess Coll%";
                ws.Range("P5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("Q5").Value = "Atten%";
                ws.Range("Q5").Style.Font.SetBold().Font.FontSize = 12;


                int i = 0;
                double SUB_TAR_INST_AMT = 0;
                double SUB_ACTUAL_OD = 0;
                double SUB_MONTHLY_COLL = 0;
                double SUB_INST_COLL = 0;
                double SUB_OD_COLLECTION = 0;
                double SUB_EXCESS_COLLECTION = 0;
                double SUB_NO_OF_VEHICLE = 0;
                double SUB_ATTEN_V = 0;
                double AVG_INST_COLL_PERCENT = 0;
                double AVG_OVERDUE_COLL_PERCENT = 0;
                double AVG_ATTEN_V_PERCENT = 0;
                double SUB_EXCESS_COLLECTION_PERCENT = 0;
                double AVG_TAR_COLL_PERCENT = 0;

                DataTable ZoneList = new Collection_DAL().ZoneInfo();

                var ti = new CultureInfo("en-US", false).TextInfo;

                foreach (DataRow zone in ZoneList.Rows)
                {
                    DataRow[] rs = dt.Select("ZONE_NAME = '" + zone["ZONE_NAME"].ToString() + "'");

                    if (rs.Length != 0)
                    {
                        foreach (DataRow record in rs)
                        {
                            SUB_TAR_INST_AMT += Convert.ToDouble(record["OP_TAR_INST_AMT"]);
                            SUB_ACTUAL_OD += Convert.ToDouble(record["ACTUAL_OPENING_OVERDUE"]);
                            SUB_MONTHLY_COLL += Convert.ToDouble(record["MONTHLY_COLL"]);
                            SUB_INST_COLL += Convert.ToDouble(record["INST_COLL"]);
                            SUB_OD_COLLECTION += Convert.ToDouble(record["OD_COLECTION"]);
                            SUB_EXCESS_COLLECTION += Convert.ToDouble(record["EXCESS_COLLECTION"]);
                            SUB_NO_OF_VEHICLE += Convert.ToDouble(record["NO_OF_VEHICLE"]);
                            SUB_ATTEN_V += Convert.ToDouble(record["ATTEN_V"]);

                            ws.Cell("A" + (i + 6)).Value = i + 1;
                            ws.Cell("B" + (i + 6)).Value = record["ZONE_NAME"].ToString();
                            ws.Cell("C" + (i + 6)).Value = record["RO_CODE"].ToString();
                            ws.Cell("D" + (i + 6)).Value = ti.ToTitleCase(record["RO_NAME"].ToString().ToLower());
                            ws.Cell("E" + (i + 6)).Value = record["OP_TAR_INST_AMT"].ToString();
                            ws.Cell("F" + (i + 6)).Value = record["ACTUAL_OPENING_OVERDUE"].ToString();
                            ws.Cell("G" + (i + 6)).Value = record["MONTHLY_COLL"].ToString();
                            ws.Cell("H" + (i + 6)).Value = record["INST_COLL"].ToString();
                            ws.Cell("I" + (i + 6)).Value = record["OD_COLECTION"].ToString();
                            ws.Cell("J" + (i + 6)).Value = record["EXCESS_COLLECTION"].ToString();
                            ws.Cell("K" + (i + 6)).Value = record["NO_OF_VEHICLE"].ToString();
                            ws.Cell("L" + (i + 6)).Value = record["ATTEN_V"].ToString();
                            ws.Cell("M" + (i + 6)).Value = record["TAR_COLL_PERCENT"].ToString();
                            ws.Cell("N" + (i + 6)).Value = record["INST_COLL_PERCENT"].ToString();
                            ws.Cell("O" + (i + 6)).Value = record["OVERDUE_COLL_PERCENT"].ToString();
                            ws.Cell("P" + (i + 6)).Value = record["EXCESS_COLLECTION_PERCENT"].ToString();
                            ws.Cell("Q" + (i + 6)).Value = record["ATTEN_V_PERCENT"].ToString();
                            i++;
                        }
                        AVG_TAR_COLL_PERCENT = Math.Round(SUB_MONTHLY_COLL / SUB_TAR_INST_AMT * 100, 0);
                        AVG_INST_COLL_PERCENT = Math.Round(SUB_INST_COLL / SUB_TAR_INST_AMT * 100, 0);
                        AVG_OVERDUE_COLL_PERCENT = Math.Round(SUB_OD_COLLECTION / SUB_ACTUAL_OD * 100, 0);
                        SUB_EXCESS_COLLECTION_PERCENT = Math.Round(SUB_EXCESS_COLLECTION / SUB_TAR_INST_AMT * 100, 0);
                        AVG_ATTEN_V_PERCENT = Math.Round(SUB_ATTEN_V / SUB_NO_OF_VEHICLE * 100, 0);

                        ws.Cell("D" + (i + 6)).Value = "Sub Total";
                        ws.Cell("E" + (i + 6)).Value = SUB_TAR_INST_AMT.ToString();
                        ws.Cell("F" + (i + 6)).Value = SUB_ACTUAL_OD.ToString();
                        ws.Cell("G" + (i + 6)).Value = SUB_MONTHLY_COLL.ToString();
                        ws.Cell("H" + (i + 6)).Value = SUB_INST_COLL.ToString();
                        ws.Cell("I" + (i + 6)).Value = SUB_OD_COLLECTION.ToString();
                        ws.Cell("J" + (i + 6)).Value = SUB_EXCESS_COLLECTION.ToString();
                        ws.Cell("K" + (i + 6)).Value = SUB_NO_OF_VEHICLE.ToString();
                        ws.Cell("L" + (i + 6)).Value = SUB_ATTEN_V.ToString();
                        ws.Cell("M" + (i + 6)).Value = AVG_TAR_COLL_PERCENT.ToString();
                        ws.Cell("N" + (i + 6)).Value = AVG_INST_COLL_PERCENT.ToString();
                        ws.Cell("O" + (i + 6)).Value = AVG_OVERDUE_COLL_PERCENT.ToString();
                        ws.Cell("P" + (i + 6)).Value = SUB_EXCESS_COLLECTION_PERCENT.ToString();
                        ws.Cell("Q" + (i + 6)).Value = AVG_ATTEN_V_PERCENT.ToString();
                        ws.Range("D" + (i + 6) + ":Q" + (i + 6)).Style.Font.SetBold().Font.FontSize = 11;
                        i++;

                    }/*if (rs.Length != 0)*/

                }


                ws.Range("A5" + ":Q" + (i + 5)).Style.Alignment.SetWrapText(true);

                ws.Range("A5" + ":Q" + (i + 5)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Range("A5" + ":Q" + (i + 5)).Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                ws.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                ws.PageSetup.AdjustTo(75);
                ws.PageSetup.SetRowsToRepeatAtTop(1, 5);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ROSummary-" + zone_name + ".xlsx");
                }

            }


        }


        //-------------------------------RO ZONE WISE SUMMARY-------------------------------


        public ActionResult RoZoneWiseSummary()
        {
            ViewBag.actionName = "Zone Wise Summary";
            return View();
        }

        [HttpPost]
        public ActionResult ExportROZoneWiseSummary(string from_date, string contract)
        {
            string date = from_date + "-01";

            DateTime formatted_date = DateTime.ParseExact(date, "yyyy-MM-dd", null);

            String formatted_from_date = formatted_date.ToString("MMMM, yyyy");

            DataTable dt = new Collection_DAL().RoZoneWiseSummary(date, contract);


            using (XLWorkbook wb = new XLWorkbook())
            {

                var ws = wb.Worksheets.Add(from_date);

                ws.Column("A").Width = 5;
                ws.Column("B").Width = 8;
                ws.Column("C").Width = 12;
                ws.Column("E").Width = 16;
                ws.Column("F").Width = 10;
                ws.Column("J").Width = 12;
                ws.Column("M").Width = 12;

                ws.Range("A1:O5").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                ws.Cell("A1").Value = "Ifad Autos Ltd (Zone wise Summary) - " + contract;
                ws.Range("A1:O1").Merge().Style.Font.SetBold().Font.FontSize = 14;

                ws.Cell("A2").Value = "Collection for the Month of " + formatted_from_date;
                ws.Range("A2:O2").Merge().Style.Font.SetBold().Font.FontSize = 12;





                ws.Cell("A5").Value = "SL NO";
                ws.Range("A5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("B5").Value = "Zone";
                ws.Range("B5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("C5").Value = "Tar Inst Amt.";
                ws.Range("C5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("D5").Value = "Opening AOD";
                ws.Range("D5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("E5").Value = "Monthly  Collection";
                ws.Range("E5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("F5").Value = "Inst. Coll";
                ws.Range("F5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("G5").Value = "Ovd Coll";
                ws.Range("G5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("H5").Value = "Excess Coll";
                ws.Range("H5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("I5").Value = "No.Veh";
                ws.Range("I5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("J5").Value = "Att. Veh";
                ws.Range("J5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("K5").Value = "G.Coll%";
                ws.Range("K5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("L5").Value = "Inst.Coll %";
                ws.Range("L5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("M5").Value = "Ovd Coll%";
                ws.Range("M5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("N5").Value = "Excess Coll%";
                ws.Range("N5").Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("O5").Value = "Atten %";
                ws.Range("O5").Style.Font.SetBold().Font.FontSize = 12;

                int i = 0;
                int j = 1;
                
              

                DataTable ZoneList = new Collection_DAL().ZoneInfo();

                int zone_group_start = 5;

                DataTable zone_group_dt = new DataTable("zone_group_table");
                zone_group_dt.Columns.Add("ZONE_GROUP", typeof(string));
             

               DataTable dtZoneGroup=   dt.DefaultView.ToTable(true, "ZONE_GROUP");




                foreach (DataRow zone in dtZoneGroup.Rows)
                {
                     DataRow[] rs = dt.Select("ZONE_GROUP = '" + zone["ZONE_GROUP"].ToString() + "'");

                    double SUB_TAR_INST_AMT = 0;
                    double SUB_ACTUAL_OD = 0;
                    double SUB_MONTHLY_COLL = 0;
                    double SUB_INST_COLL = 0;
                    double SUB_OD_COLLECTION = 0;
                    double SUB_EXCESS_COLLECTION = 0;
                    double SUB_NO_OF_VEHICLE = 0;
                    double SUB_ATTEN_V = 0;
                    double G_COLL_PERCENT = 0;
                    double AVG_INST_COLL_PERCENT = 0;
                    double AVG_OVERDUE_COLL_PERCENT = 0;
                    double AVG_ATTEN_V_PERCENT = 0;
                    double AVG_EXCESS_COLLECTION_PERCENT = 0;
                    double AVG_ATTN_V_PERCENT = 0;

                    if (rs.Length != 0)
                    {
                        foreach (DataRow record in rs)
                        {
                             SUB_TAR_INST_AMT += Convert.ToDouble(record["TAR_INST_AMT"]);
                             SUB_ACTUAL_OD += Convert.ToDouble(record["ACTUAL_OD"]);
                             SUB_MONTHLY_COLL += Convert.ToDouble(record["MONTHLY_COLL"]);
                             SUB_INST_COLL += Convert.ToDouble(record["INST_COLL"]);
                             SUB_OD_COLLECTION += Convert.ToDouble(record["OD_COLECTION"]);
                             SUB_EXCESS_COLLECTION += Convert.ToDouble(record["EXCESS_COLLECTION"]);
                             SUB_NO_OF_VEHICLE += Convert.ToDouble(record["NO_OF_VEHICLE"]);
                             SUB_ATTEN_V += Convert.ToDouble(record["ATTEN_V"]);
                            /*SUB_TAR_COLL_PERCENT += Convert.ToDouble(record["TAR_COLL_PERCENT"]);
                            SUB_INST_COLL_PERCENT += Convert.ToDouble(record["INST_COLL_PERCENT"]);
                            AVG_INST_COLL_PERCENT = SUB_INST_COLL_PERCENT / (i + 1);*/
                            G_COLL_PERCENT= Math.Round(SUB_MONTHLY_COLL / SUB_TAR_INST_AMT * 100, 0);
                            AVG_INST_COLL_PERCENT = Math.Round(SUB_INST_COLL / SUB_TAR_INST_AMT * 100, 0);
                             AVG_OVERDUE_COLL_PERCENT = Math.Round(SUB_OD_COLLECTION / SUB_ACTUAL_OD * 100, 0);
                             AVG_ATTEN_V_PERCENT = Math.Round(SUB_ATTEN_V / SUB_NO_OF_VEHICLE * 100, 0);
                            AVG_EXCESS_COLLECTION_PERCENT = Math.Round(SUB_EXCESS_COLLECTION / SUB_TAR_INST_AMT * 100, 0);
                            AVG_ATTN_V_PERCENT= Math.Round(SUB_ATTEN_V / SUB_NO_OF_VEHICLE * 100, 0);


                            ws.Cell("A" + (i + 6)).Value =j;
                            ws.Cell("B" + (i + 6)).Value = record["ZONE_NAME"].ToString();
                            ws.Cell("C" + (i + 6)).Value = record["TAR_INST_AMT"].ToString();
                            ws.Cell("D" + (i + 6)).Value = record["ACTUAL_OD"].ToString();
                            ws.Cell("E" + (i + 6)).Value = record["MONTHLY_COLL"].ToString();
                            ws.Cell("F" + (i + 6)).Value = record["INST_COLL"].ToString();
                            ws.Cell("G" + (i + 6)).Value = record["OD_COLECTION"].ToString();
                            ws.Cell("H" + (i + 6)).Value = record["EXCESS_COLLECTION"].ToString();
                            ws.Cell("I" + (i + 6)).Value = record["NO_OF_VEHICLE"].ToString();
                            ws.Cell("J" + (i + 6)).Value = record["ATTEN_V"].ToString();
                            ws.Cell("K" + (i + 6)).Value = record["GROUP_COLL_PERCENT"].ToString();
                            ws.Cell("L" + (i + 6)).Value = record["INST_COLL_PERCENT"].ToString();
                            ws.Cell("M" + (i + 6)).Value = record["OD_COLLECTION_PERCENT"].ToString();
                            ws.Cell("N" + (i + 6)).Value = record["EXCESS_COLL_PERCENT"].ToString();
                            ws.Cell("O" + (i + 6)).Value = record["ATTN_V_PERCENT"].ToString();

                            i++;
                            j++;
                        }

                        ws.Cell("B" + (i + 6)).Value = zone["ZONE_GROUP"].ToString()+ " Total";
                        ws.Cell("C" + (i + 6)).Value = SUB_TAR_INST_AMT.ToString();
                        ws.Cell("D" + (i + 6)).Value = SUB_ACTUAL_OD.ToString();
                        ws.Cell("E" + (i + 6)).Value = SUB_MONTHLY_COLL.ToString();
                        ws.Cell("F" + (i + 6)).Value = SUB_INST_COLL.ToString();
                        ws.Cell("G" + (i + 6)).Value = SUB_OD_COLLECTION.ToString();
                        ws.Cell("H" + (i + 6)).Value = SUB_EXCESS_COLLECTION.ToString();
                        ws.Cell("I" + (i + 6)).Value = SUB_NO_OF_VEHICLE.ToString();
                        ws.Cell("J" + (i + 6)).Value = SUB_ATTEN_V.ToString();
                        ws.Cell("K" + (i + 6)).Value = G_COLL_PERCENT.ToString();
                        ws.Cell("L" + (i + 6)).Value =  AVG_INST_COLL_PERCENT.ToString();
                        ws.Cell("M" + (i + 6)).Value = AVG_OVERDUE_COLL_PERCENT.ToString();
                        ws.Cell("N" + (i + 6)).Value = AVG_EXCESS_COLLECTION_PERCENT.ToString();
                        ws.Cell("O" + (i + 6)).Value = AVG_ATTN_V_PERCENT.ToString();
                        
                        ws.Range("A" + (i + 6) + ":O" + (i + 6)).
                            Style.Font.SetBold().Font.FontSize = 12;


                        ws.Range("A" +(zone_group_start) + ":O" + (i + 6)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        ws.Range("A" + (zone_group_start)+":O" + (i + 6)).Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                       

                        i++;
                        i++;
                        zone_group_start = i + 6;


                    }/*if (rs.Length != 0)*/

                }






                ws.Range("A5" + ":O" + (i + 6)).Style.Alignment.SetWrapText(true);

               

                ws.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                ws.PageSetup.AdjustTo(75);
                ws.PageSetup.SetRowsToRepeatAtTop(1, 5);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",  " ZoneWiseSummary-"+ contract+ " " + formatted_from_date + ".xlsx");
                }

            }


        }

        [HttpPost]
        public string GetJsonRoZoneWiseSummary(string from_date, string contract)
        {
            from_date = from_date + "-01";

            string[] new_from_date = from_date.Split('-');

            string year = new_from_date[0];
            string month = new_from_date[1];

            DataTable dt = new Collection_DAL().RoZoneWiseSummary(from_date, contract);

            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }



        //[HttpPost]
        //public ActionResult ExportROZoneWiseSummary(string from_date, string contract)
        //{
        //    string date = from_date + "-01";

        //    DateTime formatted_date = DateTime.ParseExact(date, "yyyy-MM-dd", null);

        //    String formatted_from_date = formatted_date.ToString("MMMM, yyyy");

        //    DataTable dt = new Collection_DAL().RoZoneWiseSummary(date, contract);


        //    using (XLWorkbook wb = new XLWorkbook())
        //    {

        //        var ws = wb.Worksheets.Add(from_date);

        //        ws.Column("A").Width = 5;
        //        ws.Column("B").Width = 8;
        //        ws.Column("C").Width = 12;
        //        ws.Column("E").Width = 16;
        //        ws.Column("F").Width = 10;
        //        ws.Column("J").Width = 12;
        //        ws.Column("M").Width = 12;

        //        ws.Range("A1:Q5").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //        ws.Cell("A1").Value = "Ifad Autos Ltd (Zone wise Summary) - "+ contract;
        //        ws.Range("A1:Q1").Merge().Style.Font.SetBold().Font.FontSize = 14;

        //        ws.Cell("A2").Value = "Collection for the Month of " + formatted_from_date;
        //        ws.Range("A2:Q2").Merge().Style.Font.SetBold().Font.FontSize = 12;





        //        ws.Cell("A5").Value = "SL NO";
        //        ws.Range("A5").Style.Font.SetBold().Font.FontSize = 12;

        //        ws.Cell("B5").Value = "Zone";
        //        ws.Range("B5").Style.Font.SetBold().Font.FontSize = 12;

        //        ws.Cell("C5").Value = "Tar Inst Amt.";
        //        ws.Range("C5").Style.Font.SetBold().Font.FontSize = 12;

        //        ws.Cell("D5").Value = "A.OverDue Amt";
        //        ws.Range("D5").Style.Font.SetBold().Font.FontSize = 12;

        //        ws.Cell("E5").Value = "Monthly  Collection";
        //        ws.Range("E5").Style.Font.SetBold().Font.FontSize = 12;

        //        ws.Cell("F5").Value = "Inst. Coll";
        //        ws.Range("F5").Style.Font.SetBold().Font.FontSize = 12;

        //        ws.Cell("G5").Value = "Ovd Coll";
        //        ws.Range("G5").Style.Font.SetBold().Font.FontSize = 12;

        //        ws.Cell("H5").Value = "Excess Coll";
        //        ws.Range("H5").Style.Font.SetBold().Font.FontSize = 12;

        //        ws.Cell("I5").Value = "No.Veh";
        //        ws.Range("I5").Style.Font.SetBold().Font.FontSize = 12;

        //        ws.Cell("J5").Value = "Att. Veh";
        //        ws.Range("J5").Style.Font.SetBold().Font.FontSize = 12;

        //        ws.Cell("K5").Value = "G.Coll%";
        //        ws.Range("K5").Style.Font.SetBold().Font.FontSize = 12;

        //        ws.Cell("L5").Value = "Inst.Coll %";
        //        ws.Range("L5").Style.Font.SetBold().Font.FontSize = 12;

        //        ws.Cell("M5").Value = "Ovd Coll%";
        //        ws.Range("M5").Style.Font.SetBold().Font.FontSize = 12;

        //        ws.Cell("N5").Value = "Excess Coll%";
        //        ws.Range("N5").Style.Font.SetBold().Font.FontSize = 12;

        //        ws.Cell("O5").Value = "Atten %";
        //        ws.Range("O5").Style.Font.SetBold().Font.FontSize = 12;

        //        int i = 0;
        //        /*double SUB_TAR_INST_AMT = 0;
        //        double SUB_ACTUAL_OD = 0;
        //        double SUB_MONTHLY_COLL = 0;
        //        double SUB_INST_COLL = 0;
        //        double SUB_OD_COLLECTION = 0;
        //        double SUB_EXCESS_COLLECTION = 0;
        //        double SUB_NO_OF_VEHICLE = 0;
        //        double SUB_ATTEN_V = 0;
        //        double SUB_TAR_COLL_PERCENT = 0;
        //        double SUB_INST_COLL_PERCENT = 0;
        //        double AVG_INST_COLL_PERCENT = 0;
        //        double AVG_OVERDUE_COLL_PERCENT = 0;
        //        double AVG_ATTEN_V_PERCENT = 0;*/

        //        DataTable ZoneList = new Collection_DAL().ZoneInfo();

        //        string zone_group = ""; 

        //        foreach (DataRow zone in ZoneList.Rows)
        //        {
        //          //  DataRow[] rs = dt.Select("ZONE_NAME = '" + zone["ZONE_NAME"].ToString() + "'");
        //            DataRow[] rs = dt.Select();

        //            if (rs.Length != 0)
        //            {
        //                foreach (DataRow record in rs)
        //                {
        //                    /* SUB_TAR_INST_AMT += Convert.ToDouble(record["TAR_INST_AMT"]);
        //                     SUB_ACTUAL_OD += Convert.ToDouble(record["ACTUAL_OD"]);
        //                     SUB_MONTHLY_COLL += Convert.ToDouble(record["MONTHLY_COLL"]);
        //                     SUB_INST_COLL += Convert.ToDouble(record["INST_COLL"]);
        //                     SUB_OD_COLLECTION += Convert.ToDouble(record["OD_COLECTION"]);
        //                     SUB_EXCESS_COLLECTION += Convert.ToDouble(record["EXCESS_COLLECTION"]);
        //                     SUB_NO_OF_VEHICLE += Convert.ToDouble(record["NO_OF_VEHICLE"]);
        //                     SUB_ATTEN_V += Convert.ToDouble(record["ATTEN_V"]);
        //                     SUB_TAR_COLL_PERCENT += Convert.ToDouble(record["TAR_COLL_PERCENT"]);
        //                     *//*SUB_INST_COLL_PERCENT += Convert.ToDouble(record["INST_COLL_PERCENT"]);
        //                     AVG_INST_COLL_PERCENT = SUB_INST_COLL_PERCENT / (i + 1);*//*
        //                     AVG_INST_COLL_PERCENT = Math.Round(SUB_INST_COLL / SUB_TAR_INST_AMT * 100, 0);
        //                     AVG_OVERDUE_COLL_PERCENT = Math.Round(SUB_OD_COLLECTION / SUB_ACTUAL_OD * 100, 0);
        //                     AVG_ATTEN_V_PERCENT = Math.Round(SUB_ATTEN_V / SUB_NO_OF_VEHICLE * 100, 0);
        //                    */
        //                    ws.Cell("A" + (i + 6)).Value = i + 1;
        //                    ws.Cell("B" + (i + 6)).Value = record["ZONE_NAME"].ToString();
        //                    ws.Cell("C" + (i + 6)).Value = record["TAR_INST_AMT"].ToString();
        //                    ws.Cell("D" + (i + 6)).Value = record["ACTUAL_OD"].ToString();
        //                    ws.Cell("E" + (i + 6)).Value = record["MONTHLY_COLL"].ToString();
        //                    ws.Cell("F" + (i + 6)).Value = record["INST_COLL"].ToString();
        //                    ws.Cell("G" + (i + 6)).Value = record["OD_COLECTION"].ToString();
        //                    ws.Cell("H" + (i + 6)).Value = record["EXCESS_COLLECTION"].ToString();
        //                    ws.Cell("I" + (i + 6)).Value = record["NO_OF_VEHICLE"].ToString();
        //                    ws.Cell("J" + (i + 6)).Value = record["ATTEN_V"].ToString();
        //                    ws.Cell("K" + (i + 6)).Value = record["GROUP_COLL_PERCENT"].ToString();
        //                    ws.Cell("L" + (i + 6)).Value = record["INST_COLL_PERCENT"].ToString();
        //                    ws.Cell("M" + (i + 6)).Value = record["OD_COLLECTION_PERCENT"].ToString();
        //                    ws.Cell("N" + (i + 6)).Value = record["EXCESS_COLL_PERCENT"].ToString();
        //                    ws.Cell("O" + (i + 6)).Value = record["ATTN_V_PERCENT"].ToString();

        //                    i++;
        //                }

        //                /*ws.Cell("D" + (i + 6)).Value = "Sub Total";
        //                ws.Cell("E" + (i + 6)).Value = SUB_TAR_INST_AMT.ToString();
        //                ws.Cell("F" + (i + 6)).Value = SUB_ACTUAL_OD.ToString();
        //                ws.Cell("G" + (i + 6)).Value = SUB_MONTHLY_COLL.ToString();
        //                ws.Cell("H" + (i + 6)).Value = SUB_INST_COLL.ToString();
        //                ws.Cell("I" + (i + 6)).Value = SUB_OD_COLLECTION.ToString();
        //                ws.Cell("J" + (i + 6)).Value = SUB_EXCESS_COLLECTION.ToString();
        //                ws.Cell("K" + (i + 6)).Value = SUB_NO_OF_VEHICLE.ToString();
        //                ws.Cell("L" + (i + 6)).Value = SUB_ATTEN_V.ToString();
        //                ws.Cell("M" + (i + 6)).Value = SUB_TAR_COLL_PERCENT.ToString();
        //                ws.Cell("N" + (i + 6)).Value = AVG_INST_COLL_PERCENT.ToString();
        //                ws.Cell("O" + (i + 6)).Value = AVG_OVERDUE_COLL_PERCENT.ToString();
        //                ws.Cell("Q" + (i + 6)).Value = AVG_ATTEN_V_PERCENT.ToString();
        //                ws.Range("D" + (i + 6) + ":Q" + (i + 6)).Style.Font.SetBold().Font.FontSize = 12;
        //                i++;*/

        //            }/*if (rs.Length != 0)*/

        //        }






        //        ws.Range("A5" + ":Q" + (i + 6)).Style.Alignment.SetWrapText(true);

        //        ws.Range("A5" + ":Q" + (i + 6)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        ws.Range("A5" + ":Q" + (i + 6)).Style.Border.InsideBorder = XLBorderStyleValues.Thin;

        //        ws.PageSetup.PageOrientation = XLPageOrientation.Landscape;
        //        ws.PageSetup.AdjustTo(75);
        //        ws.PageSetup.SetRowsToRepeatAtTop(1, 5);

        //        using (MemoryStream stream = new MemoryStream())
        //        {
        //            wb.SaveAs(stream);
        //            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ROZoneWiseSummary-" + formatted_from_date + ".xlsx");
        //        }

        //    }


        //}

        //[HttpPost]
        //public string GetJsonRoZoneWiseSummary(string from_date, string contract)
        //{
        //    from_date = from_date + "-01";

        //    string[] new_from_date = from_date.Split('-');

        //    string year = new_from_date[0];
        //    string month = new_from_date[1];

        //    DataTable dt = new Collection_DAL().RoZoneWiseSummary(from_date, contract);

        //    string JSONString = string.Empty;
        //    JSONString = JsonConvert.SerializeObject(dt);
        //    return JSONString;
        //}



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
                ws.Table(0).ShowAutoFilter = false;
                ws.Table(0).Theme = XLTableTheme.None;
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