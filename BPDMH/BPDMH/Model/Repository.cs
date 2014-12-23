//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Windows.Forms;
//using BPDMH.Interfaces;
//
//namespace BPDMH.Model
//{
//    public class Repository<T> : IRepository<T> where T : class
//    {
//        private readonly IColumnNames _iColumnNames;
//        
//        //DataContext _db;
//        public Repository(IColumnNames iColumnNames)
//        {
//            this._iColumnNames = iColumnNames;
//            //_db = new DataContext("Database string connection");
//            //_db.DeferredLoadingEnabled = false;
//        }
//
//        public Repository()
//        {
//            
//        }
//
//        public void Add(T entity, string[] param, string sqlSyntax)
//        {
//            string colNames = null;
//            foreach (var columnName in param)
//                colNames += columnName +",";
//            
//            if (colNames != null)
//            {
//                var finala = colNames;
//            }
//
////            SqlConnection conn = null;
////            const string connectionString = "Data Source=localhost\\sqlexpress;Initial Catalog=BPDMH;User ID=thartmann;Password=th1478"; 
////            SqlDataReader rdr = null;
////
////            try
////            {
////                // create and open a connection object
////                conn = new
////                    SqlConnection(connectionString);
////                conn.Open();
////
////                // 1. create a command object identifying
////                // the stored procedure
////                SqlCommand cmd = new SqlCommand(
////                    "KendaraanCRUD", conn);
////
////                // 2. set the command object so it knows
////                // to execute a stored procedure
////                cmd.CommandType = CommandType.StoredProcedure;
////
////                // 3. add parameter to command, which
////                // will be passed to the stored procedure
////                cmd.Parameters.Add(new SqlParameter("@KdKnd", "01"));
////                cmd.Parameters.Add(new SqlParameter("@NoPolisi", "B3190JKT"));
////                cmd.Parameters.Add(new SqlParameter("@Jenis", "Truk"));
////                cmd.Parameters.Add(new SqlParameter("@Keterangan", "TEST"));
////                cmd.Parameters.Add(new SqlParameter("@StatementType", "INSERT"));
////
////                // execute the command
////                rdr = cmd.ExecuteReader();
////
////                // iterate through results, printing each to console
////                while (rdr.Read())
////                {
////                    Console.WriteLine(
////                        "Product: {0,-35} Total: {1,2}",
////                        rdr["KdKendaraan"],
////                        rdr["NoPolisi"]);
////                }
////            }
////            finally
////            {
////                if (conn != null)
////                {
////                    conn.Close();
////                }
////                if (rdr != null)
////                {
////                    rdr.Close();
////                }
////            }
//            insert();
//        }
//
//        private void insert()
//        {
//            //This is where the data/table will be 
//            //stored for the returned person
//            var myDataSet = new DataSet();
//            //the name of the stored procedure to run
//            string saveSp = "KendaraanCRUD";
//            //the name of the stored procedure to run
//            string getSp = "KendaraanCRUD";
//            string firstName = "kd12"; //parameters
//            string lastName = "B123jkt"; //parameters
//            //System.DateTime DOB = "01/01/1980";
//            string jenis = "Truk";
//            string keter = "Ok";
//            string statement = "INSERT";
////            int currentAge = 25;
////            bool isAlive = true;
//            //This is where the person id will be stored
//            Int32 personID;
//
//            try
//            {
//                //Instantiation of the class
//                var myconn =
//                         new SqlOperation.SqlOperation("localhost\\sqlexpress", "BPDMH", "thartmann", "th1478");
//                //After the class has been instantiated, 
//                //we will then save a person into the database
//                //NOTE: personID will also be set to the returned 
//                //value for the stored procedure
//                //NOTE: the class willl automatically put ticks 
//                //        around whatever needs to have ticks
//                //        and will change c# boolean to sql boolean. 
//                //        An error will be thrown if a SQL type
//                //        has not been handled for.
//
//                //There are two ways you can add the parameters to 
//                //the sp. Either as an ArrayList or an Array 
//                //of objects.
//                var param = new object[5];
//                param[0] = firstName;
//                param[1] = lastName;
//                param[2] = jenis;
//                param[3] = keter;
//                param[4] = statement;
//
//                //NOTE: when you use this method, the 
//                //PARAMETERS MUST BE PASSED IN CORRECT ORDER
//                //We will now execute the storedprocedure 
//                //and set the personID in one step.
//                personID =  Convert.ToInt32(myconn.ExcecuteStoredProcedure(saveSp, param).Tables[0].Rows[0][0]);
//                //We will now execute the getPerson stored 
//                //procedure but with the ArrayList approach.
//                var parameters = new ArrayList {personID};
//                //Lets say that the Stored procedure returns Columns 
//                //with column names: ID, FirstName, LastName, DOB, 
//                //CurrentAge,isAlive
//                var paramss = new object[1];
//                paramss[0] = firstName;
//                paramss[1] = lastName;
//                paramss[2] = jenis;
//                paramss[3] = keter;
//                paramss[4] = "SELECT";
//                myDataSet = myconn.ExcecuteStoredProcedure(getSp, parameters);
//                MessageBox.Show("Hello! My name is " +
//                            myDataSet.Tables[0].Rows[0]["KdKnd"] +
//                            " " +
//                            myDataSet.Tables[0].Rows[0]["NoPolisi"] +
//                            ". And my ID is : " +
//                            myDataSet.Tables[0].Rows[0]["Jenis"]);
//            }
//            catch (Exception error)
//            {
//                MessageBox.Show(error.Message);
//            }
//        }
//
//        public void Delete(T entity)
//        {
//            throw new System.NotImplementedException();
//        }
//
//        public void Update(T entity)
//        {
//            throw new System.NotImplementedException();
//        }
//
//        public T GetBy(T entity)
//        {
//            throw new System.NotImplementedException();
//        }
//
//        public IEnumerable<T> GetAll()
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}