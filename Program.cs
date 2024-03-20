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

            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            var insertSql = @"
                INSERT INTO
                    [Category]
                VALUES(
                    id,
                    title,
                    url,
                    summary,
                    order,
                    description,
                    featured)";


            using var connection = new SqlConnection(connectionString);

            var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
            foreach (var item in categories)
            {
                Console.WriteLine($"{item.Id} - {item.Title}");
            }


            // Microsoft.Data.SqlClient
        }
    }
}