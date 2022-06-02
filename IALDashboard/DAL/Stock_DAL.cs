using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OracleClient;

namespace IALDashboard.DAL
{
    public class Stock_DAL: BaseDataAccessLayer
    {

        public DataTable DailyStockReport() {

            DataTable dt = new DataTable();

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
            finally {
                Dispose();
            }

            return dt;

        
        }
    }
}