using System.Data;
using System.Data.SqlClient;
using System.Net.Http.Headers;

namespace AdoNetSQLConnection
{
    public class SingletonSqlConnection : ISingletonSqlConnection
    {
        private static SingletonSqlConnection instance;
        private readonly SqlConnection sqlConnection;

        private SingletonSqlConnection(string connectionString)
        {
            sqlConnection = new SqlConnection(connectionString);
            OpenConnection();
        }

        public static SingletonSqlConnection GetInstance(string connectionString)
        {
            if (instance == null)
            {
                instance = new SingletonSqlConnection(connectionString);
                return instance;
            }

            return instance;
        }

        public SqlCommand GetCommand(string query)
        {
            return new SqlCommand(query, sqlConnection);
        }

        public DataTable ExecuteQueryCommand(string query)
        {
            DataTable dataTable = new DataTable();
            SqlCommand command = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            dataTable.Load(reader);
            command.Dispose();

            return dataTable;
        }

        public T GetValueOrDefault<T>(DataRow row, string column, T defaultValue = default!)
        {
            return !row.IsNull(column) ? row.Field<T>(column)! : defaultValue;
        }

        public void OpenConnection()
        {
            if (sqlConnection.State == ConnectionState.Open) return;

            sqlConnection.Open();
        }

        public void CloseConnection()
        {
            if (sqlConnection.State == ConnectionState.Closed) return;

            sqlConnection.Close();
        }
    }
}
