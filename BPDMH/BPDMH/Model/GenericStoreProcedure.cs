using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace BPDMH.Model
{
    public class GenericStoreProcedure
    {
        public GenericStoreProcedure(string connectionString)
        {
            this._connectionString = connectionString;
        }

        /// <summary>
        /// Connection string for the database
        /// </summary>
        private readonly String _connectionString;

        /// <summary>
        /// Calls a stored procedure with a single table as the parameter
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure to call (ie integration.UpsertTestOrderTrackingNum)</param>
        /// <param name="parameterName">Name of the parameter (ie "@TestOrderTrackingNumObjects")</param>
        /// <param name="sprocParamObjects">Parameter for the sproc</param>
        /// <param name="tableParamTypeName">name of the table valued parameter.  (ie. integration.TestOrderTrackingNumTableType)</param>
        /// <param name="connection">The connection to use.  This is optional and is there to allow transactions.</param>
        public void ExecuteTableParamedProcedure<T>(string storedProcedureName, string parameterName, string tableParamTypeName, IEnumerable<T> sprocParamObjects, SqlConnection connection = null)
        {
            // If we don't have a connection, then make one.
            // The reason this is optionally passed in is so we can do a transaction if needed.
            bool connectionCreated = false;
            if (connection == null)
            {
                connection = new SqlConnection(_connectionString);
                connection.Open();
                connectionCreated = true;
            }

            // Create the command that we are going to be sending
            using (var command = connection.CreateCommand())
            {
                command.CommandText = storedProcedureName;
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = command.Parameters.AddWithValue(parameterName, CreateDataTable(sprocParamObjects));
                parameter.SqlDbType = SqlDbType.Structured;
                parameter.TypeName = tableParamTypeName;

                // Call the sproc.
                command.ExecuteNonQuery();
            }

            // if we made the connection then we need to clean it up
            if (connectionCreated)
                connection.Close();
        }

        /// <summary>
        /// Calls a list of sprocs in a transaction.
        /// Example Usage: CallSprocsInTransaction(connection=>model.SprocToCall(paramObjects, connection), connection=>model.Sproc2ToCall(param2Objects, connection...);
        /// </summary>
        /// <param name="sprocsToCall">List of sprocs to call.</param>
        public void CallSprocsInTransaction(params Action<SqlConnection>[] sprocsToCall)
        {
            // Create a new connection that will run the transaction
            using (var connection = new SqlConnection(_connectionString))
            {
                // Create a transaction to wrap our calls in
                var transaction = connection.BeginTransaction();
                try
                {
                    // Call each sproc that was passed in.
                    foreach (var action in sprocsToCall)
                    {
                        // We send the connection to the action so that it will all take place on the same connection.
                        // If we don't then if we do a rollback, the rollback will be for a connection that did not run the sprocs.
                        action(connection);
                    }
                }
                catch (Exception e)
                {
                    // If we failed then roll back.
                    // The idea here is that the caller wants all the sprocs to succeed or none of them.
                    transaction.Rollback();
                    throw;
                }
                // If everything was good, then commit our calls.
                transaction.Commit();
            }
        }



        /// <summary>
        /// Create the data table to be sent up to SQL Server
        /// </summary>
        /// <typeparam name="T">Type of object to be created</typeparam>
        /// <param name="sprocParamObjects">The data to be sent in the table param to SQL Server</param>
        /// <returns></returns>
        private static DataTable CreateDataTable<T>(IEnumerable<T> sprocParamObjects)
        {
            var table = new DataTable();

            var type = typeof (T);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                table.Columns.Add(property.Name, property.PropertyType);
            }

            foreach (var sprocParamObject in sprocParamObjects)
            {
                var propertyValues = new List<object>();
                foreach (PropertyInfo property in properties)
                {
                    propertyValues.Add(property.GetValue(sprocParamObject, null));                  
                }           
                table.Rows.Add(propertyValues.ToArray());

                Console.WriteLine(table);
            }
            return table;
        } 
    }


}