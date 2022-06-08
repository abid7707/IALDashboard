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


                string StrSql = " SELECT * FROM  IAL_STOCK_LOCATION_TEMP";
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