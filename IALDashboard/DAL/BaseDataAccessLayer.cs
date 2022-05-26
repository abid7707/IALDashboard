using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Data;

namespace IALDashboard.DAL
{
    public class BaseDataAccessLayer
    {

        // Change Date: 10/01/2015 BY Habib
        private string ConnectionString = ConfigurationManager.ConnectionStrings["ReportDBConn"].ToString();
        //private string ConnectionString = WindowsRegistry.DecryptEncryptedConnectionString();

        //private string ConnectionString = ClsConnection.Oracle_HR_Connection_String;
        public OracleConnection _DBConnection;
        public OracleTransaction _Transaction;
        private const string ReturnValue = "Return_Value";

        public BaseDataAccessLayer()
        {
            _DBConnection = new OracleConnection(ConnectionString);

        }
        public bool TestDBConnection()
        {
            try
            {
                OracleConnection connection = new OracleConnection();
                connection.ConnectionString = ConnectionString;
                connection.Open();
                connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        protected int ExecuteCommandByManualTransction(OracleCommand command)
        {


            int ReturnValue = 0;
            try
            {
                ReturnValue = command.ExecuteNonQuery();
            }
            catch (Exception E)
            {
                E.Message.ToString();

                string[] strMsg = { };

                strMsg = E.Message.ToString().Split(':');
                if (strMsg[0] == "ORA-02292")
                    ReturnValue = 1;
            }
            return ReturnValue;
        }

        protected int ExecuteCommandByManualTransctionWithoutCatch(OracleCommand command)
        {
            int ReturnValue = 0;
            ReturnValue = command.ExecuteNonQuery();
            return ReturnValue;
        }

        public BaseDataAccessLayer(OracleTransaction transaction)
        {
            _DBConnection = transaction.Connection;
            _Transaction = transaction;
        }
        public void SetTransection(OracleTransaction transaction)
        {
            _DBConnection = transaction.Connection;
            _Transaction = transaction;
        }

        /// <summary>
        /// Database Connection Object
        /// </summary>
        protected OracleConnection DBConnection
        {
            get
            {
                if (_DBConnection == null)
                {
                    _DBConnection = new OracleConnection(ConnectionString);
                }

                return _DBConnection;
            }
        }

        public OracleTransaction BeginTransaction()
        {
            return _DBConnection.BeginTransaction();
        }

        /// <summary>
        /// to open connection
        /// </summary>
        protected void OpenConnection()
        {
            if (DBConnection.State != ConnectionState.Open)
                DBConnection.Open();
        }

        /// <summary>
        /// to close connection
        /// </summary>
        protected void CloseConnection()
        {
            if (DBConnection != null)
                if (DBConnection.State != ConnectionState.Closed)
                    if (_Transaction == null)
                        DBConnection.Close();

        }

        protected OracleDataReader GetDataReader(OracleCommand command)
        {
            OpenConnection();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }


        protected OracleDataReader GetSingleRow(OracleCommand command)
        {
            OpenConnection();
            return command.ExecuteReader(CommandBehavior.SingleRow | CommandBehavior.CloseConnection);
        }

        protected object SelectScaler(OracleCommand command)
        {
            OpenConnection();
            return command.ExecuteScalar();
        }

        protected Int64 SelectRecords(OracleCommand command, out OracleDataReader reader)
        {
            AddReturnParameter(command);
            reader = GetDataReader(command);
            return GetReturnParameter(command);
        }


        protected Int64 SelectRecord(OracleCommand command, out OracleDataReader reader)
        {
            AddReturnParameter(command);
            reader = GetSingleRow(command);
            return GetReturnParameter(command);
        }

        protected Int64 InsertRecord(OracleCommand command)
        {
            AddReturnParameter(command);
            ExecuteCommand(command);
            return GetReturnParameter(command);
        }

        protected int InsertRecordWithOutReturn(OracleCommand command)
        {
            // AddReturnParameter(command);
            return ExecuteCommand(command);

        }
        protected Int64 UpdateRecord(OracleCommand command)
        {
            AddReturnParameter(command);
            ExecuteCommand(command);
            return GetReturnParameter(command);
        }
        protected int UpdateRecordWithOutReturn(OracleCommand command)
        {
            return ExecuteCommand(command);
        }
        protected Int64 DeleteRecord(OracleCommand command)
        {
            AddReturnParameter(command);
            ExecuteCommand(command);
            return GetReturnParameter(command);
        }


        protected int ExecuteCommand(OracleCommand command)
        {

            int ReturnValue = 0;
            try
            {
                ReturnValue = command.ExecuteNonQuery();
                _Transaction.Commit();
                if (ReturnValue == 1)
                {
                    ReturnValue = -1;

                }
            }
            catch (Exception E)
            {
                //E.Message.ToString();
                //Trans.Rollback();

                string[] strMsg = { };

                strMsg = E.Message.ToString().Split(':');
                if (strMsg[0] == "ORA-02292")
                    ReturnValue = 1;

                _Transaction.Rollback();
            }

            CloseConnection();
            OracleConnection.ClearAllPools();


            return ReturnValue;
        }

        protected void AddReturnParameter(OracleCommand command)
        {
            OracleParameter param = new OracleParameter(ReturnValue, OracleType.Number);
            param.Direction = ParameterDirection.ReturnValue;
            command.Parameters.Add(param);
        }

        protected Int64 GetReturnParameter(OracleCommand command)
        {
            if (command.Parameters[ReturnValue] != null)
            {
                if (command.Parameters[ReturnValue].Value != null)
                    return Convert.ToInt64(command.Parameters[ReturnValue].Value);
                else
                    return 0;
            }
            else
                return 0;
        }

        protected object GetOutParameter(OracleCommand command, string paramName)
        {
            OracleParameter idParameter = command.Parameters[paramName];
            if (idParameter != null)
                return (object)idParameter.Value;
            else
                return 0;
        }


        protected System.Data.DataSet GetDataSet(OracleCommand command)
        {
            return GetDataSet(command, "Default");
        }

        public System.Data.DataSet GetDataSet(OracleCommand command, string tablename)
        {
            System.Data.DataSet dsdataset = new System.Data.DataSet();
            dsdataset.Tables.Add(new DataTable(tablename));

            OracleDataAdapter dataadapter = new OracleDataAdapter(command);
            dataadapter.Fill(dsdataset, tablename);

            return dsdataset;
        }
        public System.Data.DataSet GetDataTable(OracleCommand command, System.Data.DataSet dataset, string tablename)
        {
            dataset.Tables.Add(new DataTable(tablename));
            OracleDataAdapter dataadapter = new OracleDataAdapter(command);
            dataadapter.Fill(dataset, tablename);

            return dataset;
        }
        public OracleCommand GetSPCommand(string StoredProcedureName)
        {
            OracleCommand command;
            if (_Transaction == null)
            {
                OpenConnection();
                _Transaction = BeginTransaction();
                command = new OracleCommand(StoredProcedureName, DBConnection, _Transaction);
            }
            else
                command = new OracleCommand(StoredProcedureName, DBConnection, _Transaction);

            command.CommandType = CommandType.StoredProcedure;

            return command;
        }


        protected OracleCommand GetSQLCommand(string QueryString)
        {
            OracleCommand command;
            if (_Transaction == null)
                command = new OracleCommand(QueryString, DBConnection);
            else
                command = new OracleCommand(QueryString, _Transaction.Connection);

            command.CommandType = CommandType.Text;

            return command;
        }


        protected OracleCommand GetTableCommand(string TableName)
        {
            OracleCommand command;
            if (_Transaction == null)
                command = new OracleCommand(TableName, DBConnection);
            else
                command = new OracleCommand(TableName, _Transaction.Connection);

            command.CommandType = CommandType.TableDirect;

            return command;
        }

        public void AddParameters(OracleCommand command, params OracleParameter[] Parameters)
        {
            foreach (OracleParameter param in Parameters)
            {
                command.Parameters.Add(param);
            }
        }


        protected OracleParameter pInt(string paramName, int value)
        {
            return pInt(paramName, value, ParameterDirection.Input);
        }

        protected OracleParameter pInt(string paramName, int value, ParameterDirection direction)
        {
            OracleParameter param = new OracleParameter(paramName, OracleType.Int32);
            param.Value = value;
            param.Direction = direction;
            return param;
        }

        //protected OracleParameter pInt(string paramName, int[] value, int p_Size)
        //{
        //    return pInt(paramName, value, ParameterDirection.Input, p_Size);
        //}

        //protected OracleParameter pInt(string paramName, int[] value, ParameterDirection direction, int p_Size)
        //{
        //    OracleParameter param = new OracleParameter(paramName, OracleType.Int32);
        //    param.Value = value;
        //    param.Direction = direction;
        //    param.Size = p_Size;
        //    param.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
        //    return param;
        //}

        protected OracleParameter pIntOut(string paramName)
        {
            OracleParameter param = new OracleParameter(paramName, OracleType.Number);
            param.Direction = ParameterDirection.Output;
            return param;
        }

        protected OracleParameter pMoney(string paramName, double value)
        {
            return pMoney(paramName, value, ParameterDirection.Input);
        }
        protected OracleParameter pMoney(string paramName, double value, ParameterDirection direction)
        {
            OracleParameter param = new OracleParameter(paramName, OracleType.Double);
            param.Value = value;
            param.Direction = direction;
            return param;
        }

        //protected OracleParameter pMoney(string paramName, double[] value, int p_Size)
        //{
        //    return pMoney(paramName, value, ParameterDirection.Input, p_Size);
        //}
        //protected OracleParameter pMoney(string paramName, double[] value, ParameterDirection direction, int p_Size)
        //{
        //    OracleParameter param = new OracleParameter(paramName, OracleType.Double);
        //    param.Value = value;
        //    param.Direction = direction;
        //    param.Size = p_Size;
        //    param.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
        //    return param;
        //}
        protected OracleParameter pMoneyOut(string paramName)
        {
            return pMoney(paramName, double.MinValue, ParameterDirection.Output);
        }




        protected OracleParameter pLong(string paramName, long value)
        {
            return pLong(paramName, value, ParameterDirection.Input);
        }
        protected OracleParameter pLong(string paramName, long value, ParameterDirection direction)
        {
            OracleParameter param = new OracleParameter(paramName, OracleType.Number);
            param.Value = value;
            param.Direction = direction;
            return param;
        }

        public OracleParameter pVarChar(string paramName, string value)
        {
            return pVarChar(paramName, value, ParameterDirection.Input);
        }
        protected OracleParameter pVarCharOut(string paramName, int length)
        {
            OracleParameter param = new OracleParameter(paramName, OracleType.VarChar, length);
            param.Direction = ParameterDirection.Output;
            return param;

        }

        protected OracleParameter pVarChar(string paramName, string value, ParameterDirection direction)
        {
            OracleParameter param = new OracleParameter(paramName, OracleType.VarChar);
            param.Value = value;
            param.Direction = direction;
            return param;
        }

        //public OracleParameter pVarChar(string paramName, string[] value, int p_size)
        //{
        //    return pVarChar(paramName, value, ParameterDirection.Input, p_size);
        //}
        //protected OracleParameter pVarChar(string paramName, string[] value, ParameterDirection direction, int p_size)
        //{
        //    OracleParameter param = new OracleParameter(paramName, OracleType.Varchar2);
        //    param.Value = value;
        //    param.Direction = direction;
        //    param.Size = p_size;
        //    param.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
        //    return param;
        //}


        protected OracleParameter pNVarChar(string paramName, String value)
        {
            return pNVarChar(paramName, value, ParameterDirection.Input);
        }



        protected OracleParameter pNVarChar(string paramName, String value, ParameterDirection direction)
        {
            OracleParameter param = new OracleParameter(paramName, OracleType.NVarChar);
            param.Value = value;
            param.Direction = direction;
            return param;
        }


        //protected OracleParameter pNVarChar(string paramName, string[] value, int p_size)
        //{
        //    return pNVarChar(paramName, value, ParameterDirection.Input, p_size);
        //}



        //protected OracleParameter pNVarChar(string paramName, string[] value, ParameterDirection direction, int p_size)
        //{
        //    OracleParameter param = new OracleParameter(paramName, OracleType.NVarchar2);
        //    param.Value = value;
        //    param.Direction = direction;
        //    param.Size = p_size;
        //    param.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
        //    return param;
        //}

        protected OracleParameter pNVarCharOut(string paramName)
        {
            return pNVarChar(paramName, String.Empty, ParameterDirection.Output);
        }
        protected OracleParameter pDouble(string paramName, double value)
        {
            return pDouble(paramName, value, ParameterDirection.Input);
        }

        protected OracleParameter pFloatOut(string paramName)
        {
            return pDouble(paramName, float.MinValue, ParameterDirection.Output);
        }

        protected OracleParameter pDouble(string paramName, double value, ParameterDirection direction)
        {
            OracleParameter param = new OracleParameter(paramName, OracleType.Double);
            param.Value = value;
            param.Direction = direction;
            return param;
        }

        protected OracleParameter pDecimal(string paramName, Decimal value)
        {
            return pDecimal(paramName, value, ParameterDirection.Input);
        }

        protected OracleParameter pDecimalOut(string paramName)
        {
            return pDecimal(paramName, decimal.MinValue, ParameterDirection.Output);
        }

        protected OracleParameter pDecimal(string paramName, Decimal value, ParameterDirection direction)
        {
            OracleParameter param = new OracleParameter(paramName, OracleType.Number);
            param.Value = value;
            param.Direction = direction;
            return param;
        }



        protected OracleParameter pDateTime(string paramName, DateTime value)
        {
            return pDateTime(paramName, value, ParameterDirection.Input);
        }
        protected OracleParameter pDateTime(string paramName, DateTime value, ParameterDirection direction)
        {
            OracleParameter param = new OracleParameter(paramName, OracleType.Timestamp);
            param.Value = value;
            param.Direction = direction;
            return param;
        }
        //protected OracleParameter pDateTime(string paramName, DateTime[] value, int p_size)
        //{
        //    return pDateTime(paramName, value, ParameterDirection.Input, p_size);
        //}
        //protected OracleParameter pDateTime(string paramName, DateTime[] value, ParameterDirection direction, int p_size)
        //{
        //    OracleParameter param = new OracleParameter(paramName, OracleType.Date);
        //    param.Value = value;
        //    param.Direction = direction;
        //    param.Size = p_size;
        //    param.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
        //    return param;
        //}
        protected OracleParameter pDateTimeOut(string paramName)
        {
            return pDateTime(paramName, DateTime.MinValue, ParameterDirection.Output);
        }



        protected OracleParameter pImage(string paramName, byte[] value, ParameterDirection direction)
        {
            OracleParameter param = new OracleParameter(paramName, OracleType.Blob);
            param.Value = value;
            param.Direction = direction;
            return param;

        }

        protected OracleParameter pImage(string paramName, byte[] value)
        {
            return pImage(paramName, value, ParameterDirection.Input);
        }

        protected OracleParameter pByte(string paramName, byte[] value, ParameterDirection direction)
        {
            OracleParameter param = new OracleParameter(paramName, OracleType.Byte);
            param.Value = value;
            param.Direction = direction;
            return param;
        }

        protected OracleParameter pByte(string paramName, byte[] value)
        {
            return pByte(paramName, value, ParameterDirection.Input);
            //OracleParameter param = new OracleParameter (   paramName, value);
            //return param;
        }



        protected OracleParameter pRefCur(string paramName)
        {
            OracleParameter param = new OracleParameter(paramName, OracleType.Cursor);
            param.Direction = ParameterDirection.Output;
            return param;
        }



        public void Dispose()
        {
            CloseConnection();
            //			if( _Transaction == null )
            DBConnection.Dispose();
            //			GC.Collect();
        }

    }

}