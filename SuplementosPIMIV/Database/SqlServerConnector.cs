using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace DataBase.Connectors
{
    public class SqlServerConnector : BaseConnector
    {
        /// <summary>
        /// The connection;
        /// </summary>
        private SqlConnection connection = null;

        /// <summary>
        /// Initializes a new instance of the MySQLConnector class.
        /// </summary>
        public SqlServerConnector() { }

        /// <summary>
        /// Initializes a new instance of the AccessConnector class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public SqlServerConnector(string connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// To open a connection.
        /// </summary>
        /// <returns>False if failed to open a connection.</returns>
        public override bool Open()
        {
            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();
            }
            catch (Exception error)
            {
                string a = error.Message;
                return false;
            }

            return true;
        }

        /// <summary>
        /// To close a connection.
        /// </summary>
        /// <returns>False if failed to close.</returns>
        public override bool Close()
        {
            try
            {
                connection.Close();
                connection = null;
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// To make a query that will return a db reader.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public override System.Data.IDataReader QueryWithReader(string query)
        {
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            return reader;

        }

        /// <summary>
        /// To make a query like insert or update.
        /// </summary>
        /// <param name="text">The text to submit</param>
        /// <returns>False if failled.</returns>
        public override int Execute(string text)
        {
            SqlCommand command = new SqlCommand(text, connection);
            int result = command.ExecuteNonQuery();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override DbCommand GetCommand(string text)
        {
            return new SqlCommand(text, connection); ;
        }
    }
}
