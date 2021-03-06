using System.Data;
using System.Data.OracleClient;

namespace IALDashboard.DAL
{
    public class Stock_DAL : BaseDataAccessLayer
    {

        public DataTable DailyStockReport()
        {

            DataTable dt = new DataTable("Grid");

            try
            {

                OracleCommand com = GetSPCommand("SP_Daily_Stock_Report");
                com.Parameters.Add("P_PART_NO", OracleType.VarChar).Value = "";
                com.Parameters.Add("P_CONTRACT", OracleType.VarChar).Value = "";
                com.Parameters.Add("P_RECORDSET", OracleType.Cursor).Direction = ParameterDirection.Output;
                OracleDataAdapter oraData = new OracleDataAdapter(com);

                oraData.Fill(dt);
            }
            catch
            {

            }
            finally
            {
                Dispose();
            }

            return dt;


        }

        public DataTable StockTableTemp()
        {

            DataTable stocklist = new DataTable("StockReport");



            try
            {


                string StrSql_old = " SELECT "
                + " PARTDES, PART_NO, PRODUCT_FAMILY, "
                + " NVL(BUS_BODY_QTY,0) BUS_BODY_QTY, NVL(DEALER_PD_QTY,0) DEALER_PD_QTY, NVL(FAIR_DEMO_QTY,0) FAIR_DEMO_QTY,"
                + " NVL(LC_QTY,0) LC_QTY, NVL(OUTSIDE_BWS_QTY,0) OUTSIDE_BWS_QTY, NVL(TOTAL_OT_QTY,0) TOTAL_OT_QTY, "
                + " NVL(TRANSPORTT_QTY,0) TRANSPORTT_QTY, NVL(DHAMRAI_CKD#RFD_CKD,0) DHAMRAI_CKD#RFD_CKD ,NVL(DHAMRAI_CBU#RFD_CBU,0) DHAMRAI_CBU#RFD_CBU, "
                + " NVL(DHAMRAI_CKD#DO_ISSUED,0) DHAMRAI_CKD#DO_ISSUED, NVL(DHAMRAI_CKD#BOOKED_DAP,0) DHAMRAI_CKD#BOOKED_DAP ,NVL(DHAMRAI_CKD#DPD,0) DHAMRAI_CKD#DPD, "
                + " NVL(DHAMRAI_CKD#FAIR_AND_DEMO,0) DHAMRAI_CKD#FAIR_AND_DEMO, NVL(DHAMRAI_CKD#OBW,0) DHAMRAI_CKD#OBW,NVL(DHAMRAI_CKD#BUS_BODY,0) DHAMRAI_CKD#BUS_BODY, "
                + " NVL(DHAMRAI_CBU#PDI_PTS,0) DHAMRAI_CBU#PDI_PTS, NVL(DHAMRAI_CKD#TOTAL_OT,0) DHAMRAI_CKD#TOTAL_OT, NVL(JOYDEBPUR#RFD,0) JOYDEBPUR#RFD,"
                + " NVL(JOYDEBPUR#DO_ISSUED,0) JOYDEBPUR#DO_ISSUED, NVL(JOYDEBPUR#BOOKED,0) JOYDEBPUR#BOOKED, NVL(JOYDEBPUR#TOTAL_OT,0) JOYDEBPUR#TOTAL_OT,"
                + " NVL(JOYDEBPUR#FAIR_AND_DEMO,0) JOYDEBPUR#FAIR_AND_DEMO, NVL(JOYDEBPUR#PDI_PTS,0) JOYDEBPUR#PDI_PTS, NVL(JOYDEBPUR#DPD,0) JOYDEBPUR#DPD, "
                + " NVL(JOYDEBPUR#OBW,0) JOYDEBPUR#OBW, NVL(CUMILLA#RFD,0) CUMILLA#RFD, NVL(CUMILLA#DO_ISSUED,0) CUMILLA#DO_ISSUED, "
                + " NVL(CUMILLA#BOOKED,0) CUMILLA#BOOKED, NVL(CUMILLA#FAIR_AND_DEMO,0) CUMILLA#FAIR_AND_DEMO, NVL(BOGRA#DPD,0) BOGRA#DPD, "
                + " NVL(CUMILLA#OBW,0) CUMILLA#OBW, NVL(CUMILLA#PDI_PTS,0) CUMILLA#PDI_PTS, NVL(CUMILLA#OSC,0) CUMILLA#OSC, "
                + " NVL(CTG#RFD,0) CTG#RFD, NVL(CTG#DO_ISSUED,0) CTG#DO_ISSUED, NVL(CTG#BOOKED,0) CTG#BOOKED, "
                + " NVL(CTG#FAIR_AND_DEMO,0) CTG#FAIR_AND_DEMO, NVL(CTG#PDI_PTS,0) CTG#PDI_PTS, NVL(CTG#DPD,0) CTG#DPD, "
                + " NVL(CTG#TOTAL_OT,0) CTG#TOTAL_OT, NVL(JASHORE#RFD,0) JASHORE#RFD, NVL(JASHORE#DO_ISSUED,0) JASHORE#DO_ISSUED, "
                + " NVL(JASHORE#BOOKED,0) JASHORE#BOOKED, NVL(JASHORE#PDI_PTS,0) JASHORE#PDI_PTS, NVL(JASHORE#FAIR_AND_DEMO,0) JASHORE#FAIR_AND_DEMO, "
                + " NVL(JASHORE#DPD,0) JASHORE#DPD, NVL(JASHORE#OBW,0) JASHORE#OBW, NVL(JASHORE#TOTAL_OT,0) JASHORE#TOTAL_OT, "
                + " NVL(BOGRA#RFD,0) BOGRA#RFD, NVL(BOGRA#DO_ISSUED,0) BOGRA#DO_ISSUED, NVL(BOGRA#BOOKED,0) BOGRA#BOOKED, "
                + " NVL(BOGRA#FAIR_AND_DEMO,0) BOGRA#FAIR_AND_DEMO, NVL(BOGRA#OBW,0) BOGRA#OBW, NVL(BOGRA#PDI_PTS,0) BOGRA#PDI_PTS, "
                + " NVL(BOGRA#TOTAL_OT,0) BOGRA#TOTAL_OT,NVL(DHAMRAI_CKD#PDI_PTS,0) DHAMRAI_CKD#PDI_PTS, NVL(DHAMRAI_CBU#DO_ISSUED,0) DHAMRAI_CBU#DO_ISSUED, "
                + " NVL(DHAMRAI_CBU#BOOKED_DAP,0) DHAMRAI_CBU#BOOKED_DAP, "
                + " (COALESCE(DHAMRAI_CBU#PDI_PTS ,0) + COALESCE(JOYDEBPUR#PDI_PTS ,0) + COALESCE(CUMILLA#PDI_PTS ,0) + COALESCE(CTG#PDI_PTS ,0) + COALESCE(JASHORE#PDI_PTS ,0) + COALESCE(BOGRA#PDI_PTS ,0) + COALESCE(DHAMRAI_CKD#PDI_PTS ,0)) AS PDI_PTS, "
                + " (COALESCE(DHAMRAI_CKD#BOOKED_DAP ,0) + COALESCE(JOYDEBPUR#BOOKED ,0) + COALESCE(CUMILLA#BOOKED ,0) + COALESCE(CTG#BOOKED ,0) + COALESCE(JASHORE#BOOKED ,0) + COALESCE(BOGRA#BOOKED ,0) + COALESCE(DHAMRAI_CBU#BOOKED_DAP ,0)) AS BOOKED, "
                + " (COALESCE(DHAMRAI_CKD#DO_ISSUED, 0) + COALESCE(JOYDEBPUR#DO_ISSUED, 0) + COALESCE(CUMILLA#DO_ISSUED, 0) + COALESCE(CTG#DO_ISSUED, 0) + COALESCE(JASHORE#DO_ISSUED, 0) + COALESCE(BOGRA#DO_ISSUED, 0) + COALESCE(DHAMRAI_CBU#DO_ISSUED, 0)) AS DO_ISSUED, "
                + " (COALESCE(DHAMRAI_CKD#RFD_CKD, 0) + COALESCE(DHAMRAI_CBU#RFD_CBU,0) + COALESCE(JOYDEBPUR#RFD, 0) + COALESCE(CUMILLA#RFD, 0) + COALESCE(CTG#RFD, 0) + COALESCE(JASHORE#RFD,0) + COALESCE(BOGRA#RFD,0)) AS RFD, "
                + " (COALESCE(DHAMRAI_CBU#PDI_PTS ,0) + COALESCE(JOYDEBPUR#PDI_PTS ,0) + COALESCE(CUMILLA#PDI_PTS ,0) + COALESCE(CTG#PDI_PTS ,0) + COALESCE(JASHORE#PDI_PTS ,0) + COALESCE(BOGRA#PDI_PTS ,0) + COALESCE(DHAMRAI_CKD#PDI_PTS ,0) +"
                + " COALESCE(DHAMRAI_CKD#BOOKED_DAP ,0) + COALESCE(JOYDEBPUR#BOOKED ,0) + COALESCE(CUMILLA#BOOKED ,0) + COALESCE(CTG#BOOKED ,0) + COALESCE(JASHORE#BOOKED ,0) + COALESCE(BOGRA#BOOKED ,0) + COALESCE(DHAMRAI_CBU#BOOKED_DAP ,0) +"
                + " COALESCE(DHAMRAI_CKD#DO_ISSUED, 0) + COALESCE(JOYDEBPUR#DO_ISSUED, 0) + COALESCE(CUMILLA#DO_ISSUED, 0) + COALESCE(CTG#DO_ISSUED, 0) + COALESCE(JASHORE#DO_ISSUED, 0) + COALESCE(BOGRA#DO_ISSUED, 0) + COALESCE(DHAMRAI_CBU#DO_ISSUED, 0) +"
                + " COALESCE(DHAMRAI_CKD#RFD_CKD, 0) + COALESCE(DHAMRAI_CBU#RFD_CBU,0) + COALESCE(JOYDEBPUR#RFD, 0) + COALESCE(CUMILLA#RFD, 0) + COALESCE(CTG#RFD, 0) + COALESCE(JASHORE#RFD,0) + COALESCE(BOGRA#RFD,0)) AS TOTAL_QTY"
                + " FROM REPORTDB.IAL_STOCK_LOCATION_TEMP";


                string StrSql_2 = " SELECT "
               + " PARTDES, PART_NO, PRODUCT_FAMILY, SEQ, MODEL, PART_SEGMENT,"
               + " NVL(BUS_BODY_QTY,0) BUS_BODY_QTY, NVL(DEALER_PD_QTY,0) DEALER_PD_QTY, NVL(FAIR_DEMO_QTY,0) FAIR_DEMO_QTY,"
               + " NVL(LC_QTY,0) LC_QTY, NVL(OUTSIDE_BWS_QTY,0) OUTSIDE_BWS_QTY, NVL(TOTAL_OT_QTY,0) TOTAL_OT_QTY, "
               + " NVL(TRANSPORTT_QTY,0) TRANSPORTT_QTY, NVL(DHAMRAI_CKD_XX_RFD,0) DHAMRAI_CKD_XX_RFD ,NVL(DHAMRAI_CBU_XX_RFD,0) DHAMRAI_CBU_XX_RFD, "
               + " NVL(DHAMRAI_CKD_XX_DO_ISSUED,0) DHAMRAI_CKD_XX_DO_ISSUED, NVL(DHAMRAI_CKD_XX_BOOKED,0) DHAMRAI_CKD_XX_BOOKED ,NVL(DHAMRAI_CKD_XX_DPD,0) DHAMRAI_CKD_XX_DPD, "
               + " NVL(DHAMRAI_CKD_XX_FAIR_AND_DEMO,0) DHAMRAI_CKD_XX_FAIR_AND_DEMO, NVL(DHAMRAI_CKD_XX_OBW,0) DHAMRAI_CKD_XX_OBW,NVL(DHAMRAI_CKD_XX_BUS_BODY,0) DHAMRAI_CKD_XX_BUS_BODY, "
               + " NVL(DHAMRAI_CBU_XX_PDI_PTS,0) DHAMRAI_CBU_XX_PDI_PTS, NVL(DHAMRAI_CKD_XX_TOTAL_OT,0) DHAMRAI_CKD_XX_TOTAL_OT, NVL(JOYDEBPUR_XX_RFD,0) JOYDEBPUR_XX_RFD,"
               + " NVL(JOYDEBPUR_XX_DO_ISSUED,0) JOYDEBPUR_XX_DO_ISSUED, NVL(JOYDEBPUR_XX_BOOKED,0) JOYDEBPUR_XX_BOOKED, NVL(JOYDEBPUR_XX_TOTAL_OT,0) JOYDEBPUR_XX_TOTAL_OT,"
               + " NVL(JOYDEBPUR_XX_FAIR_AND_DEMO,0) JOYDEBPUR_XX_FAIR_AND_DEMO, NVL(JOYDEBPUR_XX_PDI_PTS,0) JOYDEBPUR_XX_PDI_PTS, NVL(JOYDEBPUR_XX_DPD,0) JOYDEBPUR_XX_DPD, "
               + " NVL(JOYDEBPUR_XX_OBW,0) JOYDEBPUR_XX_OBW, NVL(CUMILLA_XX_RFD,0) CUMILLA_XX_RFD, NVL(CUMILLA_XX_DO_ISSUED,0) CUMILLA_XX_DO_ISSUED, "
               + " NVL(CUMILLA_XX_BOOKED,0) CUMILLA_XX_BOOKED, NVL(CUMILLA_XX_FAIR_AND_DEMO,0) CUMILLA_XX_FAIR_AND_DEMO, NVL(BOGRA_XX_DPD,0) BOGRA_XX_DPD, "
               + " NVL(CUMILLA_XX_OBW,0) CUMILLA_XX_OBW, NVL(CUMILLA_XX_PDI_PTS,0) CUMILLA_XX_PDI_PTS, NVL(CUMILLA_XX_OSC,0) CUMILLA_XX_OSC, "
               + " NVL(CHATTOGRAM_XX_RFD,0) CHATTOGRAM_XX_RFD, NVL(CHATTOGRAM_XX_DO_ISSUED,0) CHATTOGRAM_XX_DO_ISSUED, NVL(CHATTOGRAM_XX_BOOKED,0) CHATTOGRAM_XX_BOOKED, "
               + " NVL(CHATTOGRAM_XX_FAIR_AND_DEMO,0) CHATTOGRAM_XX_FAIR_AND_DEMO, NVL(CHATTOGRAM_XX_PDI_PTS,0) CHATTOGRAM_XX_PDI_PTS, NVL(CHATTOGRAM_XX_DPD,0) CHATTOGRAM_XX_DPD, "
               + " NVL(CHATTOGRAM_XX_TOTAL_OT,0) CHATTOGRAM_XX_TOTAL_OT, NVL(JASHORE_XX_RFD,0) JASHORE_XX_RFD, NVL(JASHORE_XX_DO_ISSUED,0) JASHORE_XX_DO_ISSUED, "
               + " NVL(JASHORE_XX_BOOKED,0) JASHORE_XX_BOOKED, NVL(JASHORE_XX_PDI_PTS,0) JASHORE_XX_PDI_PTS, NVL(JASHORE_XX_FAIR_AND_DEMO,0) JASHORE_XX_FAIR_AND_DEMO, "
               + " NVL(JASHORE_XX_DPD,0) JASHORE_XX_DPD, NVL(JASHORE_XX_OBW,0) JASHORE_XX_OBW, NVL(JASHORE_XX_TOTAL_OT,0) JASHORE_XX_TOTAL_OT, "
               + " NVL(BOGRA_XX_RFD,0) BOGRA_XX_RFD, NVL(BOGRA_XX_DO_ISSUED,0) BOGRA_XX_DO_ISSUED, NVL(BOGRA_XX_BOOKED,0) BOGRA_XX_BOOKED, "
               + " NVL(BOGRA_XX_FAIR_AND_DEMO,0) BOGRA_XX_FAIR_AND_DEMO, NVL(BOGRA_XX_OBW,0) BOGRA_XX_OBW, NVL(BOGRA_XX_PDI_PTS,0) BOGRA_XX_PDI_PTS, "
               + " NVL(BOGRA_XX_TOTAL_OT,0) BOGRA_XX_TOTAL_OT,NVL(DHAMRAI_CKD_XX_PDI_PTS,0) DHAMRAI_CKD_XX_PDI_PTS, NVL(DHAMRAI_CBU_XX_DO_ISSUED,0) DHAMRAI_CBU_XX_DO_ISSUED, "
               + " NVL(DHAMRAI_CBU_XX_BOOKED,0) DHAMRAI_CBU_XX_BOOKED, "
               + " (COALESCE(DHAMRAI_CBU_XX_PDI_PTS ,0) + COALESCE(JOYDEBPUR_XX_PDI_PTS ,0) + COALESCE(CUMILLA_XX_PDI_PTS ,0) + COALESCE(CHATTOGRAM_XX_PDI_PTS ,0) + COALESCE(JASHORE_XX_PDI_PTS ,0) + COALESCE(BOGRA_XX_PDI_PTS ,0) + COALESCE(DHAMRAI_CKD_XX_PDI_PTS ,0)) AS PDI_PTS, "
               + " (COALESCE(DHAMRAI_CKD_XX_BOOKED ,0) + COALESCE(JOYDEBPUR_XX_BOOKED ,0) + COALESCE(CUMILLA_XX_BOOKED ,0) + COALESCE(CHATTOGRAM_XX_BOOKED ,0) + COALESCE(JASHORE_XX_BOOKED ,0) + COALESCE(BOGRA_XX_BOOKED ,0) + COALESCE(DHAMRAI_CBU_XX_BOOKED_DAP ,0)) AS BOOKED, "
               + " (COALESCE(DHAMRAI_CKD_XX_DO_ISSUED, 0) + COALESCE(JOYDEBPUR_XX_DO_ISSUED, 0) + COALESCE(CUMILLA_XX_DO_ISSUED, 0) + COALESCE(CHATTOGRAM_XX_DO_ISSUED, 0) + COALESCE(JASHORE_XX_DO_ISSUED, 0) + COALESCE(BOGRA_XX_DO_ISSUED, 0) + COALESCE(DHAMRAI_CBU_XX_DO_ISSUED, 0)) AS DO_ISSUED, "
               + " (COALESCE(DHAMRAI_CKD_XX_RFD, 0) + COALESCE(DHAMRAI_CBU_XX_RFD,0) + COALESCE(JOYDEBPUR_XX_RFD, 0) + COALESCE(CUMILLA_XX_RFD, 0) + COALESCE(CHATTOGRAM_XX_RFD, 0) + COALESCE(JASHORE_XX_RFD,0) + COALESCE(BOGRA_XX_RFD,0)) AS RFD, "
               + " (COALESCE(DHAMRAI_CBU_XX_PDI_PTS ,0) + COALESCE(JOYDEBPUR_XX_PDI_PTS ,0) + COALESCE(CUMILLA_XX_PDI_PTS ,0) + COALESCE(CHATTOGRAM_XX_PDI_PTS ,0) + COALESCE(JASHORE_XX_PDI_PTS ,0) + COALESCE(BOGRA_XX_PDI_PTS ,0) + COALESCE(DHAMRAI_CKD_XX_PDI_PTS ,0) +"
               + " COALESCE(DHAMRAI_CKD_XX_BOOKED ,0) + COALESCE(JOYDEBPUR_XX_BOOKED ,0) + COALESCE(CUMILLA_XX_BOOKED ,0) + COALESCE(CHATTOGRAM_XX_BOOKED ,0) + COALESCE(JASHORE_XX_BOOKED ,0) + COALESCE(BOGRA_XX_BOOKED ,0) + COALESCE(DHAMRAI_CBU_XX_BOOKED_DAP ,0) +"
               + " COALESCE(DHAMRAI_CKD_XX_DO_ISSUED, 0) + COALESCE(JOYDEBPUR_XX_DO_ISSUED, 0) + COALESCE(CUMILLA_XX_DO_ISSUED, 0) + COALESCE(CHATTOGRAM_XX_DO_ISSUED, 0) + COALESCE(JASHORE_XX_DO_ISSUED, 0) + COALESCE(BOGRA_XX_DO_ISSUED, 0) + COALESCE(DHAMRAI_CBU_XX_DO_ISSUED, 0) +"
               + " COALESCE(DHAMRAI_CKD_XX_RFD, 0) + COALESCE(DHAMRAI_CBU_XX_RFD,0) + COALESCE(JOYDEBPUR_XX_RFD, 0) + COALESCE(CUMILLA_XX_RFD, 0) + COALESCE(CHATTOGRAM_XX_RFD, 0) + COALESCE(JASHORE_XX_RFD,0) + COALESCE(BOGRA_XX_RFD,0)) AS TOTAL_QTY"
               + " FROM REPORTDB.IAL_DAILY_STK_RPT_PROS_DATA";


                string StrSql = "  SELECT  "
                + " PARTDES, S.PART_NO, S.PRODUCT_FAMILY, S.SEQ, S.MODEL, S.PART_SEGMENT, "
                + " NVL(BUS_BODY_QTY,0) BUS_BODY_QTY, NVL(DEALER_PD_QTY,0) DEALER_PD_QTY, NVL(FAIR_DEMO_QTY,0) FAIR_DEMO_QTY, "
                + " NVL(LC.LC_QTY,0) LC_QTY, NVL(OUTSIDE_BWS_QTY,0) OUTSIDE_BWS_QTY, NVL(TOTAL_OT_QTY,0) TOTAL_OT_QTY,  "
                + " NVL(TRANSPORTT_QTY,0) TRANSPORTT_QTY, NVL(DHAMRAI_CKD_XX_RFD,0) DHAMRAI_CKD_XX_RFD ,NVL(DHAMRAI_CBU_XX_RFD,0) DHAMRAI_CBU_XX_RFD,  "
                + " NVL(DHAMRAI_CKD_XX_DO_ISSUED,0) DHAMRAI_CKD_XX_DO_ISSUED, NVL(DHAMRAI_CKD_XX_BOOKED,0) DHAMRAI_CKD_XX_BOOKED ,NVL(DHAMRAI_CKD_XX_DPD,0) DHAMRAI_CKD_XX_DPD,  "
                + " NVL(DHAMRAI_CKD_XX_FAIR_AND_DEMO,0) DHAMRAI_CKD_XX_FAIR_AND_DEMO, NVL(DHAMRAI_CKD_XX_OBW,0) DHAMRAI_CKD_XX_OBW,NVL(DHAMRAI_CKD_XX_BUS_BODY,0) DHAMRAI_CKD_XX_BUS_BODY,  "
                + " NVL(DHAMRAI_CBU_XX_PDI_PTS,0) DHAMRAI_CBU_XX_PDI_PTS, NVL(DHAMRAI_CKD_XX_TOTAL_OT,0) DHAMRAI_CKD_XX_TOTAL_OT, NVL(JOYDEBPUR_XX_RFD,0) JOYDEBPUR_XX_RFD, "
                + " NVL(JOYDEBPUR_XX_DO_ISSUED,0) JOYDEBPUR_XX_DO_ISSUED, NVL(JOYDEBPUR_XX_BOOKED,0) JOYDEBPUR_XX_BOOKED, NVL(JOYDEBPUR_XX_TOTAL_OT,0) JOYDEBPUR_XX_TOTAL_OT, "
                + " NVL(JOYDEBPUR_XX_FAIR_AND_DEMO,0) JOYDEBPUR_XX_FAIR_AND_DEMO, NVL(JOYDEBPUR_XX_PDI_PTS,0) JOYDEBPUR_XX_PDI_PTS, NVL(JOYDEBPUR_XX_DPD,0) JOYDEBPUR_XX_DPD,  "
                + " NVL(JOYDEBPUR_XX_OBW,0) JOYDEBPUR_XX_OBW, NVL(CUMILLA_XX_RFD,0) CUMILLA_XX_RFD, NVL(CUMILLA_XX_DO_ISSUED,0) CUMILLA_XX_DO_ISSUED,  "
                + " NVL(CUMILLA_XX_BOOKED,0) CUMILLA_XX_BOOKED, NVL(CUMILLA_XX_FAIR_AND_DEMO,0) CUMILLA_XX_FAIR_AND_DEMO, NVL(BOGRA_XX_DPD,0) BOGRA_XX_DPD,  "
                + " NVL(CUMILLA_XX_OBW,0) CUMILLA_XX_OBW, NVL(CUMILLA_XX_PDI_PTS,0) CUMILLA_XX_PDI_PTS, NVL(CUMILLA_XX_OSC,0) CUMILLA_XX_OSC,  "
                + " NVL(CHATTOGRAM_XX_RFD,0) CHATTOGRAM_XX_RFD, NVL(CHATTOGRAM_XX_DO_ISSUED,0) CHATTOGRAM_XX_DO_ISSUED, NVL(CHATTOGRAM_XX_BOOKED,0) CHATTOGRAM_XX_BOOKED,  "
                + " NVL(CHATTOGRAM_XX_FAIR_AND_DEMO,0) CHATTOGRAM_XX_FAIR_AND_DEMO, NVL(CHATTOGRAM_XX_PDI_PTS,0) CHATTOGRAM_XX_PDI_PTS, NVL(CHATTOGRAM_XX_DPD,0) CHATTOGRAM_XX_DPD,  "
                + " NVL(CHATTOGRAM_XX_TOTAL_OT,0) CHATTOGRAM_XX_TOTAL_OT, NVL(JASHORE_XX_RFD,0) JASHORE_XX_RFD, NVL(JASHORE_XX_DO_ISSUED,0) JASHORE_XX_DO_ISSUED,  "
                + " NVL(JASHORE_XX_BOOKED,0) JASHORE_XX_BOOKED, NVL(JASHORE_XX_PDI_PTS,0) JASHORE_XX_PDI_PTS, NVL(JASHORE_XX_FAIR_AND_DEMO,0) JASHORE_XX_FAIR_AND_DEMO,  "
                + " NVL(JASHORE_XX_DPD,0) JASHORE_XX_DPD, NVL(JASHORE_XX_OBW,0) JASHORE_XX_OBW, NVL(JASHORE_XX_TOTAL_OT,0) JASHORE_XX_TOTAL_OT,  "
                + " NVL(BOGRA_XX_RFD,0) BOGRA_XX_RFD, NVL(BOGRA_XX_DO_ISSUED,0) BOGRA_XX_DO_ISSUED, NVL(BOGRA_XX_BOOKED,0) BOGRA_XX_BOOKED,  "
                + " NVL(BOGRA_XX_FAIR_AND_DEMO,0) BOGRA_XX_FAIR_AND_DEMO, NVL(BOGRA_XX_OBW,0) BOGRA_XX_OBW, NVL(BOGRA_XX_PDI_PTS,0) BOGRA_XX_PDI_PTS,  "
                + " NVL(BOGRA_XX_TOTAL_OT,0) BOGRA_XX_TOTAL_OT,NVL(DHAMRAI_CKD_XX_PDI_PTS,0) DHAMRAI_CKD_XX_PDI_PTS, NVL(DHAMRAI_CBU_XX_DO_ISSUED,0) DHAMRAI_CBU_XX_DO_ISSUED,  "
                + " NVL(DHAMRAI_CBU_XX_BOOKED,0) DHAMRAI_CBU_XX_BOOKED,  "
                + " (COALESCE(DHAMRAI_CBU_XX_PDI_PTS ,0) + COALESCE(JOYDEBPUR_XX_PDI_PTS ,0) + COALESCE(CUMILLA_XX_PDI_PTS ,0) + COALESCE(CHATTOGRAM_XX_PDI_PTS ,0) + COALESCE(JASHORE_XX_PDI_PTS ,0) + COALESCE(BOGRA_XX_PDI_PTS ,0) + COALESCE(DHAMRAI_CKD_XX_PDI_PTS ,0)) AS PDI_PTS,  "
                + " (COALESCE(DHAMRAI_CKD_XX_BOOKED ,0) + COALESCE(JOYDEBPUR_XX_BOOKED ,0) + COALESCE(CUMILLA_XX_BOOKED ,0) + COALESCE(CHATTOGRAM_XX_BOOKED ,0) + COALESCE(JASHORE_XX_BOOKED ,0) + COALESCE(BOGRA_XX_BOOKED ,0) + COALESCE(DHAMRAI_CBU_XX_BOOKED ,0)) AS BOOKED,  "
                + " (COALESCE(DHAMRAI_CKD_XX_DO_ISSUED, 0) + COALESCE(JOYDEBPUR_XX_DO_ISSUED, 0) + COALESCE(CUMILLA_XX_DO_ISSUED, 0) + COALESCE(CHATTOGRAM_XX_DO_ISSUED, 0) + COALESCE(JASHORE_XX_DO_ISSUED, 0) + COALESCE(BOGRA_XX_DO_ISSUED, 0) + COALESCE(DHAMRAI_CBU_XX_DO_ISSUED, 0)) AS DO_ISSUED,  "
                + " (COALESCE(DHAMRAI_CKD_XX_RFD, 0) + COALESCE(DHAMRAI_CBU_XX_RFD,0) + COALESCE(JOYDEBPUR_XX_RFD, 0) + COALESCE(CUMILLA_XX_RFD, 0) + COALESCE(CHATTOGRAM_XX_RFD, 0) + COALESCE(JASHORE_XX_RFD,0) + COALESCE(BOGRA_XX_RFD,0)) AS RFD,  "
                + " (COALESCE(DHAMRAI_CBU_XX_PDI_PTS ,0) + COALESCE(JOYDEBPUR_XX_PDI_PTS ,0) + COALESCE(CUMILLA_XX_PDI_PTS ,0) + COALESCE(CHATTOGRAM_XX_PDI_PTS ,0) + COALESCE(JASHORE_XX_PDI_PTS ,0) + COALESCE(BOGRA_XX_PDI_PTS ,0) + COALESCE(DHAMRAI_CKD_XX_PDI_PTS ,0) + "
                + " COALESCE(DHAMRAI_CKD_XX_BOOKED ,0) + COALESCE(JOYDEBPUR_XX_BOOKED ,0) + COALESCE(CUMILLA_XX_BOOKED ,0) + COALESCE(CHATTOGRAM_XX_BOOKED ,0) + COALESCE(JASHORE_XX_BOOKED ,0) + COALESCE(BOGRA_XX_BOOKED ,0) + COALESCE(DHAMRAI_CBU_XX_BOOKED ,0) + "
                + " COALESCE(DHAMRAI_CKD_XX_DO_ISSUED, 0) + COALESCE(JOYDEBPUR_XX_DO_ISSUED, 0) + COALESCE(CUMILLA_XX_DO_ISSUED, 0) + COALESCE(CHATTOGRAM_XX_DO_ISSUED, 0) + COALESCE(JASHORE_XX_DO_ISSUED, 0) + COALESCE(BOGRA_XX_DO_ISSUED, 0) + COALESCE(DHAMRAI_CBU_XX_DO_ISSUED, 0) + "
                + " COALESCE(DHAMRAI_CKD_XX_RFD, 0) + COALESCE(DHAMRAI_CBU_XX_RFD,0) + COALESCE(JOYDEBPUR_XX_RFD, 0) + COALESCE(CUMILLA_XX_RFD, 0) + COALESCE(CHATTOGRAM_XX_RFD, 0) + COALESCE(JASHORE_XX_RFD,0) + COALESCE(BOGRA_XX_RFD,0)) AS TOTAL_QTY "
                + " FROM REPORTDB.IAL_DAILY_STK_RPT_PROS_DATA S "
                + " LEFT JOIN REPORTDB.IAL_LC_STOCK LC ON S.PART_NO=LC.PART_NO  ";

                OracleCommand command = GetSQLCommand(StrSql);
                OracleDataAdapter odaAdapter = new OracleDataAdapter(command);
                odaAdapter.Fill(stocklist);
            }

            catch
            {
            }

            finally
            {
                Dispose();
            }

            return stocklist;
        }



