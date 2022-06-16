using System.Data;
using System.Data.OracleClient;

namespace IALDashboard.DAL
{
    public class Collection_DAL : BaseDataAccessLayer
    {
        public DataTable CollectionReport()
        {

            DataTable dt = new DataTable("CollectionReport");

            try
            {

                OracleCommand com = GetSPCommand("PROC_COLLECTION_REPORT");
                com.Parameters.Add("P_FROM_DATE", OracleType.VarChar).Value = "1-5-2022";
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
    }
}