using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            // string de conexão com o banco
            const string connectionString = "Server=localhost,1433;Database=balta;User Id=sa;Password=1q2w3e!@#; TrustServerCertificate=true;";

            using (var connection = new SqlConnection(connectionString))
            {
                Console.WriteLine("Conectado!");
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT [Id], [Title] FROM [Category]";

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader.GetGuid(0)} - {reader.GetString(1)}");
                    }
                }
            }


            // Microsoft.Data.SqlClient
        }
    }
}