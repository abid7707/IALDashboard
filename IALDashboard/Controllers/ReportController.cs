using ClosedXML.Excel;
using IALDashboard.DAL;
using System.Data;
using System.IO;
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

        [HttpPost]
        public FileResult ExportDailyStockReport()
        {

            DataTable dt = new Stock_DAL().StockTableTemp();
            /*using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }*/
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