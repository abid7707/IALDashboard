using ClosedXML.Excel;
using IALDashboard.DAL;
using System;
using System.Data;
using System.IO;
using System.Web.Mvc;

namespace IALDashboard.Controllers
{
    [Filters.AuthorizedUser]
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult DailyStockReport()
        {
            DataTable dt = new Stock_DAL().DailyStockProcessData();

            ViewBag.stocklist = dt;
            return View();
        }




        [HttpPost]
        public FileResult ExportDailyStockReport()
        {

            DataTable dt = new Stock_DAL().DailyStockProcessData();

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
                ws.Range("E1:H1").Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Font.SetBold().Font.FontSize = 12;
                ws.Cell("E2").Value = "RFD";
                ws.Cell("E2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("F2").Value = "DO ISSUED";
                ws.Cell("F2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("G2").Value = "Booked";
                ws.Cell("G2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("H2").Value = "PDI/PTS";
                ws.Cell("H2").Style.Font.SetBold().Font.FontSize = 11;

                ws.Cell("I1").Value = "Dhamrai CBU";
                ws.Range("I1:L1").Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Font.Bold = true;
                ws.Cell("I2").Value = "RFD";
                ws.Cell("I2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("J2").Value = "DO ISSUED";
                ws.Cell("J2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("K2").Value = "Booked";
                ws.Cell("K2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("L2").Value = "PDI/PTS";
                ws.Cell("L2").Style.Font.SetBold().Font.FontSize = 11;

                ws.Cell("m1").Value = "Joydevpur";
                ws.Range("m1:p1").Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Font.SetBold().Font.FontSize = 12;
                ws.Cell("M2").Value = "RFD";
                ws.Cell("M2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("N2").Value = "DO ISSUED";
                ws.Cell("N2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("O2").Value = "Booked";
                ws.Cell("O2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("P2").Value = "PDI/PTS";
                ws.Cell("P2").Style.Font.SetBold().Font.FontSize = 11;

                ws.Cell("Q1").Value = "Chattagram";
                ws.Range("Q1:T1").Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Font.SetBold().Font.FontSize = 12;
                ws.Cell("Q2").Value = "RFD";
                ws.Cell("Q2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("R2").Value = "DO ISSUED";
                ws.Cell("R2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("S2").Value = "Booked";
                ws.Cell("S2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("T2").Value = "PDI/PTS";
                ws.Cell("T2").Style.Font.SetBold().Font.FontSize = 11;

                ws.Cell("U1").Value = "Cumilla";
                ws.Range("U1:X1").Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Font.SetBold().Font.FontSize = 12;
                ws.Cell("U2").Value = "RFD";
                ws.Cell("U2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("V2").Value = "DO ISSUED";
                ws.Cell("V2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("W2").Value = "Booked";
                ws.Cell("W2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("X2").Value = "PDI/PTS";
                ws.Cell("X2").Style.Font.SetBold().Font.FontSize = 11;

                ws.Cell("Y1").Value = "Jashore";
                ws.Range("Y1:AB1").Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Font.SetBold().Font.FontSize = 12;
                ws.Cell("Y2").Value = "RFD";
                ws.Cell("Y2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("Z2").Value = "DO ISSUED";
                ws.Cell("Z2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("AA2").Value = "Booked";
                ws.Cell("AA2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("AB2").Value = "PDI/PTS";
                ws.Cell("AB2").Style.Font.SetBold().Font.FontSize = 11;

                ws.Cell("AC1").Value = "Bogra";
                ws.Range("AC1:AF1").Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Font.SetBold().Font.FontSize = 12;
                ws.Cell("AC2").Value = "RFD";
                ws.Cell("AC2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("AD2").Value = "DO ISSUED";
                ws.Cell("AD2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("AE2").Value = "Booked";
                ws.Cell("AE2").Style.Font.SetBold().Font.FontSize = 11;
                ws.Cell("AF2").Value = "PDI/PTS";
                ws.Cell("AF2").Style.Font.SetBold().Font.FontSize = 11;

                ws.Cell("AG1").Value = "Total";
                ws.Range("AG1:AJ1").Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Font.SetBold().Font.FontSize = 12;
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
                ws.Cell("AP1").Value = "CDK in STOCK";
                ws.Range("AP1:AP2").Merge().Style.Font.SetBold().Font.FontSize = 12;

                ws.Cell("AQ1").Value = "LC";
                ws.Range("AQ1:AQ2").Merge().Style.Font.SetBold().Font.FontSize = 12;

                DataTable part_segment_list = new Stock_DAL().GetPartSegment();
                double SUB_TOTAL_QTY = 0;
                double SUB_DHAMRAI_CKD_XX_RFD = 0;
                double SUB_DHAMRAI_CKD_XX_DO_ISSUED = 0;
                double SUB_DHAMRAI_CKD_XX_BOOKED = 0;
                double SUB_DHAMRAI_CKD_XX_PDI_PTS = 0;
                double SUB_DHAMRAI_CBU_XX_RFD = 0;
                double SUB_DHAMRAI_CBU_XX_DO_ISSUED = 0;
                double SUB_DHAMRAI_CBU_XX_BOOKED = 0;
                double SUB_DHAMRAI_CBU_XX_PDI_PTS = 0;
                double SUB_JOYDEBPUR_XX_RFD = 0;
                double SUB_JOYDEBPUR_XX_DO_ISSUED = 0;
                double SUB_JOYDEBPUR_XX_BOOKED = 0;
                double SUB_JOYDEBPUR_XX_PDI_PTS = 0;
                double SUB_CHATTOGRAM_XX_RFD = 0;
                double SUB_CHATTOGRAM_XX_DO_ISSUED = 0;
                double SUB_CHATTOGRAM_XX_BOOKED = 0;
                double SUB_CHATTOGRAM_XX_PDI_PTS = 0;
                double SUB_CUMILLA_XX_RFD = 0;
                double SUB_CUMILLA_XX_DO_ISSUED = 0;
                double SUB_CUMILLA_XX_BOOKED = 0;
                double SUB_CUMILLA_XX_PDI_PTS = 0;
                double SUB_JASHORE_XX_RFD = 0;
                double SUB_JASHORE_XX_DO_ISSUED = 0;
                double SUB_JASHORE_XX_BOOKED = 0;
                double SUB_JASHORE_XX_PDI_PTS = 0;
                double SUB_BOGRA_XX_RFD = 0;
                double SUB_BOGRA_XX_DO_ISSUED = 0;
                double SUB_BOGRA_XX_BOOKED = 0;
                double SUB_BOGRA_XX_PDI_PTS = 0;
                double SUB_RFD = 0;
                double SUB_DO_ISSUED = 0;
                double SUB_BOOKED = 0;
                double SUB_PDI_PTS = 0;
                double SUB_FAIR_DEMO_QTY = 0;
                double SUB_DEALER_PD_QTY = 0;
                double SUB_OUTSIDE_BWS_QTY = 0;
                double SUB_BUS_BODY_QTY = 0;
                double SUB_TOTAL_OT_QTY = 0;
                double SUB_LC_QTY = 0;
                double SUB_CKD_IN_STOCK = 0;

                int i = 0;
                int c = 0;

                foreach (DataRow row in part_segment_list.Rows)
                {

                    c = i + 3;

                    DataRow[] stock_list = dt.Select("PART_SEGMENT = '" + row["PART_SEGMENT"].ToString() + "'");


                    foreach (DataRow stock_row in stock_list)
                    {
                        ws.Cell("A" + (i + 3)).Value = i + 1;
                        ws.Cell("C" + (i + 3)).Value = stock_row["MODEL"];
                        ws.Cell("D" + (i + 3)).Value = (Convert.ToInt16(stock_row["TOTAL_QTY"]) + Convert.ToInt16(stock_row["LC_QTY"])) ;
                        ws.Cell("E" + (i + 3)).Value = stock_row["DHAMRAI_CKD_XX_RFD"];
                        ws.Cell("F" + (i + 3)).Value = stock_row["DHAMRAI_CKD_XX_DO_ISSUED"];
                        ws.Cell("G" + (i + 3)).Value = stock_row["DHAMRAI_CKD_XX_BOOKED"];
                        ws.Cell("H" + (i + 3)).Value = stock_row["DHAMRAI_CKD_XX_PDI_PTS"];
                        ws.Cell("I" + (i + 3)).Value = stock_row["DHAMRAI_CBU_XX_RFD"];
                        ws.Cell("J" + (i + 3)).Value = stock_row["DHAMRAI_CBU_XX_DO_ISSUED"];
                        ws.Cell("K" + (i + 3)).Value = stock_row["DHAMRAI_CBU_XX_BOOKED"];
                        ws.Cell("L" + (i + 3)).Value = stock_row["DHAMRAI_CBU_XX_PDI_PTS"];
                        ws.Cell("M" + (i + 3)).Value = stock_row["JOYDEBPUR_XX_RFD"];
                        ws.Cell("N" + (i + 3)).Value = stock_row["JOYDEBPUR_XX_DO_ISSUED"];
                        ws.Cell("O" + (i + 3)).Value = stock_row["JOYDEBPUR_XX_BOOKED"];
                        ws.Cell("P" + (i + 3)).Value = stock_row["JOYDEBPUR_XX_PDI_PTS"];
                        ws.Cell("Q" + (i + 3)).Value = stock_row["CHATTOGRAM_XX_RFD"];
                        ws.Cell("R" + (i + 3)).Value = stock_row["CHATTOGRAM_XX_DO_ISSUED"];
                        ws.Cell("S" + (i + 3)).Value = stock_row["CHATTOGRAM_XX_BOOKED"];
                        ws.Cell("T" + (i + 3)).Value = stock_row["CHATTOGRAM_XX_PDI_PTS"];
                        ws.Cell("U" + (i + 3)).Value = stock_row["CUMILLA_XX_RFD"];
                        ws.Cell("V" + (i + 3)).Value = stock_row["CUMILLA_XX_DO_ISSUED"];
                        ws.Cell("W" + (i + 3)).Value = stock_row["CUMILLA_XX_BOOKED"];
                        ws.Cell("X" + (i + 3)).Value = stock_row["CUMILLA_XX_PDI_PTS"];
                        ws.Cell("Y" + (i + 3)).Value = stock_row["JASHORE_XX_RFD"];
                        ws.Cell("Z" + (i + 3)).Value = stock_row["JASHORE_XX_DO_ISSUED"];
                        ws.Cell("AA" + (i + 3)).Value = stock_row["JASHORE_XX_BOOKED"];
                        ws.Cell("AB" + (i + 3)).Value = stock_row["JASHORE_XX_PDI_PTS"];
                        ws.Cell("AC" + (i + 3)).Value = stock_row["BOGRA_XX_RFD"];
                        ws.Cell("AD" + (i + 3)).Value = stock_row["BOGRA_XX_DO_ISSUED"];
                        ws.Cell("AE" + (i + 3)).Value = stock_row["BOGRA_XX_BOOKED"];
                        ws.Cell("AF" + (i + 3)).Value = stock_row["BOGRA_XX_PDI_PTS"];
                        ws.Cell("AG" + (i + 3)).Value = stock_row["RFD"];
                        ws.Cell("AH" + (i + 3)).Value = stock_row["DO_ISSUED"];
                        ws.Cell("AI" + (i + 3)).Value = stock_row["BOOKED"];
                        ws.Cell("AJ" + (i + 3)).Value = stock_row["PDI_PTS"];
                        ws.Cell("AK" + (i + 3)).Value = stock_row["FAIR_DEMO_QTY"];
                        ws.Cell("AL" + (i + 3)).Value = stock_row["DEALER_PD_QTY"];
                        ws.Cell("AM" + (i + 3)).Value = stock_row["OUTSIDE_BWS_QTY"];
                        ws.Cell("AN" + (i + 3)).Value = stock_row["BUS_BODY_QTY"];
                        ws.Cell("AO" + (i + 3)).Value = stock_row["TOTAL_OT_QTY"];
                        ws.Cell("AP" + (i + 3)).Value = stock_row["CKD_IN_STOCK"];
                        ws.Cell("AQ" + (i + 3)).Value = stock_row["LC_QTY"];

                        SUB_TOTAL_QTY += Convert.ToDouble(stock_row["TOTAL_QTY"]);
                        SUB_DHAMRAI_CKD_XX_RFD += Convert.ToDouble(stock_row["DHAMRAI_CKD_XX_RFD"]);
                        SUB_DHAMRAI_CKD_XX_DO_ISSUED += Convert.ToDouble(stock_row["DHAMRAI_CKD_XX_DO_ISSUED"]);
                        SUB_DHAMRAI_CKD_XX_BOOKED += Convert.ToDouble(stock_row["DHAMRAI_CKD_XX_BOOKED"]);
                        SUB_DHAMRAI_CKD_XX_PDI_PTS += Convert.ToDouble(stock_row["DHAMRAI_CKD_XX_PDI_PTS"]);
                        SUB_DHAMRAI_CBU_XX_RFD += Convert.ToDouble(stock_row["DHAMRAI_CBU_XX_RFD"]);
                        SUB_DHAMRAI_CBU_XX_DO_ISSUED += Convert.ToDouble(stock_row["DHAMRAI_CBU_XX_DO_ISSUED"]);
                        SUB_DHAMRAI_CBU_XX_BOOKED += Convert.ToDouble(stock_row["DHAMRAI_CBU_XX_BOOKED"]);
                        SUB_DHAMRAI_CBU_XX_PDI_PTS += Convert.ToDouble(stock_row["DHAMRAI_CBU_XX_PDI_PTS"]);
                        SUB_JOYDEBPUR_XX_RFD += Convert.ToDouble(stock_row["JOYDEBPUR_XX_RFD"]);
                        SUB_JOYDEBPUR_XX_DO_ISSUED += Convert.ToDouble(stock_row["JOYDEBPUR_XX_DO_ISSUED"]);
                        SUB_JOYDEBPUR_XX_BOOKED += Convert.ToDouble(stock_row["JOYDEBPUR_XX_BOOKED"]);
                        SUB_JOYDEBPUR_XX_PDI_PTS += Convert.ToDouble(stock_row["JOYDEBPUR_XX_PDI_PTS"]);
                        SUB_CHATTOGRAM_XX_RFD += Convert.ToDouble(stock_row["CHATTOGRAM_XX_RFD"]);
                        SUB_CHATTOGRAM_XX_DO_ISSUED += Convert.ToDouble(stock_row["CHATTOGRAM_XX_DO_ISSUED"]);
                        SUB_CHATTOGRAM_XX_BOOKED += Convert.ToDouble(stock_row["CHATTOGRAM_XX_BOOKED"]);
                        SUB_CHATTOGRAM_XX_PDI_PTS += Convert.ToDouble(stock_row["CHATTOGRAM_XX_PDI_PTS"]);
                        SUB_CUMILLA_XX_RFD += Convert.ToDouble(stock_row["CUMILLA_XX_RFD"]);
                        SUB_CUMILLA_XX_DO_ISSUED += Convert.ToDouble(stock_row["CUMILLA_XX_DO_ISSUED"]);
                        SUB_CUMILLA_XX_BOOKED += Convert.ToDouble(stock_row["CUMILLA_XX_BOOKED"]);
                        SUB_CUMILLA_XX_PDI_PTS += Convert.ToDouble(stock_row["CUMILLA_XX_PDI_PTS"]);
                        SUB_JASHORE_XX_RFD += Convert.ToDouble(stock_row["JASHORE_XX_RFD"]);
                        SUB_JASHORE_XX_DO_ISSUED += Convert.ToDouble(stock_row["JASHORE_XX_DO_ISSUED"]);
                        SUB_JASHORE_XX_BOOKED += Convert.ToDouble(stock_row["JASHORE_XX_BOOKED"]);
                        SUB_JASHORE_XX_PDI_PTS += Convert.ToDouble(stock_row["JASHORE_XX_PDI_PTS"]);
                        SUB_BOGRA_XX_RFD += Convert.ToDouble(stock_row["BOGRA_XX_RFD"]);
                        SUB_BOGRA_XX_DO_ISSUED += Convert.ToDouble(stock_row["BOGRA_XX_DO_ISSUED"]);
                        SUB_BOGRA_XX_BOOKED += Convert.ToDouble(stock_row["BOGRA_XX_BOOKED"]);
                        SUB_BOGRA_XX_PDI_PTS += Convert.ToDouble(stock_row["BOGRA_XX_PDI_PTS"]);
                        SUB_RFD += Convert.ToDouble(stock_row["RFD"]);
                        SUB_DO_ISSUED += Convert.ToDouble(stock_row["DO_ISSUED"]);
                        SUB_BOOKED += Convert.ToDouble(stock_row["BOOKED"]);
                        SUB_PDI_PTS += Convert.ToDouble(stock_row["PDI_PTS"]);
                        SUB_FAIR_DEMO_QTY += Convert.ToDouble(stock_row["FAIR_DEMO_QTY"]);
                        SUB_DEALER_PD_QTY += Convert.ToDouble(stock_row["DEALER_PD_QTY"]);
                        SUB_OUTSIDE_BWS_QTY += Convert.ToDouble(stock_row["OUTSIDE_BWS_QTY"]);
                        SUB_BUS_BODY_QTY += Convert.ToDouble(stock_row["BUS_BODY_QTY"]);
                        SUB_TOTAL_OT_QTY += Convert.ToDouble(stock_row["TOTAL_OT_QTY"]);
                        SUB_CKD_IN_STOCK += Convert.ToDouble(stock_row["CKD_IN_STOCK"]);
                        SUB_LC_QTY += Convert.ToDouble(stock_row["LC_QTY"]);

                        i++;

                    }

                    /*var test = "B" + c + ":B" + (i + 2);
                    ws.Cell("D" + (i + 2)).Value = test;*/
                    if (stock_list.Length != 0)
                    {
                        ws.Range("B" + c + ":B" + (i + 2)).Merge().Style.Font.FontSize = 12;
                        ws.Range("B" + c + ":B" + (i + 2)).Merge().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                        ws.Cell("B" + (c)).Value = row["PART_SEGMENT"].ToString();
                    }

                }
                ws.Cell("D" + (i + 3)).Value = (SUB_TOTAL_QTY+ SUB_LC_QTY);
                ws.Cell("E" + (i + 3)).Value = SUB_DHAMRAI_CKD_XX_RFD;
                ws.Cell("F" + (i + 3)).Value = SUB_DHAMRAI_CKD_XX_DO_ISSUED;
                ws.Cell("G" + (i + 3)).Value = SUB_DHAMRAI_CKD_XX_BOOKED;
                ws.Cell("H" + (i + 3)).Value = SUB_DHAMRAI_CKD_XX_PDI_PTS;
                ws.Cell("I" + (i + 3)).Value = SUB_DHAMRAI_CBU_XX_RFD;
                ws.Cell("J" + (i + 3)).Value = SUB_DHAMRAI_CBU_XX_DO_ISSUED;
                ws.Cell("K" + (i + 3)).Value = SUB_DHAMRAI_CBU_XX_BOOKED;
                ws.Cell("L" + (i + 3)).Value = SUB_DHAMRAI_CBU_XX_PDI_PTS;
                ws.Cell("M" + (i + 3)).Value = SUB_JOYDEBPUR_XX_RFD;
                ws.Cell("N" + (i + 3)).Value = SUB_JOYDEBPUR_XX_DO_ISSUED;
                ws.Cell("O" + (i + 3)).Value = SUB_JOYDEBPUR_XX_BOOKED;
                ws.Cell("P" + (i + 3)).Value = SUB_JOYDEBPUR_XX_PDI_PTS;
                ws.Cell("Q" + (i + 3)).Value = SUB_CHATTOGRAM_XX_RFD;
                ws.Cell("R" + (i + 3)).Value = SUB_CHATTOGRAM_XX_DO_ISSUED;
                ws.Cell("S" + (i + 3)).Value = SUB_CHATTOGRAM_XX_BOOKED;
                ws.Cell("T" + (i + 3)).Value = SUB_CHATTOGRAM_XX_PDI_PTS;
                ws.Cell("U" + (i + 3)).Value = SUB_CUMILLA_XX_RFD;
                ws.Cell("V" + (i + 3)).Value = SUB_CUMILLA_XX_DO_ISSUED;
                ws.Cell("W" + (i + 3)).Value = SUB_CUMILLA_XX_BOOKED;
                ws.Cell("X" + (i + 3)).Value = SUB_CUMILLA_XX_PDI_PTS;
                ws.Cell("Y" + (i + 3)).Value = SUB_JASHORE_XX_RFD;
                ws.Cell("Z" + (i + 3)).Value = SUB_JASHORE_XX_DO_ISSUED;
                ws.Cell("AA" + (i + 3)).Value = SUB_JASHORE_XX_BOOKED;
                ws.Cell("AB" + (i + 3)).Value = SUB_JASHORE_XX_PDI_PTS;
                ws.Cell("AC" + (i + 3)).Value = SUB_BOGRA_XX_RFD;
                ws.Cell("AD" + (i + 3)).Value = SUB_BOGRA_XX_DO_ISSUED;
                ws.Cell("AE" + (i + 3)).Value = SUB_BOGRA_XX_BOOKED;
                ws.Cell("AF" + (i + 3)).Value = SUB_BOGRA_XX_PDI_PTS;
                ws.Cell("AG" + (i + 3)).Value = SUB_RFD;
                ws.Cell("AH" + (i + 3)).Value = SUB_DO_ISSUED;
                ws.Cell("AI" + (i + 3)).Value = SUB_BOOKED;
                ws.Cell("AJ" + (i + 3)).Value = SUB_PDI_PTS;
                ws.Cell("AK" + (i + 3)).Value = SUB_FAIR_DEMO_QTY;
                ws.Cell("AL" + (i + 3)).Value = SUB_DEALER_PD_QTY;
                ws.Cell("AM" + (i + 3)).Value = SUB_OUTSIDE_BWS_QTY;
                ws.Cell("AN" + (i + 3)).Value = SUB_BUS_BODY_QTY;
                ws.Cell("AO" + (i + 3)).Value = SUB_TOTAL_OT_QTY;
                ws.Cell("AP" + (i + 3)).Value = SUB_CKD_IN_STOCK;
                ws.Cell("AQ" + (i + 3)).Value = SUB_LC_QTY;
                ws.Cell("C" + (i + 3)).Value = "Total";
                ws.Range("C" + (i + 3) + ":AP" + (i + 3)).Style.Font.SetBold();

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Daily Stock Report " + DateTime.Now.ToString("yyyy-dd-M") + ".xlsx");
                }

            }


        }



    }
}