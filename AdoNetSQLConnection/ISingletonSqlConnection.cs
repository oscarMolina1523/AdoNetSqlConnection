using System.Data;
using System.Data.SqlClient;

namespace AdoNetSQLConnection
{
    public interface ISingletonSqlConnection
    {
        void CloseConnection();
        DataTable ExecuteQueryCommand(string query);
        SqlCommand GetCommand(string query);
        T GetValueOrDefault<T>(DataRow row, string column, T defaultValue = default);
        void OpenConnection();
    }
}
