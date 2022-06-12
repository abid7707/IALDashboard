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

            DataTable stocklist = new DataTable();



            try
            {


                string StrSql = " SELECT "
                + " PARTDES, PART_NO, PRODUCT_FAMILY, "
                + " BUS_BODY_QTY, DEALER_PD_QTY, FAIR_DEMO_QTY, "
                + " LC_QTY, OUTSIDE_BWS_QTY, TOTAL_OT_QTY, "
                + " TRANSPORTT_QTY, DHAMRAI_CKD#RFD_CKD, DHAMRAI_CBU#RFD_CBU, "
                + " DHAMRAI_CKD#DO_ISSUED, DHAMRAI_CKD#BOOKED_DAP, DHAMRAI_CKD#DPD, "
                + " DHAMRAI_CKD#FAIR_AND_DEMO, DHAMRAI_CKD#OBW, DHAMRAI_CKD#BUS_BODY, "
                + " DHAMRAI_CBU#PDI_PTS, DHAMRAI_CKD#TOTAL_OT, JOYDEBPUR#RFD, "
                + " JOYDEBPUR#DO_ISSUED, JOYDEBPUR#BOOKED, JOYDEBPUR#TOTAL_OT, "
                + " JOYDEBPUR#FAIR_AND_DEMO, JOYDEBPUR#PDI_PTS, JOYDEBPUR#DPD, "
                + " JOYDEBPUR#OBW, CUMILLA#RFD, CUMILLA#DO_ISSUED, "
                + " CUMILLA#BOOKED, CUMILLA#FAIR_AND_DEMO, BOGRA#DPD, "
                + " CUMILLA#OBW, CUMILLA#PDI_PTS, CUMILLA#OSC, "
                + " CTG#RFD, CTG#DO_ISSUED, CTG#BOOKED, "
                + " CTG#FAIR_AND_DEMO, CTG#PDI_PTS, CTG#DPD, "
                + " CTG#TOTAL_OT, JASHORE#RFD, JASHORE#DO_ISSUED, "
                + " JASHORE#BOOKED, JASHORE#PDI_PTS, JASHORE#FAIR_AND_DEMO, "
                + " JASHORE#DPD, JASHORE#OBW, JASHORE#TOTAL_OT, "
                + " BOGRA#RFD, BOGRA#DO_ISSUED, BOGRA#BOOKED, "
                + " BOGRA#FAIR_AND_DEMO, BOGRA#OBW, BOGRA#PDI_PTS, "
                + " BOGRA#TOTAL_OT, DHAMRAI_CKD#PDI_PTS, DHAMRAI_CBU#DO_ISSUED, "
                + " DHAMRAI_CBU#BOOKED_DAP, "
                + " (COALESCE(DHAMRAI_CBU#PDI_PTS ,0) + COALESCE(JOYDEBPUR#PDI_PTS ,0) + COALESCE(CUMILLA#PDI_PTS ,0) + COALESCE(CTG#PDI_PTS ,0) + COALESCE(JASHORE#PDI_PTS ,0) + COALESCE(BOGRA#PDI_PTS ,0) + COALESCE(DHAMRAI_CKD#PDI_PTS ,0)) AS PDI_PTS, "
                + " (COALESCE(DHAMRAI_CKD#BOOKED_DAP ,0) + COALESCE(JOYDEBPUR#BOOKED ,0) + COALESCE(CUMILLA#BOOKED ,0) + COALESCE(CTG#BOOKED ,0) + COALESCE(JASHORE#BOOKED ,0) + COALESCE(BOGRA#BOOKED ,0) + COALESCE(DHAMRAI_CBU#BOOKED_DAP ,0)) AS BOOKED, "
                + " (COALESCE(DHAMRAI_CKD#DO_ISSUED, 0) + COALESCE(JOYDEBPUR#DO_ISSUED, 0) + COALESCE(CUMILLA#DO_ISSUED, 0) + COALESCE(CTG#DO_ISSUED, 0) + COALESCE(JASHORE#DO_ISSUED, 0) + COALESCE(BOGRA#DO_ISSUED, 0) + COALESCE(DHAMRAI_CBU#DO_ISSUED, 0)) AS DO_ISSUED, "
                + " (COALESCE(DHAMRAI_CKD#RFD_CKD, 0) + COALESCE(DHAMRAI_CBU#RFD_CBU,0) + COALESCE(JOYDEBPUR#RFD, 0) + COALESCE(CUMILLA#RFD, 0) + COALESCE(CTG#RFD, 0) + COALESCE(JASHORE#RFD,0) + COALESCE(BOGRA#RFD,0)) AS RFD, "
                + " (COALESCE(DHAMRAI_CBU#PDI_PTS ,0) + COALESCE(JOYDEBPUR#PDI_PTS ,0) + COALESCE(CUMILLA#PDI_PTS ,0) + COALESCE(CTG#PDI_PTS ,0) + COALESCE(JASHORE#PDI_PTS ,0) + COALESCE(BOGRA#PDI_PTS ,0) + COALESCE(DHAMRAI_CKD#PDI_PTS ,0) +"
                + " COALESCE(DHAMRAI_CKD#BOOKED_DAP ,0) + COALESCE(JOYDEBPUR#BOOKED ,0) + COALESCE(CUMILLA#BOOKED ,0) + COALESCE(CTG#BOOKED ,0) + COALESCE(JASHORE#BOOKED ,0) + COALESCE(BOGRA#BOOKED ,0) + COALESCE(DHAMRAI_CBU#BOOKED_DAP ,0) +"
                + " COALESCE(DHAMRAI_CKD#DO_ISSUED, 0) + COALESCE(JOYDEBPUR#DO_ISSUED, 0) + COALESCE(CUMILLA#DO_ISSUED, 0) + COALESCE(CTG#DO_ISSUED, 0) + COALESCE(JASHORE#DO_ISSUED, 0) + COALESCE(BOGRA#DO_ISSUED, 0) + COALESCE(DHAMRAI_CBU#DO_ISSUED, 0) +"
                + " COALESCE(DHAMRAI_CKD#RFD_CKD, 0) + COALESCE(DHAMRAI_CBU#RFD_CBU,0) + COALESCE(JOYDEBPUR#RFD, 0) + COALESCE(CUMILLA#RFD, 0) + COALESCE(CTG#RFD, 0) + COALESCE(JASHORE#RFD,0) + COALESCE(BOGRA#RFD,0)) AS TOTAL_QTY"
                + " FROM REPORTDB.IAL_STOCK_LOCATION_TEMP";




                //string StrSql = " SELECT * FROM  IAL_STOCK_LOCATION_TEMP";
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

    }
}

