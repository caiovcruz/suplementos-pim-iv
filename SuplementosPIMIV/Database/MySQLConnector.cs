using MySql.Data.MySqlClient;
using System;
using System.Data.Common;

namespace DataBase.Connectors
{
    public class MySQLConnector : BaseConnector
    {
        /// <summary>
        /// The connection;
        /// </summary>
        private MySqlConnection connection = null;

        /// <summary>
        /// Initializes a new instance of the MySQLConnector class.
        /// </summary>
        public MySQLConnector() { }

        /// <summary>
        /// Initializes a new instance of the AccessConnector class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public MySQLConnector(string connectionString)
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
                connection = new MySqlConnection(ConnectionString);
                connection.Open();
            }
            catch (Exception e)
            {
                string s = e.Message;
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
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            return reader;
        }

        /// <summary>
        /// To make a query like insert or update.
        /// </summary>
        /// <param name="text">The text to submit</param>
        /// <returns>False if failled.</returns>
        public override int Execute(string text)
        {
            MySqlCommand command = new MySqlCommand(text, connection);
            int result = command.ExecuteNonQuery();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override DbCommand GetCommand(string text)
        {
            return new MySqlCommand(text, connection);
        }
    }
}
