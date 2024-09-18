using AdoNetSQLConnection.Entities;
using System.Data;
using System.Data.SqlClient;

namespace AdoNetSQLConnection.Repositories
{
    public class UsersRepository
    {
        private readonly SqlConnection _connection;
        public UsersRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public List<User> Get()
        {
            //DataSet dataSet = new DataSet();
            // RAW QUERY => Consulta Sin procesar
            DataTable dataTable = new DataTable();
            string query = "SELECT * FROM Security.Users;";
            SqlCommand command = new SqlCommand(query, _connection);
            SqlDataReader reader = command.ExecuteReader();
            dataTable.Load(reader);
            command.Dispose();

            List<User> users = new();
            foreach (DataRow row in dataTable.Rows)
            {
                User user = new User()
                {
                    Id = GetValueOrDefault<Guid>(row, "Id"),
                    UserName = GetValueOrDefault<string>(row, "UserName"),
                    Email = GetValueOrDefault<string>(row, "Email"),
                    TwoFactorEnabled = GetValueOrDefault<bool>(row, "TwoFactorEnabled"),
                    LockoutEnd = GetValueOrDefault<DateTime?>(row, "LockoutEnd")
                };

                users.Add(user);
            }

            return users;
        }

        private static T GetValueOrDefault<T>(DataRow row, string column, T defaultValue = default!)
        {
            return !row.IsNull(column) ? row.Field<T>(column)! : defaultValue;
        }
    }
}
