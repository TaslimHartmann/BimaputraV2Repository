using System;
using System.Data;
using System.Data.SqlClient;

namespace BPDMH.Model
{
    public class DataAccess : IDisposable
    {

        private SqlCommand _cmd;
        private string _SqlConnString;
        
        public DataAccess(string ConnectionString)
        {
            _cmd = new SqlCommand();
            _cmd.CommandTimeout = 240;
            _SqlConnString = ConnectionString;
        }

        #region IDisposable implementation

        /// <summary>
        /// Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
        /// </summary>
        ~DataAccess()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);            
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cmd.Connection.Dispose();
                _cmd.Dispose();
            }
        }

        #endregion

        #region data retrieval methods

        public DataTable ExecReturnDataTable()
        {
            using (var conn = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    PrepareCommandForExecution(conn);
                    using (SqlDataAdapter adap = new SqlDataAdapter(_cmd))
                    {
                        DataTable dt = new DataTable();
                        adap.Fill(dt);
                        return dt;
                    }
                }
                finally
                {
                    _cmd.Connection.Close();
                }
            }
        }                

        public object ExecScalar()
        {
            using (var conn = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    PrepareCommandForExecution(conn);
                    return _cmd.ExecuteScalar();
                }
                finally
                {
                    _cmd.Connection.Close();
                }
            }
        }    

        #endregion

        #region data insert and update methods

        public void ExecNonQuery()
        {
            using (var conn = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    PrepareCommandForExecution(conn);
                    _cmd.ExecuteNonQuery();
                }
                finally
                {
                    _cmd.Connection.Close();
                }
            }
        }

        #endregion

        #region helper methods

        public void AddParm(string ParameterName, SqlDbType ParameterType, object Value)
        { _cmd.Parameters.Add(ParameterName, ParameterType).Value = Value; }

        private SqlCommand PrepareCommandForExecution(SqlConnection conn)
        {
            try
            {
                _cmd.Connection = conn;
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandTimeout = this.CommandTimeout;
                _cmd.Connection.Open();

                return _cmd;
            }
            finally
            {
                _cmd.Connection.Close();
            }
        }

        #endregion

        #region properties

        public int CommandTimeout
        {
            get { return _cmd.CommandTimeout; }
            set { _cmd.CommandTimeout = value; }
        }

        public string ProcedureName
        {
            get { return _cmd.CommandText; }
            set { _cmd.CommandText = value; }
        }

        public string ConnectionString
        {
            get { return _SqlConnString; }
            set { _SqlConnString = value; }
        }

        #endregion

        public void UpdateWorkOrder(int workOrderID, int paymentTermTypeID, string acceptedBy, string lastIssuedBy)
        {
            using (var data = new DataAccess(this.ConnectionString))
            {
                data.ProcedureName = "UpdateWorkOrderDetails";
                data.AddParm("@WorkOrderID", SqlDbType.Int, workOrderID);
                data.AddParm("@PaymentTermTypeID", SqlDbType.Int, paymentTermTypeID);
                data.AddParm("@AcceptedBy", SqlDbType.VarChar, acceptedBy);
                data.AddParm("@LastIssuedBy", SqlDbType.VarChar, lastIssuedBy);
                data.ExecNonQuery();
            }
        }

        public DataTable GetWorkOrder(int workOrderID)
        {
            using (var data = new DataAccess(this.ConnectionString))
            {
                data.ProcedureName = "GetWorkOrder";
                data.AddParm("@WorkOrderID", SqlDbType.Int, workOrderID);
                return data.ExecReturnDataTable();
            }
        }
    }
}