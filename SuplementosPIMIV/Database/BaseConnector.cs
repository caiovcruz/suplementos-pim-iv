using System.Data;
using System.Data.Common;

namespace DataBase.Connectors
{
    /// <summary>
    /// To connect to a DB.
    /// </summary>
    public abstract class BaseConnector
    {
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Initializes a new instance of the BaseConnector class.
        /// </summary>
        public BaseConnector() { }

        /// <summary>
        /// To get the dbcommand.
        /// </summary>
        /// <returns>Return a db command.</returns>
        public abstract DbCommand GetCommand(string text);

        /// <summary>
        /// To open a connection.
        /// </summary>
        /// <returns>False if failed to open a connection.</returns>
        public abstract bool Open();

        /// <summary>
        /// To close a connection.
        /// </summary>
        /// <returns>False if failed to close.</returns>
        public abstract bool Close();

        /// <summary>
        /// To make a query that will return a db reader.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public abstract IDataReader QueryWithReader(string query);

        /// <summary>
        /// To make a query like insert or update.
        /// </summary>
        /// <param name="text">The text to submit</param>
        /// <returns>Numbers of affected columns.</returns>
        public abstract int Execute(string text);
    }
}
