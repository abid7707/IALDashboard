using System.Data;
using System.Data.OracleClient;

namespace IALDashboard.DAL
{
    public class Collection_DAL : BaseDataAccessLayer
    {

        public DataTable CollectionReport(string from_date)
        {

            DataTable dt = new DataTable("CollectionReport");

            try
            {
                OracleCommand com = GetSPCommand("PROC_COLLECTION_REPORT");
                com.Parameters.Add("P_FROM_DATE", OracleType.VarChar).Value = from_date;
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


        public DataTable ROSheet(string from_date, string ro_code, string zone_name)
        {

            DataTable dt = new DataTable("ROSheet");

            try
            {

                OracleCommand com = GetSPCommand("PROC_RO_SHEET");
                com.Parameters.Add("P_FROM_DATE", OracleType.VarChar).Value = from_date;
                com.Parameters.Add("P_RO_CODE", OracleType.VarChar).Value = ro_code;
                com.Parameters.Add("P_ZONE_NAME", OracleType.VarChar).Value = zone_name;
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

        public DataTable ROListByZone(string from_date, string zone_name)
        {

            DataTable dt = new DataTable("ROSheet");

            try
            {

                OracleCommand com = GetSPCommand("PROC_RO_BY_ZONE");
                com.Parameters.Add("P_FROM_DATE", OracleType.VarChar).Value = from_date;
                com.Parameters.Add("P_ZONE_NAME", OracleType.VarChar).Value = zone_name;
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


        public DataTable ROSummary(string from_date, string zone_name)
        {

            DataTable dt = new DataTable("ROSheet");

            try
            {

                OracleCommand com = GetSPCommand("PROC_RO_SUMMARY");
                com.Parameters.Add("P_FROM_DATE", OracleType.VarChar).Value = from_date;
                com.Parameters.Add("P_ZONE_NAME", OracleType.VarChar).Value = zone_name;
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

        public DataTable RoZoneWiseSummary(string from_date)
        {

            DataTable dt = new DataTable("ROZoneWiseSummary");

            try
            {

                OracleCommand com = GetSPCommand("PROC_RO_ZONEWISE_SUMMARY");
                com.Parameters.Add("P_FROM_DATE", OracleType.VarChar).Value = from_date;
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




        public DataTable ROInfo(string zone_name)
        {
            DataTable dt = new DataTable("rolist");

            try
            {

                OracleCommand com = GetSPCommand("PROC_GET_RO");
                com.Parameters.Add("P_ZONE_NAME", OracleType.VarChar).Value = zone_name;
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

        public DataTable getROByCODE(string ro_code)
        {
            DataTable dt = new DataTable("ro");

            try
            {

                OracleCommand com = GetSPCommand("PROC_GET_RO_BY_CODE");
                com.Parameters.Add("P_RO_CODE", OracleType.VarChar).Value = ro_code;
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

        public DataTable ZoneInfo()
        {
            DataTable dt = new DataTable("zonelist");

            try
            {

                OracleCommand com = GetSPCommand("PROC_GET_ZONE");
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