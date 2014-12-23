//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//
//namespace BPDMH.SqlOperation
//{
//    public class SqlOperation
//    {
//		/*
//			This is a struct that can be used as an identifier for a control/variable.
//			The struct is used by CreateCommand. When the struct is passed in, this statement
//			will be created:
//			
//			struct values:
//			paramName = @weDate
//			itemValue = "01/01/2004"
//			
//			sqlCommand would be ---> @weDate='01/01/2004'			
//		*/
//		public struct SIdentifierItem
//		{
//			private object mItemValue;
//			private string mParameterName;
//			private string mReturnType;
//
//			public SIdentifierItem(string parameterName, Object itemValue, string returnType)
//			{
//				mItemValue = new object();
//				mParameterName = parameterName;
//				mItemValue = itemValue;
//				mReturnType = returnType;
//				
//			}
//			public string ParameterName
//			{
//				get
//				{
//					return mParameterName;
//				}
//			}
//
//			public object ItemValue
//			{
//				get
//				{
//					return mItemValue;
//				}
//			}
//			public string ReturnType
//			{
//				get
//				{
//					return mReturnType;
//				}
//			}
//		};
//		
//		#region Private Variables used by COperationsSQL
//
//		public struct SRoleInfo
//		{
//			public int MRoleId;
//			public string MRoleName;
//		};
//
//		private string mConnectionString; //Full Connection String
//		private string mServer; //Corresponding part of the Connection String
//		private string mDatabase; //Corresponding part of the Connection String
//		private string mLoginName; //Corresponding part of the Connection String
//		private string _mPassword; //Corresponding part of the Connection String
//		private int _mSqlUserId; //UserID from sysUsers
//		private SRoleInfo[] _mSqlRoleInfo; //Role info for given UserID
//		private SqlConnection _mSqlConnection;
//		private SqlDataAdapter _mSqlAdapter;
//		private DataSet _mSqlDataSet;
//		private SqlCommand _mSqlCommand;
//		private int _mSqlTimeout;
//		#endregion
//
//		#region Public Methods used by COperationsSQL
//
//		/// <summary>
//		///		COperationsSQL Constructor1: breaks down each component that is required of a Connection String
//		///	for SQLServer.  Parameters are the Server CabangId, Database CabangId, LoginName, and Password.  The userID
//		///	and sRoleInfo for the given login name is also returned.
//		/// </summary>
//		/// <param name="server">CabangId of Server to connect to</param>
//		/// <param name="database">CabangId of Database to connect to</param>
//		/// <param name="loginName">Login CabangId to Connect with</param>
//		/// <param name="password">Login Password to Connect with</param>
//		public SqlOperation(string server, string database, string loginName, string password)
//		{
//			mConnectionString = "server=" + server + ";database=" + database + ";uid=" + loginName + ";pwd=" + password + ";";
//			mServer = server;
//			mDatabase = database;
//			mLoginName = loginName;
//			_mPassword = password;
//			_mSqlTimeout = 30;
//			try
//			{
//				Exception error;
//				if(mLoginName.ToLower() == "sa")
//				{
//					mConnectionString = "";
//					mServer = "";
//					mDatabase = "";
//					mLoginName = "";
//					_mPassword = "";
//					error = new Exception("Login as 'sa' not allowed.");
//					throw(error);
//				}
//				TestConnection();
//				GetUserId(); //Get login id from the sysusers table and put into sqluserID
//				GetRoleInfo();  //Get role info from sysusers table and put into mSqlRoleInfo array
//			}
//			catch(Exception error)
//			{
//				throw(error);
//			}
//		}
//		/// <summary>
//		///		COperationsSQL Constructor2: does not require each component of a Connection String
//		///	for SQLServer. Only valid Connection Strings will work, an exception will be thrown otherwise.  The constructor that breaks
//		///	down each part of the connection string is recommended, but this constructor may also be used
//		///	if more conveniant. The userID and sRoleInfo for the given login name is also returned.
//		/// </summary>
//		/// <param name="connectionStringCoded">Ex: "server=AMS;database=Test;uid=johndoe;pwd=1234;</param>
//		public SqlOperation(string connectionStringCoded)
//		{
//			string delim = ";";
//			string[] param = null;
//
//			//minimal syntax verification checks to see if connection string rnds with a semicolon
//			if(!connectionStringCoded.EndsWith(";"))
//			{
//				Exception error;
//				error = new Exception("Connection string syntax is incorrect.  Syntax should be: server=<srv>;database=<db>;uid=<name;>pwd=<password>;");
//				throw (error);
//			}
//
//			mConnectionString = connectionStringCoded;
//
//			//Chop up connection string to get individual components
//			connectionStringCoded = connectionStringCoded.ToLower();  //SQL is case insensitive so convertion to all lower cas will not affect it
//			connectionStringCoded = connectionStringCoded.Replace("server=","");
//			connectionStringCoded = connectionStringCoded.Replace("database=","");
//			connectionStringCoded = connectionStringCoded.Replace("uid=","");
//			connectionStringCoded = connectionStringCoded.Replace("pwd=","");
//
//
//			//Chop up connection string to get individual components
//			param = connectionStringCoded.Split(delim.ToCharArray(), 4);
//			mServer = param[0];
//			mDatabase = param[1];
//			mLoginName = param[2];
//			//substring is used to get rid of the last semicolon
//            _mPassword = param[3].Substring(0,param[3].Length - 1);
//			_mSqlTimeout = 30;
//						
//					
//			try
//			{
//				Exception error;
//				if(mLoginName.ToLower() == "sa")
//				{
//					mConnectionString = "";
//					mServer = "";
//					mDatabase = "";
//					mLoginName = "";
//					_mPassword = "";
//					error = new Exception("Login as 'sa' not allowed.");
//					throw(error);
//				}
//				TestConnection();
//				GetUserId(); //Get login id from the sysusers table and put into sqluserID
//				GetRoleInfo();  //Get role info from sysusers table and put into mSqlRoleInfo array
//				
//			}
//			catch(Exception error)
//			{
//				throw(error);
//			}
//		}
//
//		/// <summary>
//		///		COperationsSQL Constructor3: Takes the Connectionstring of an existing COperatoinsSQL
//		/// </summary>
//		/// <param name="rhs">Right Hand Side operator, Will be of Class Type COperationsSQL</param>
//		public SqlOperation(SqlOperation rhs)
//		{
//			mConnectionString = rhs.mConnectionString;
//			mServer = rhs.mServer;
//			mDatabase = rhs.mDatabase;
//			mLoginName = rhs.mLoginName;
//			_mSqlTimeout = 30;
//			try
//			{
//				Exception error;
//				if(mLoginName.ToLower() == "sa")
//				{
//					mConnectionString = "";
//					mServer = "";
//					mDatabase = "";
//					mLoginName = "";
//					_mPassword = "";
//					error = new Exception("Login as 'sa' not allowed.");
//					throw(error);
//				}
//				TestConnection();
//				GetUserId(); //Get login id from the sysusers table and put into sqluserID
//				GetRoleInfo();  //Get role info from sysusers table and put into mSqlRoleInfo array
//			}
//			catch(Exception error)
//			{
//				throw(error);
//			}
//		}
//		/// <summary>
//		///	ExcecuteStoredProcedure Method1: Excecutes a stored procedure.  The Stored Procedure name in the first variable,
//		///	second variable that needs to be passed in is the Variables that will be used for the excecution of the Stored Procedure.
//		///	The compliation of these variables is a series of items in an ArralList.  The parameters passed into the arraylist must be in
//		///	the same order as the parameters in the Stored Procedure definition. Also, the parameters passed in must be of the same type
//		///	as the stored procedure.  With the correct type passed in, the sql statment will be
//		///	Types Supported by COperationsSQL:
//		///	SQL			C#
//		///	varchar		string (the ' character will be handled for)
//		///	int			int (WARNING: implicit type conversion on sql side will occur if a numeric type is passed in)
//		///	money		double (WARNING: implicit type conversion on sql side will occur if an int is passed in)
//		///	float		double (WARNING: implicit type conversion on sql side will occur if an int is passed in)
//		///	bit			bool (WARNING: implicit type conversion on sql side will occur if an int is passed in)
//		///	char		char (the ' character will be handled for)
//		///	null		DBNull.Value
//	    /// </summary>
//	    public DataSet ExcecuteStoredProcedure(string storedProcedureName, ArrayList parameters)
//		{
//			_mSqlDataSet = new DataSet();
//			_mSqlCommand = new SqlCommand(CreateCommand(storedProcedureName, parameters), _mSqlConnection);
//			_mSqlCommand.CommandTimeout = _mSqlTimeout;
//			_mSqlAdapter = new SqlDataAdapter(_mSqlCommand);
//			_mSqlAdapter.Fill(_mSqlDataSet);
//			return _mSqlDataSet;
//			
//		}
//
//		/// <summary>
//		///		ExcecuteStoredProcedure Method2: See ExcecuteStoredProcedure Method1 for details.
//		///	Executes a stored Procedure without
//		///	any parameters to be passed in.
//		/// </summary>
//		/// <param name="storedProcedureName">CabangId of SQL Stored Procedure</param>
//		/// <returns></returns>
//		public DataSet ExcecuteStoredProcedure(string storedProcedureName)
//		{
//			_mSqlDataSet = new DataSet();
//			_mSqlCommand = new SqlCommand(storedProcedureName, _mSqlConnection);
//			_mSqlCommand.CommandTimeout = _mSqlTimeout;
//			_mSqlAdapter = new SqlDataAdapter(_mSqlCommand);
//			_mSqlAdapter.Fill(_mSqlDataSet);
//			return _mSqlDataSet;
//		}
//
//		/// <summary>
//		/// ExcecuteStoredProcedure Method2: Executes a stored procedure.
//		/// </summary>
//		/// <param name="storedProcedureName">The name of the procedure to execute</param>
//		/// <param name="parameters">Parameter list containing the arguments in order that will be passed to the procedure</param>
//		/// <returns>Returns a Dataset containing any information returned by the procedure</returns>
//		public DataSet ExcecuteStoredProcedure(string storedProcedureName, params object[] parameters)
//		{
//			var arr = new ArrayList();
//			foreach(var obj in parameters)
//				arr.Add(obj);
//
//			return ExcecuteStoredProcedure(storedProcedureName, arr);
//		}
//
//		/// <summary>
//		/// ExcecuteSelectStatement Method1: Valid select statement is excecuted as if it
//		///	was begin typed in a query analyzer.
//		/// </summary>
//		/// <param name="selectStatement">Select Statement with correct SQL Syntax</param>
//		/// <returns></returns>
//		public DataSet ExcecuteSelectStatement(string selectStatement)
//		{
//			_mSqlDataSet = new DataSet();
//			_mSqlCommand = new SqlCommand(selectStatement, _mSqlConnection);
//			_mSqlCommand.CommandTimeout = _mSqlTimeout;
//			_mSqlAdapter = new SqlDataAdapter(_mSqlCommand);
//			_mSqlAdapter.Fill(_mSqlDataSet);
//			return _mSqlDataSet;
//		}
//
//		/// <summary>
//		/// Executes a select statement that does not return a table
//		/// i.e. a bulk insert statement
//		/// </summary>
//		/// <param name="selectStatement">Select statement to execute</param>
//		/// <returns>Returns the number of rows affected</returns>
//		public int ExecuteNonQuery(string selectStatement)
//		{
//			int nRetVal;
//			_mSqlDataSet = new DataSet();
//			_mSqlCommand = new SqlCommand(selectStatement, _mSqlConnection);
//			_mSqlCommand.CommandTimeout = _mSqlTimeout;
//			_mSqlAdapter = new SqlDataAdapter(_mSqlCommand);
//			_mSqlAdapter.SelectCommand.Connection.Open();
//			
//			nRetVal = _mSqlAdapter.SelectCommand.ExecuteNonQuery();
//			_mSqlAdapter.SelectCommand.Connection.Close();
//
//			return nRetVal;
//		}
//
//		#endregion
//
//		#region Get Functions
//		
//		/*
//			These get pprocedures allow read access to private variables:
//			mSQLConnection and mSQLAdapter (just in case the client ever needs to use them).
//		*/
//		public string SQLConnectionString
//		{
//			get
//			{
//				return mConnectionString;
//			}
//		}
//
//		public SqlConnection SQLConnection
//		{
//			get
//			{
//				return _mSqlConnection;
//			}
//		}
//
//		public SqlDataAdapter SQLDataAdapter
//		{
//			get
//			{
//				return _mSqlAdapter;
//			}
//		}
//
//		public SRoleInfo[] userRoleInfo
//		{
//			get
//			{
//				return _mSqlRoleInfo;
//			}
//		}
//
//		public int uID
//		{
//			get
//			{
//				return _mSqlUserId;
//			}
//		}
//
//		public int TimeOut
//		{
//			get
//			{
//				return _mSqlTimeout;
//			}
//			set
//			{
//				_mSqlTimeout = value;
//			}
//		}
//
//		#endregion
//
//		#region Private Methods used by COperationsSQL
//
//		/*
//			TestConnection Method: tests the SQL Connection to see if its successful.  Used by the constructor, if an exception occurs,
//			the constructor throws that exception to Client.
//		*/ 
//		private void TestConnection()
//		{
//				_mSqlConnection = new SqlConnection(mConnectionString);
//				_mSqlConnection.Open();
//				_mSqlConnection.Close();
//		}
//		/*
//			Connect Method: connects To the database with the given ConnectionString.  Its use is to encapsulate the mSQLConnection.Open and it can
//			be argued that this method is not needed. However, actions can be performed before and after Opening of the
//			connection and the extensibility of this method might be desirable by other classes that inherit COperationsSQL.
//		*/
//		private void Connect()
//		{
//			_mSqlConnection = new SqlConnection(mConnectionString);
//			_mSqlConnection.Open();
//		}
//		/*
//			Disconnect Method: disconnects connection.  Its use is to encapsulate the mSQLConnection.Close and it can
//			be argued that this method is not needed. However, actions can be performed before and after closing of the
//			connection and the extensibility of this method might be desirable by other classes that inherit COperationsSQL. 
//		*/
//		private void Disconnect()
//		{
//			_mSqlConnection.Close();	
//		}
//
//		/*
//			CreateCommand Method: This is a simple chop method that converts the variable passed in by the Arraylist into
//			a valid SQL statement, Tick (') marks are replaced, and ticks are put around varchar and char variables along with
//			commas.
//		*/
//		private string CreateCommand(string storedProcedureName, ArrayList parameters)
//		{	
//			string convertedString = storedProcedureName + " ";
//            if(parameters.Count == 0)
//				return convertedString;		//return just the stored procedure name if the Arraylist does not have any elements
//			foreach(Object x in parameters)
//			{
//				 //Temporary Variables to Compare SqlType To
//                object y = x;
//				var tidentifierItem = new SIdentifierItem("",0,"");
//										
//				//Declare Comparison variable
//				Type SqlType;
//                SqlType = y.GetType();
//				
//				/*
//					Look at struct definition identifierItem for details.
//					
//					Checks to see if the type is of identifierItem, if so then
//					add the parameter name and continue as usual
//				*/
//				if(Object.Equals(SqlType, tidentifierItem.GetType()))
//				{
//
//					SIdentifierItem temp;
//					temp = (SIdentifierItem)x;
//					convertedString += temp.ParameterName + "=";
//					double tDouble = 0;
//					string tString = "";
//					bool tBool = false;
//
//					switch(temp.ReturnType.ToLower())
//					{
//						
//						case "int":
//						case "money":
//						case "decimal":
//							SqlType = tDouble.GetType();
//							break;
//						case "varchar":
//						case "char":
//						case "datetime":
//							SqlType = tString.GetType();
//							break;
//						case "bit":
//							SqlType = tBool.GetType();
//							break;
//						default:
//							throw(new Exception("Unhandled identifierType.rType: " + temp.ReturnType.ToLower()));
//					}
//					y = temp.ItemValue;
//				}
//				
//				switch(SqlType.ToString())
//				{
//
//					case "System.Int16":
//					case "System.Int32":
//					case "System.Int64":
//                    case "System.Double":
//					case "System.Single":
//						convertedString += y.ToString() + ", ";
//						break;
//					case "System.String":
//					case "System.Char":
//						if(y.ToString().ToLower().Trim() == "null")
//							convertedString += "null, ";
//						else
//							convertedString += "'" + y.ToString().Replace("'","''") + "', ";
//						break;
//					case "System.Boolean":
//						if(Convert.ToBoolean(y) == false)
//						{
//							convertedString += "0" + ", ";
//						}
//						else
//						{
//							convertedString += "1" + ", ";
//						}
//						break;
//					case "System.DBNull":
//						convertedString += "null, ";
//						break;
//					default:
//						throw(new Exception("Unhandled variable type: " + SqlType.ToString()));
//				}
//			}
//			return convertedString.Substring(0, convertedString.Length - 2);	//Substring is used to get rid of the last comma and space
//		}
//
//		/*
//			getUserID Method:  Gets the user id from the sysusers table where the CabangId equals
//			the log in name that was passed in.  Example: select uid from sysusers where name = 'mickeymouse'
//		*/
//		private void GetUserId()
//		{
//			DataSet temp;
//			temp = ExcecuteSelectStatement("select uid from sysusers where name = '" + mLoginName + "'");
//			//Checks to see if more than one row gets returned when the user name is passed in.  This should never error unless the select statement gets changed.
//			if(temp.Tables[0].Rows.Count > 1)
//			{
//				Exception error;
//				error = new Exception("getUserID returned more than one row.  Method should not return more than one row.");
//				throw (error);
//			}
//
//			//set the userid that is returned by the select statement
//			_mSqlUserId = Convert.ToInt32(temp.Tables[0].Rows[0]["uid"]);
//			
//		}
//
//		private void GetRoleInfo()
//		{
//			int count = 0;
//			DataSet temp = new DataSet();
//			temp = ExcecuteSelectStatement("select uid, name from sysusers where uid in (select groupuid from sysmembers where memberuid = " + _mSqlUserId + ")");
//			_mSqlRoleInfo = new SRoleInfo[temp.Tables[0].Rows.Count];
//			for(count = 0; count < _mSqlRoleInfo.Length; count++)
//			{
//				_mSqlRoleInfo[count].MRoleId = Convert.ToInt32(temp.Tables[0].Rows[count]["uid"]);
//				_mSqlRoleInfo[count].MRoleName = Convert.ToString(temp.Tables[0].Rows[count]["name"]);
//			}
//		}
//		#endregion
//
//        public void UpsertMethod(List<ITypeThatHasTheSamePropertiesAsTheTbleType> rows,
//                         SqlConnection connection = null)
//        {
//            ExecuteTableParamedProcedure("schema.UpsertSproc", "@UpsertParam",
//                                         "schema.UpsertTableType", rows, connection);
//        }
//	}
//
//
//}