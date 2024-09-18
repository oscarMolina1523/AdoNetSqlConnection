using AdoNetSQLConnection.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AdoNetSQLConnection.Repositories
{
    public class TareasRepository
    {
        private readonly ISingletonSqlConnection _connectionBuilder;
        public TareasRepository(ISingletonSqlConnection connectionBuilder)
        {
            _connectionBuilder = connectionBuilder;
        }

        public void Crear(ToDo tarea)
        {
            string insertQuery = "INSERT INTO Tareas (Id, Title, Description) VALUES(@ID, @Titulo, @Descripcion)";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(insertQuery);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@ID",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = tarea.Id
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Titulo",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = tarea.Title
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Descripcion",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = tarea.Description
                },
            };
            sqlCommand.Parameters.AddRange(parameters);
            int rows = sqlCommand.ExecuteNonQuery();
            Console.WriteLine($"{rows} Filas fueron insertadas");
        }
        
        public void Actualizar(ToDo tarea)
        {
            string updateQuery = "UPDATE Tareas SET Title = @Titulo, Description = @Descripcion WHERE Id = @Id;";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(updateQuery);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Id",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = tarea.Id
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Titulo",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = tarea.Title
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Descripcion",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = tarea.Description
                },
            };
            sqlCommand.Parameters.AddRange(parameters);
            int rows = sqlCommand.ExecuteNonQuery();
            Console.WriteLine($"{rows} Filas fueron insertadas");
        }

        public void Eliminar(Guid id)
        {
            string deleteQuery = "DELETE FROM Tareas WHERE Id = @TareaId;";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(deleteQuery);
            SqlParameter parameter = new SqlParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "@TareaId",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = id
            };
            sqlCommand.Parameters.Add(parameter);
            int rows = sqlCommand.ExecuteNonQuery();
            Console.WriteLine($"{rows} Filas fueron eliminadas");
        }

        public List<ToDo> Get()
        {
            string query = "SELECT * FROM Tareas;";
            DataTable dataTable = _connectionBuilder.ExecuteQueryCommand(query);
            return dataTable.AsEnumerable().Select(MapEntityFromDataRow).ToList();
        }

        private ToDo MapEntityFromDataRow(DataRow row)
        {
            return new ToDo()
            {
                Id = _connectionBuilder.GetValueOrDefault<Guid>(row, "Id"),
                Title = _connectionBuilder.GetValueOrDefault<string>(row, "Title"),
                Description = _connectionBuilder.GetValueOrDefault<string>(row, "Description")
            };
        }
    }
}