        public DataTable DailyStockProcessData()
        {

            DataTable stocklist = new DataTable("StockReport");



            try
            {





                string StrSql2 = " SELECT PARTDES, "
                + "    PART_NO, "
                + "    PRODUCT_FAMILY, SEQ, MODEL, PART_SEGMENT, MAX(CKD_IN_STOCK) CKD_IN_STOCK,"
                + "    sum(NVL(BUS_BODY_QTY, 0)) BUS_BODY_QTY, "
                + "    sum(NVL(DEALER_PD_QTY, 0)) DEALER_PD_QTY, "
                + "    sum(NVL(FAIR_DEMO_QTY, 0)) FAIR_DEMO_QTY, "
                + "    MAX(NVL(LC_QTY, 0)) LC_QTY, "
                + "    sum(NVL(OUTSIDE_BWS_QTY, 0)) OUTSIDE_BWS_QTY, "
                + "    sum(NVL(TOTAL_OT_QTY, 0)) TOTAL_OT_QTY, "
                + "    sum(NVL(TRANSPORTT_QTY, 0)) TRANSPORTT_QTY, "
                + "    sum(NVL(DHAMRAI_CKD_XX_RFD, 0)) DHAMRAI_CKD_XX_RFD, "
                + "    sum(NVL(DHAMRAI_CBU_XX_RFD, 0)) DHAMRAI_CBU_XX_RFD, "
                + "    sum(NVL(DHAMRAI_CKD_XX_DO_ISSUED, 0)) DHAMRAI_CKD_XX_DO_ISSUED, "
                + "    sum(NVL(DHAMRAI_CKD_XX_BOOKED_DAP, 0)) DHAMRAI_CKD_XX_BOOKED_DAP, "
                + "    sum(NVL(DHAMRAI_CKD_XX_DPD, 0)) DHAMRAI_CKD_XX_DPD, "
                + "    sum(NVL(DHAMRAI_CKD_XX_FAIR_AND_DEMO, 0)) DHAMRAI_CKD_XX_FAIR_AND_DEMO, "
                + "    sum(NVL(DHAMRAI_CKD_XX_OBW, 0)) DHAMRAI_CKD_XX_OBW, "
                + "    sum(NVL(DHAMRAI_CKD_XX_BUS_BODY, 0)) DHAMRAI_CKD_XX_BUS_BODY, "
                + "    sum(NVL(DHAMRAI_CBU_XX_PDI_PTS, 0)) DHAMRAI_CBU_XX_PDI_PTS, "
                + "    sum(NVL(DHAMRAI_CKD_XX_TOTAL_OT, 0)) DHAMRAI_CKD_XX_TOTAL_OT, "
                + "    sum(NVL(JOYDEBPUR_XX_RFD, 0)) JOYDEBPUR_XX_RFD, "
                + "    sum(NVL(JOYDEBPUR_XX_DO_ISSUED, 0)) JOYDEBPUR_XX_DO_ISSUED, "
                + "    sum(NVL(JOYDEBPUR_XX_BOOKED, 0)) JOYDEBPUR_XX_BOOKED, "
                + "    sum(NVL(JOYDEBPUR_XX_TOTAL_OT, 0)) JOYDEBPUR_XX_TOTAL_OT, "
                + "    sum(NVL(JOYDEBPUR_XX_FAIR_AND_DEMO, 0)) JOYDEBPUR_XX_FAIR_AND_DEMO, "
                + "    sum(NVL(JOYDEBPUR_XX_PDI_PTS, 0)) JOYDEBPUR_XX_PDI_PTS, "
                + "    sum(NVL(JOYDEBPUR_XX_DPD, 0)) JOYDEBPUR_XX_DPD, "
                + "    sum(NVL(JOYDEBPUR_XX_OBW, 0)) JOYDEBPUR_XX_OBW, "
                + "    sum(NVL(CUMILLA_XX_RFD, 0)) CUMILLA_XX_RFD, "
                + "    sum(NVL(CUMILLA_XX_DO_ISSUED, 0)) CUMILLA_XX_DO_ISSUED, "
                + "    sum(NVL(CUMILLA_XX_BOOKED, 0)) CUMILLA_XX_BOOKED, "
                + "    sum(NVL(CUMILLA_XX_FAIR_AND_DEMO, 0)) CUMILLA_XX_FAIR_AND_DEMO, "
                + "    sum(NVL(BOGRA_XX_DPD, 0)) BOGRA_XX_DPD, "
                + "    sum(NVL(CUMILLA_XX_OBW, 0)) CUMILLA_XX_OBW, "
                + "    sum(NVL(CUMILLA_XX_PDI_PTS, 0)) CUMILLA_XX_PDI_PTS, "
                + "    sum(NVL(CUMILLA_XX_OSC, 0)) CUMILLA_XX_OSC, "
                + "    sum(NVL(CHATTOGRAM_XX_RFD, 0)) CHATTOGRAM_XX_RFD, "
                + "    sum(NVL(CHATTOGRAM_XX_DO_ISSUED, 0)) CHATTOGRAM_XX_DO_ISSUED, "
                + "    sum(NVL(CHATTOGRAM_XX_BOOKED, 0)) CHATTOGRAM_XX_BOOKED, "
                + "    sum(NVL(CHATTOGRAM_XX_FAIR_AND_DEMO, 0)) CHATTOGRAM_XX_FAIR_AND_DEMO, "
                + "    sum(NVL(CHATTOGRAM_XX_PDI_PTS, 0)) CHATTOGRAM_XX_PDI_PTS, "
                + "    sum(NVL(CHATTOGRAM_XX_DPD, 0)) CHATTOGRAM_XX_DPD, "
                + "    sum(NVL(CHATTOGRAM_XX_TOTAL_OT, 0)) CHATTOGRAM_XX_TOTAL_OT, "
                + "    sum(NVL(JASHORE_XX_RFD, 0)) JASHORE_XX_RFD, "
                + "    sum(NVL(JASHORE_XX_DO_ISSUED, 0)) JASHORE_XX_DO_ISSUED, "
                + "    sum(NVL(JASHORE_XX_BOOKED, 0)) JASHORE_XX_BOOKED, "
                + "    sum(NVL(JASHORE_XX_PDI_PTS, 0)) JASHORE_XX_PDI_PTS, "
                + "    sum(NVL(JASHORE_XX_FAIR_AND_DEMO, 0)) JASHORE_XX_FAIR_AND_DEMO, "
                + "    sum(NVL(JASHORE_XX_DPD, 0)) JASHORE_XX_DPD, "
                + "    sum(NVL(JASHORE_XX_OBW, 0)) JASHORE_XX_OBW, "
                + "    sum(NVL(JASHORE_XX_TOTAL_OT, 0)) JASHORE_XX_TOTAL_OT, "
                + "    sum(NVL(BOGRA_XX_RFD, 0)) BOGRA_XX_RFD, "
                + "    sum(NVL(BOGRA_XX_DO_ISSUED, 0)) BOGRA_XX_DO_ISSUED, "
                + "    sum(NVL(BOGRA_XX_BOOKED, 0)) BOGRA_XX_BOOKED, "
                + "    sum(NVL(BOGRA_XX_FAIR_AND_DEMO, 0)) BOGRA_XX_FAIR_AND_DEMO, "
                + "    sum(NVL(BOGRA_XX_OBW, 0)) BOGRA_XX_OBW, "
                + "    sum(NVL(BOGRA_XX_PDI_PTS, 0)) BOGRA_XX_PDI_PTS, "
                + "    sum(NVL(BOGRA_XX_TOTAL_OT, 0)) BOGRA_XX_TOTAL_OT, "
                + "    sum(NVL(DHAMRAI_CKD_XX_PDI_PTS, 0)) DHAMRAI_CKD_XX_PDI_PTS, "
                + "    sum(NVL(DHAMRAI_CBU_XX_DO_ISSUED, 0)) DHAMRAI_CBU_XX_DO_ISSUED, "
                + "    sum(NVL(DHAMRAI_CBU_XX_BOOKED_DAP, 0)) DHAMRAI_CBU_XX_BOOKED_DAP, "
                + "    sum( "
                + "        COALESCE(DHAMRAI_CBU_XX_PDI_PTS, 0) + COALESCE(JOYDEBPUR_XX_PDI_PTS, 0) + COALESCE(CUMILLA_XX_PDI_PTS, 0) + COALESCE(CHATTOGRAM_XX_PDI_PTS, 0) + COALESCE(JASHORE_XX_PDI_PTS, 0) + COALESCE(BOGRA_XX_PDI_PTS, 0) + COALESCE(DHAMRAI_CKD_XX_PDI_PTS, 0) "
                + "    ) AS PDI_PTS, "
                + "    sum( "
                + "        COALESCE(DHAMRAI_CKD_XX_BOOKED_DAP, 0) + COALESCE(JOYDEBPUR_XX_BOOKED, 0) + COALESCE(CUMILLA_XX_BOOKED, 0) + COALESCE(CHATTOGRAM_XX_BOOKED, 0) + COALESCE(JASHORE_XX_BOOKED, 0) + COALESCE(BOGRA_XX_BOOKED, 0) + COALESCE(DHAMRAI_CBU_XX_BOOKED_DAP, 0) "
                + "    ) AS BOOKED, "
                + "    sum( "
                + "        COALESCE(DHAMRAI_CKD_XX_DO_ISSUED, 0) + COALESCE(JOYDEBPUR_XX_DO_ISSUED, 0) + COALESCE(CUMILLA_XX_DO_ISSUED, 0) + COALESCE(CHATTOGRAM_XX_DO_ISSUED, 0) + COALESCE(JASHORE_XX_DO_ISSUED, 0) + COALESCE(BOGRA_XX_DO_ISSUED, 0) + COALESCE(DHAMRAI_CBU_XX_DO_ISSUED, 0) "
                + "    ) AS DO_ISSUED, "
                + "    sum( "
                + "        COALESCE(DHAMRAI_CKD_XX_RFD, 0) + COALESCE(DHAMRAI_CBU_XX_RFD, 0) + COALESCE(JOYDEBPUR_XX_RFD, 0) + COALESCE(CUMILLA_XX_RFD, 0) + COALESCE(CHATTOGRAM_XX_RFD, 0) + COALESCE(JASHORE_XX_RFD, 0) + COALESCE(BOGRA_XX_RFD, 0) "
                + "    ) AS RFD, "
                + "    sum( "
                + "        COALESCE(DHAMRAI_CBU_XX_PDI_PTS, 0) + COALESCE(JOYDEBPUR_XX_PDI_PTS, 0) + COALESCE(CUMILLA_XX_PDI_PTS, 0) + COALESCE(CHATTOGRAM_XX_PDI_PTS, 0) + COALESCE(JASHORE_XX_PDI_PTS, 0) + COALESCE(BOGRA_XX_PDI_PTS, 0) + COALESCE(DHAMRAI_CKD_XX_PDI_PTS, 0) + "
                + " COALESCE(DHAMRAI_CKD_XX_BOOKED_DAP, 0) + COALESCE(JOYDEBPUR_XX_BOOKED, 0) + COALESCE(CUMILLA_XX_BOOKED, 0) + COALESCE(CHATTOGRAM_XX_BOOKED, 0) + COALESCE(JASHORE_XX_BOOKED, 0) + COALESCE(BOGRA_XX_BOOKED, 0) + COALESCE(DHAMRAI_CBU_XX_BOOKED_DAP, 0) + "
                + " COALESCE(DHAMRAI_CKD_XX_DO_ISSUED, 0) + COALESCE(JOYDEBPUR_XX_DO_ISSUED, 0) + COALESCE(CUMILLA_XX_DO_ISSUED, 0) + COALESCE(CHATTOGRAM_XX_DO_ISSUED, 0) + COALESCE(JASHORE_XX_DO_ISSUED, 0) + COALESCE(BOGRA_XX_DO_ISSUED, 0) + COALESCE(DHAMRAI_CBU_XX_DO_ISSUED, 0) + "
                + " COALESCE(DHAMRAI_CKD_XX_RFD, 0) + COALESCE(DHAMRAI_CBU_XX_RFD, 0) + COALESCE(JOYDEBPUR_XX_RFD, 0) + COALESCE(CUMILLA_XX_RFD, 0) + COALESCE(CHATTOGRAM_XX_RFD, 0) + COALESCE(JASHORE_XX_RFD, 0) + COALESCE(BOGRA_XX_RFD, 0) + "
                + " NVL(BUS_BODY_QTY, 0) + NVL(DEALER_PD_QTY, 0) + NVL(FAIR_DEMO_QTY, 0) + NVL(LC_QTY, 0) + "
                + " NVL(OUTSIDE_BWS_QTY, 0) + NVL(TOTAL_OT_QTY, 0) + NVL(TRANSPORTT_QTY, 0) "
                + "    ) AS TOTAL_QTY "
                + " FROM REPORTDB.IAL_DAILY_STK_RPT_PROS_DATA "
                + " GROUP BY PARTDES, "
                + "    PART_NO, "
                + "    PRODUCT_FAMILY, SEQ, MODEL, PART_SEGMENT "
                + "ORDER BY SEQ";


                string StrSql = "SELECT S.PARTDES, S.PART_NO, S.PRODUCT_FAMILY, S.SEQ, S.MODEL, S.PART_SEGMENT ,"
               + "     MAX(CKD_IN_STOCK) CKD_IN_STOCK,"
               + "    sum(NVL(BUS_BODY_QTY, 0)) BUS_BODY_QTY, "
               + "    sum(NVL(DEALER_PD_QTY, 0)) DEALER_PD_QTY, "
               + "    sum(NVL(FAIR_DEMO_QTY, 0)) FAIR_DEMO_QTY, "
               + "    MAX(NVL(LC.LC_QTY, 0)) LC_QTY, "
               + "    sum(NVL(OUTSIDE_BWS_QTY, 0)) OUTSIDE_BWS_QTY, "
               + "    sum(NVL(TOTAL_OT_QTY, 0)) TOTAL_OT_QTY, "
               + "    sum(NVL(TRANSPORTT_QTY, 0)) TRANSPORTT_QTY, "
               + "    sum(NVL(DHAMRAI_CKD_XX_RFD, 0)) DHAMRAI_CKD_XX_RFD, "
               + "    sum(NVL(DHAMRAI_CBU_XX_RFD, 0)) DHAMRAI_CBU_XX_RFD, "
               + "    sum(NVL(DHAMRAI_CKD_XX_DO_ISSUED, 0)) DHAMRAI_CKD_XX_DO_ISSUED, "
               + "    sum(NVL(DHAMRAI_CKD_XX_BOOKED, 0)) DHAMRAI_CKD_XX_BOOKED, "
               + "    sum(NVL(DHAMRAI_CKD_XX_DPD, 0)) DHAMRAI_CKD_XX_DPD, "
               + "    sum(NVL(DHAMRAI_CKD_XX_FAIR_AND_DEMO, 0)) DHAMRAI_CKD_XX_FAIR_AND_DEMO, "
               + "    sum(NVL(DHAMRAI_CKD_XX_OBW, 0)) DHAMRAI_CKD_XX_OBW, "
               + "    sum(NVL(DHAMRAI_CKD_XX_BUS_BODY, 0)) DHAMRAI_CKD_XX_BUS_BODY, "
               + "    sum(NVL(DHAMRAI_CBU_XX_PDI_PTS, 0)) DHAMRAI_CBU_XX_PDI_PTS, "
               + "    sum(NVL(DHAMRAI_CKD_XX_TOTAL_OT, 0)) DHAMRAI_CKD_XX_TOTAL_OT, "
               + "    sum(NVL(JOYDEBPUR_XX_RFD, 0)) JOYDEBPUR_XX_RFD, "
               + "    sum(NVL(JOYDEBPUR_XX_DO_ISSUED, 0)) JOYDEBPUR_XX_DO_ISSUED, "
               + "    sum(NVL(JOYDEBPUR_XX_BOOKED, 0)) JOYDEBPUR_XX_BOOKED, "
               + "    sum(NVL(JOYDEBPUR_XX_TOTAL_OT, 0)) JOYDEBPUR_XX_TOTAL_OT, "
               + "    sum(NVL(JOYDEBPUR_XX_FAIR_AND_DEMO, 0)) JOYDEBPUR_XX_FAIR_AND_DEMO, "
               + "    sum(NVL(JOYDEBPUR_XX_PDI_PTS, 0)) JOYDEBPUR_XX_PDI_PTS, "
               + "    sum(NVL(JOYDEBPUR_XX_DPD, 0)) JOYDEBPUR_XX_DPD, "
               + "    sum(NVL(JOYDEBPUR_XX_OBW, 0)) JOYDEBPUR_XX_OBW, "
               + "    sum(NVL(CUMILLA_XX_RFD, 0)) CUMILLA_XX_RFD, "
               + "    sum(NVL(CUMILLA_XX_DO_ISSUED, 0)) CUMILLA_XX_DO_ISSUED, "
               + "    sum(NVL(CUMILLA_XX_BOOKED, 0)) CUMILLA_XX_BOOKED, "
               + "    sum(NVL(CUMILLA_XX_FAIR_AND_DEMO, 0)) CUMILLA_XX_FAIR_AND_DEMO, "
               + "    sum(NVL(BOGRA_XX_DPD, 0)) BOGRA_XX_DPD, "
               + "    sum(NVL(CUMILLA_XX_OBW, 0)) CUMILLA_XX_OBW, "
               + "    sum(NVL(CUMILLA_XX_PDI_PTS, 0)) CUMILLA_XX_PDI_PTS, "
               + "    sum(NVL(CUMILLA_XX_OSC, 0)) CUMILLA_XX_OSC, "
               + "    sum(NVL(CHATTOGRAM_XX_RFD, 0)) CHATTOGRAM_XX_RFD, "
               + "    sum(NVL(CHATTOGRAM_XX_DO_ISSUED, 0)) CHATTOGRAM_XX_DO_ISSUED, "
               + "    sum(NVL(CHATTOGRAM_XX_BOOKED, 0)) CHATTOGRAM_XX_BOOKED, "
               + "    sum(NVL(CHATTOGRAM_XX_FAIR_AND_DEMO, 0)) CHATTOGRAM_XX_FAIR_AND_DEMO, "
               + "    sum(NVL(CHATTOGRAM_XX_PDI_PTS, 0)) CHATTOGRAM_XX_PDI_PTS, "
               + "    sum(NVL(CHATTOGRAM_XX_DPD, 0)) CHATTOGRAM_XX_DPD, "
               + "    sum(NVL(CHATTOGRAM_XX_TOTAL_OT, 0)) CHATTOGRAM_XX_TOTAL_OT, "
               + "    sum(NVL(JASHORE_XX_RFD, 0)) JASHORE_XX_RFD, "
               + "    sum(NVL(JASHORE_XX_DO_ISSUED, 0)) JASHORE_XX_DO_ISSUED, "
               + "    sum(NVL(JASHORE_XX_BOOKED, 0)) JASHORE_XX_BOOKED, "
               + "    sum(NVL(JASHORE_XX_PDI_PTS, 0)) JASHORE_XX_PDI_PTS, "
               + "    sum(NVL(JASHORE_XX_FAIR_AND_DEMO, 0)) JASHORE_XX_FAIR_AND_DEMO, "
               + "    sum(NVL(JASHORE_XX_DPD, 0)) JASHORE_XX_DPD, "
               + "    sum(NVL(JASHORE_XX_OBW, 0)) JASHORE_XX_OBW, "
               + "    sum(NVL(JASHORE_XX_TOTAL_OT, 0)) JASHORE_XX_TOTAL_OT, "
               + "    sum(NVL(BOGRA_XX_RFD, 0)) BOGRA_XX_RFD, "
               + "    sum(NVL(BOGRA_XX_DO_ISSUED, 0)) BOGRA_XX_DO_ISSUED, "
               + "    sum(NVL(BOGRA_XX_BOOKED, 0)) BOGRA_XX_BOOKED, "
               + "    sum(NVL(BOGRA_XX_FAIR_AND_DEMO, 0)) BOGRA_XX_FAIR_AND_DEMO, "
               + "    sum(NVL(BOGRA_XX_OBW, 0)) BOGRA_XX_OBW, "
               + "    sum(NVL(BOGRA_XX_PDI_PTS, 0)) BOGRA_XX_PDI_PTS, "
               + "    sum(NVL(BOGRA_XX_TOTAL_OT, 0)) BOGRA_XX_TOTAL_OT, "
               + "    sum(NVL(DHAMRAI_CKD_XX_PDI_PTS, 0)) DHAMRAI_CKD_XX_PDI_PTS, "
               + "    sum(NVL(DHAMRAI_CBU_XX_DO_ISSUED, 0)) DHAMRAI_CBU_XX_DO_ISSUED, "
               + "    sum(NVL(DHAMRAI_CBU_XX_BOOKED, 0)) DHAMRAI_CBU_XX_BOOKED, "
               + "    sum( "
               + "        COALESCE(DHAMRAI_CBU_XX_PDI_PTS, 0) + COALESCE(JOYDEBPUR_XX_PDI_PTS, 0) + COALESCE(CUMILLA_XX_PDI_PTS, 0) + COALESCE(CHATTOGRAM_XX_PDI_PTS, 0) + COALESCE(JASHORE_XX_PDI_PTS, 0) + COALESCE(BOGRA_XX_PDI_PTS, 0) + COALESCE(DHAMRAI_CKD_XX_PDI_PTS, 0) "
               + "    ) AS PDI_PTS, "
               + "    sum( "
               + "        COALESCE(DHAMRAI_CKD_XX_BOOKED, 0) + COALESCE(JOYDEBPUR_XX_BOOKED, 0) + COALESCE(CUMILLA_XX_BOOKED, 0) + COALESCE(CHATTOGRAM_XX_BOOKED, 0) + COALESCE(JASHORE_XX_BOOKED, 0) + COALESCE(BOGRA_XX_BOOKED, 0) + COALESCE(DHAMRAI_CBU_XX_BOOKED, 0) "
               + "    ) AS BOOKED, "
               + "    sum( "
               + "        COALESCE(DHAMRAI_CKD_XX_DO_ISSUED, 0) + COALESCE(JOYDEBPUR_XX_DO_ISSUED, 0) + COALESCE(CUMILLA_XX_DO_ISSUED, 0) + COALESCE(CHATTOGRAM_XX_DO_ISSUED, 0) + COALESCE(JASHORE_XX_DO_ISSUED, 0) + COALESCE(BOGRA_XX_DO_ISSUED, 0) + COALESCE(DHAMRAI_CBU_XX_DO_ISSUED, 0) "
               + "    ) AS DO_ISSUED, "
               + "    sum( "
               + "        COALESCE(DHAMRAI_CKD_XX_RFD, 0) + COALESCE(DHAMRAI_CBU_XX_RFD, 0) + COALESCE(JOYDEBPUR_XX_RFD, 0) + COALESCE(CUMILLA_XX_RFD, 0) + COALESCE(CHATTOGRAM_XX_RFD, 0) + COALESCE(JASHORE_XX_RFD, 0) + COALESCE(BOGRA_XX_RFD, 0) "
               + "    ) AS RFD, "
               + "    sum( "
               + "        COALESCE(DHAMRAI_CBU_XX_PDI_PTS, 0) + COALESCE(JOYDEBPUR_XX_PDI_PTS, 0) + COALESCE(CUMILLA_XX_PDI_PTS, 0) + COALESCE(CHATTOGRAM_XX_PDI_PTS, 0) + COALESCE(JASHORE_XX_PDI_PTS, 0) + COALESCE(BOGRA_XX_PDI_PTS, 0) + COALESCE(DHAMRAI_CKD_XX_PDI_PTS, 0) + "
               + " COALESCE(DHAMRAI_CKD_XX_BOOKED, 0) + COALESCE(JOYDEBPUR_XX_BOOKED, 0) + COALESCE(CUMILLA_XX_BOOKED, 0) + COALESCE(CHATTOGRAM_XX_BOOKED, 0) + COALESCE(JASHORE_XX_BOOKED, 0) + COALESCE(BOGRA_XX_BOOKED, 0) + COALESCE(DHAMRAI_CBU_XX_BOOKED_DAP, 0) + "
               + " COALESCE(DHAMRAI_CKD_XX_DO_ISSUED, 0) + COALESCE(JOYDEBPUR_XX_DO_ISSUED, 0) + COALESCE(CUMILLA_XX_DO_ISSUED, 0) + COALESCE(CHATTOGRAM_XX_DO_ISSUED, 0) + COALESCE(JASHORE_XX_DO_ISSUED, 0) + COALESCE(BOGRA_XX_DO_ISSUED, 0) + COALESCE(DHAMRAI_CBU_XX_DO_ISSUED, 0) + "
               + " COALESCE(DHAMRAI_CKD_XX_RFD, 0) + COALESCE(DHAMRAI_CBU_XX_RFD, 0) + COALESCE(JOYDEBPUR_XX_RFD, 0) + COALESCE(CUMILLA_XX_RFD, 0) + COALESCE(CHATTOGRAM_XX_RFD, 0) + COALESCE(JASHORE_XX_RFD, 0) + COALESCE(BOGRA_XX_RFD, 0) + "
               + " NVL(BUS_BODY_QTY, 0) + NVL(DEALER_PD_QTY, 0) + NVL(FAIR_DEMO_QTY, 0)  + "
               + " NVL(OUTSIDE_BWS_QTY, 0) + NVL(TOTAL_OT_QTY, 0) + NVL(TRANSPORTT_QTY, 0) "
               + "    ) AS TOTAL_QTY "
               + " FROM REPORTDB.IAL_DAILY_STK_RPT_PROS_DATA S "
               + " LEFT JOIN REPORTDB.IAL_LC_STOCK LC ON S.PART_NO=LC.PART_NO "
               + " GROUP BY S.PARTDES, S.PART_NO, S.PRODUCT_FAMILY, S.SEQ, S.MODEL, S.PART_SEGMENT "
               + " ORDER BY S.SEQ ";

                OracleCommand command = GetSQLCommand(StrSql);
                OracleDataAdapter odaAdapter = new OracleDataAdapter(command);
                odaAdapter.Fill(stocklist);
            }

            catch
            {
            }

            finally
            {
                Dispose();
            }

            return stocklist;
        }

