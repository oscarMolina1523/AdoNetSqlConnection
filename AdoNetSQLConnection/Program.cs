using AdoNetSQLConnection;
using AdoNetSQLConnection.Entities;
using AdoNetSQLConnection.Repositories;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

public class Program
{
    private static void Main()
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        string connectionString = config.GetConnectionString("MSSQLServer") ?? "No Connection String Found";

        //Console.WriteLine("Inicializando la conexion a SQL");
        SingletonSqlConnection connectionBuilder = SingletonSqlConnection.GetInstance(connectionString);
        var repo = new TareasRepository(connectionBuilder);
        ToDo tarea = new ToDo()
        {
            Id = Guid.NewGuid(),
            Title = "Tarea 003",
            Description = "Descripcion de la tarea 003"
        };

        repo.Crear(tarea);
        //Guid guid = Guid.Parse("AB3872E5-471E-4E67-AEB8-22C747D3F189");
        //repo.Eliminar(guid);
        //UsersRepository repository = new UsersRepository(connection);
        //List<User> users = repository.Get();

        //Console.WriteLine("Lista de usuarios...");
        //Console.WriteLine("===================================");
        //foreach (User user in users)
        //{
        //    Console.WriteLine("Id: " + user.Id);
        //    Console.WriteLine($"UserName: {user.UserName}");
        //    Console.WriteLine("Email: {0}", user.Email);
        //    Console.WriteLine("TwoFactorEnabled: " + user.TwoFactorEnabled);
        //    Console.WriteLine("LockoutEnd: " + user.LockoutEnd ?? DateTime.UtcNow.ToString());
        //    Console.WriteLine("=====================================");
        //    Console.WriteLine();
        //}

        connectionBuilder.CloseConnection();
        //Console.WriteLine("Conexion Cerrada");
    }
}
