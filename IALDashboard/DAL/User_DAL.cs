using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Configuration;
using IALDashboard.Models;

namespace IALDashboard.DAL
{
    public class User_DAL : BaseDataAccessLayer
    {

        public DataTable VerifyUser(User user)
        {

            DataTable userinfo = new DataTable();

            try
            {
                string userid = user.user_id;
                string password = MD5(user.password);
                string StrSql = " SELECT USER_ID, USER_NAME, USER_PASS, USER_EMAIL, USER_AREA, USER_LEVEL FROM USER_TAB WHERE upper(user_id)=upper('" + userid + "') AND user_pass='" + password + "'";
                OracleCommand command = GetSQLCommand(StrSql);
                OracleDataAdapter odaAdapter = new OracleDataAdapter(command);
                odaAdapter.Fill(userinfo);
            }

            catch
            {
            }

            finally
            {
                Dispose();
            }

            return userinfo;
        }

        public DataTable UserInfo(string userid)
        {

            DataTable userinfo = new DataTable();

            string condition = "";
            if (userid != "") {
                condition = " WHERE user_id='" + userid + "'";
            }

            try
            {

                string StrSql = " SELECT USER_ID, USER_NAME, USER_PASS, USER_EMAIL, USER_AREA, USER_LEVEL FROM USER_TAB "+condition;
                OracleCommand command = GetSQLCommand(StrSql);
                OracleDataAdapter odaAdapter = new OracleDataAdapter(command);
                odaAdapter.Fill(userinfo);
            }

            catch
            {
            }

            finally
            {
                Dispose();
            }

            return userinfo;
        }

        public DataTable UserMenuAccess(string user_id)
        {
            DataTable usermenu = new DataTable();
            try
            {

                string StrSql = @"SELECT a.USER_ID,a.MENU_ID,m.MENU_LABEL,m.MENU_LINK,m.MENU_PARENT,m.SEQ FROM MENU_ACCESS_TAB a left outer join MENU_TAB m on a.MENU_ID=m.MENU_ID WHERE a.user_id='" + user_id + "' order by m.seq";
                OracleCommand command = GetSQLCommand(StrSql);
                OracleDataAdapter odaAdapter = new OracleDataAdapter(command);
                odaAdapter.Fill(usermenu);
            }

            catch
            {
            }

            finally
            {
                Dispose();
            }

            return usermenu;
        }


        public DataTable getMenu(string user_id)
        {
            DataTable usermenu = new DataTable();
            try
            {

                string StrSql = @"SELECT a.USER_ID,m.MENU_ID,m.MENU_LABEL,m.MENU_LINK,m.MENU_PARENT,m.SEQ FROM MENU_TAB m left outer join MENU_ACCESS_TAB a  on a.MENU_ID=m.MENU_ID and a.user_id='" + user_id + "' order by m.seq";
                OracleCommand command = GetSQLCommand(StrSql);
                OracleDataAdapter odaAdapter = new OracleDataAdapter(command);
                odaAdapter.Fill(usermenu);
            }

            catch
            {
            }

            finally
            {
                Dispose();
            }

            return usermenu;

        }

      

        public int user_save(string USER_ID, string USER_NAME, string Password, string USER_EMAIL = "", string USER_AREA="")
        {

            int result = 0;
            string password = MD5(Password);

            try
            {

                string StrSql = "INSERT INTO USER_TAB (USER_ID, USER_NAME, USER_PASS,USER_EMAIL,USER_AREA) VALUES('" + USER_ID + "','" + USER_NAME + "','" + password + "','" + USER_EMAIL + "','" + USER_AREA + "')";
                OracleCommand command =  GetSPCommand(StrSql);
                result = ExecuteCommand(command);

            }
            catch
            {
            }
            finally
            {
                Dispose();
            }
            return result;

        }

        public int user_update(string USER_ID, string USER_NAME, string USER_EMAIL = "", string USER_AREA="")
        {
            int result = 0;
            //string password = MD5(Password);
            try
            {
                string StrSql = "UPDATE USER_TAB SET  USER_NAME='" + USER_NAME + "',USER_EMAIL='" + USER_EMAIL + "', USER_AREA='" + USER_AREA + "' WHERE USER_ID='" + USER_ID + "'";
                OracleCommand command = GetSPCommand(StrSql);
                result = ExecuteCommand(command);
            }
            catch
            {
            }
            finally
            {
                Dispose();
            }
            return result;

        }

        public int menu_access_save(string USER_ID, int MENU_ID)
        {

            int result = 0;

            try
            {

                string StrSql = "INSERT INTO MENU_ACCESS_TAB (USER_ID, MENU_ID) VALUES('"+USER_ID+"',"+MENU_ID+")";
                OracleCommand command = GetSPCommand(StrSql);

                result = ExecuteCommand(command);
            }
            catch
            {
            }
            finally
            {
                Dispose();
            }
            return result;

        }

        public int menu_access_delete(string USER_ID)
        {
            int result = 0;
            try
            {
                string StrSql = "DELETE FROM MENU_ACCESS_TAB WHERE USER_ID='" + USER_ID + "'";
                OracleCommand command = GetSPCommand(StrSql);

                result = ExecuteCommand(command);
            }
            catch
            {
            }
            finally
            {
                Dispose();
            }
            return result;

        }


        public DataTable getDataFromProcedure() {

            DataTable _dt = new DataTable();
            try
            {
                OracleCommand command = GetSPCommand("SP_Daily_Stock_Report");
                command.Parameters.Add("P_PART_NO", OracleType.VarChar).Value = "";
                command.Parameters.Add("P_CONTRACT", OracleType.VarChar).Value = "";
                command.Parameters.Add("P_RECORDSET", OracleType.Cursor).Direction = ParameterDirection.Output;
                OracleDataAdapter odaAdept = new OracleDataAdapter(command);
                odaAdept.Fill((_dt));
            }
            catch
            {

            }
            finally
            {
                Dispose();
            }

            return _dt;

        }


        private string MD5(string Metin)
        {
            MD5CryptoServiceProvider MD5Code = new MD5CryptoServiceProvider();
            byte[] byteDizisi = Encoding.UTF8.GetBytes(Metin);
            byteDizisi = MD5Code.ComputeHash(byteDizisi);
            StringBuilder sb = new StringBuilder();
            foreach (byte ba in byteDizisi)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }
            return sb.ToString();
        }
    }
}