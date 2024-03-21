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
            string connectionString = "Server=localhost,1433;Database=balta;User Id=sa;Password=1q2w3e!@#; TrustServerCertificate=true;";

            using var connection = new SqlConnection(connectionString);
            //CreateCategory(connection);
            //GetCategory(connection);
            //ListCategories(connection);
            //UpdateCategory(connection);
            //DeleteCategory(connection);
        }

        static void CreateCategory(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            var insertQuery = @"
                INSERT INTO
                    [Category]
                VALUES
                (
                    @Id,
                    @Title,
                    @Url,
                    @Summary,
                    @Order,
                    @Description,
                    @Featured
                )";

            var rows = connection.Execute(insertQuery, new
            {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            });

            Console.WriteLine($"{rows} registros cadastrados");
        }
        static void ListCategories(SqlConnection connection)
        {
            var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category] ORDER BY [Id]");
            foreach (var item in categories)
            {
                Console.WriteLine($"{item.Id} - {item.Title}");
            }
        }
        static void GetCategory(SqlConnection connection)
        {
            var getQuery = @"SELECT [Id], [Title], [Description] FROM [Category] WHERE [Id] = @Id";

            var item = connection.QueryFirstOrDefault<Category>(getQuery, new
            {
                Id = "af3407aa-11ae-4621-a2ef-2028b85507c4"
            });

            Console.WriteLine($"{item.Id} - {item.Title} - {item.Description}");
        }
        static void UpdateCategory(SqlConnection connection)
        {
            var updateQuery = @"
                UPDATE
                    [Category]
                SET 
                    [Title] = @Title 
                WHERE
                    [Id] = @Id
                ";

            var rows = connection.Execute(updateQuery, new
            {
                Id = "af3407aa-11ae-4621-a2ef-2028b85507c4",
                Title = "Frontend 2024"
            });

            Console.WriteLine($"{rows} registros atualizados");
        }
        static void DeleteCategory(SqlConnection connection)
        {
            var deleteQuery = @"
                    DELETE [Category] WHERE [Id] = @Id;
                ";

            var rows = connection.Execute(deleteQuery, new
            {
                Id = "165de31a-7135-4344-bbe7-36be79dfe299"
            });

            Console.WriteLine($"{rows} itens excluído");
        }
    }
}