        public DataTable GetPartSegment()
        {
            DataTable dt = new DataTable("part_segment");

            try
            {

                OracleCommand com = GetSPCommand("PROC_GET_PART_SEGMENT");
                com.Parameters.Add("PCURSOR", OracleType.Cursor).Direction = ParameterDirection.Output;
                OracleDataAdapter oraData = new OracleDataAdapter(com);

                oraData.Fill(dt);
            }
            catch
            {

            }
            finally
            {
                Dispose();
            }

            return dt;
        }
        public DataTable GetPartLCStock()
        {
            DataTable dt = new DataTable("part_lc_stock");

            try
            {

                OracleCommand com = GetSPCommand("PROC_GET_PART_LC_STOCK");
                com.Parameters.Add("PCURSOR", OracleType.Cursor).Direction = ParameterDirection.Output;
                OracleDataAdapter oraData = new OracleDataAdapter(com);

                oraData.Fill(dt);
            }
            catch
            {

            }
            finally
            {
                Dispose();
            }

            return dt;
        }



        public int savePartLCStock(string[] PART_NO, int[] LC_QTY)
        {
            int i = 0;

            foreach (string part in PART_NO)
            {
                string part_no = PART_NO[i];
                int lc_qty = LC_QTY[i];

                OracleCommand com = GetSPCommand("PROC_LC_STOCK_UPDATE");
                com.Parameters.Add("P_PART_NO", OracleType.VarChar).Value = part_no;
                com.Parameters.Add("P_LC_QTY", OracleType.Number).Value = lc_qty;

                com.ExecuteNonQuery();


                i++;
            }

            return i;

        }
    }
}

