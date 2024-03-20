using Dapper;
using DataAccess.Models;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            // string de conexão com o banco
            const string connectionString = "Server=localhost,1433;Database=balta;User Id=sa;Password=1q2w3e!@#; TrustServerCertificate=true;";

            using var connection = new SqlConnection(connectionString);
            var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");

            foreach (var category in categories)
            {
                Console.WriteLine($"{category.Id} - {category.Title}");
            }

            // Microsoft.Data.SqlClient
        }
    }
}