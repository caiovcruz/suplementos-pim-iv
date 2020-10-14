using DataBase.Connectors;

namespace DataBase
{
    /// <summary>
    /// To connect to a database.
    /// </summary>
    public class DAO
    {
        /// <summary>
        /// Initializes a new instance of the DAO class.
        /// </summary>
        public DAO() { }

        /// <summary>
        /// Gets or sets the connectior.
        /// </summary>
        public BaseConnector Connector { get; set; }

        /// <summary>
        /// To setup a connection.
        /// </summary>
        /// <param name="databaseType">The database type.</param>
        /// <param name="connectionString">The connection string.</param>
        public void Setup(DatabaseTypes databaseType, string connectionString)
        {
            switch (databaseType)
            {
                case DatabaseTypes.Access:
                    {
                        Connector = new AccessConnector(connectionString);
                        break;
                    }

                case DatabaseTypes.MySql:
                    {
                        Connector = new MySQLConnector(connectionString);
                        break;
                    }

                case DatabaseTypes.SqlServer:
                    {
                        Connector = new SqlServerConnector(connectionString);
                        break;
                    }
            }
        }
    }

    /// <summary>
    /// The databases type.
    /// </summary>
    public enum DatabaseTypes
    {
        MySql,
        SqlServer,
        Access
    }
}
