﻿using Dapper;
using DataAccess.Models;
using Microsoft.Data.SqlClient;
using System.Data;

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
            //CreateManyCategories(connection);
            //GetCategory(connection);
            //ListCategories(connection);
            //UpdateCategory(connection);
            //DeleteCategory(connection, "593a505e-0372-409b-8501-7f3ad53d9ccb");
            //ExecuteProcedure(connection);
            //ExecuteScalar(connection);
            //ReadView(connection);
            OneToOne(connection);
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
        static void CreateManyCategories(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            var category2 = new Category();
            category2.Id = Guid.NewGuid();
            category2.Title = "Nova categoria";
            category2.Url = "categoria";
            category2.Description = "Descrição da categoria nova";
            category2.Order = 9;
            category2.Summary = "Categoria";
            category2.Featured = true;

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

            var rows = connection.Execute(insertQuery, new[]
            {
                new
                {
                    category.Id,
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured
                },
                new
                {
                    category2.Id,
                    category2.Title,
                    category2.Url,
                    category2.Summary,
                    category2.Order,
                    category2.Description,
                    category2.Featured
                }
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

            if (item != null)
            {
                Console.WriteLine($"{item.Id} - {item.Title}");
            }
            else
            {
                Console.WriteLine($"Item não encontrado!");
            }

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
        static void DeleteCategory(SqlConnection connection, string categoryId)
        {
            var deleteQuery = @"
                    DELETE [Category] WHERE [Id] = @Id;
                ";

            var rows = connection.Execute(deleteQuery, new
            {
                Id = categoryId
            });

            Console.WriteLine($"{rows} itens excluído");
        }

        static void ExecuteProcedure(SqlConnection connection)
        {
            var sql = "[spDeleteStudent]";
            var prs = new { StudentId = "c5db9d4b-d7f1-433b-9f73-8f8bef2627e6" };
            var rows = connection.Execute(sql, prs, commandType: CommandType.StoredProcedure);

            Console.WriteLine($"{rows} linhas afetadas");
        }
        static void ExecuteReadProcedure(SqlConnection connection)
        {
            var procedure = "[spGetCoursesBycategory]";
            var pars = new { CategoryId = "09ce0b7b-cfca-497b-92c0-3290ad9d5142" };
            var courses = connection.Query(procedure, pars, commandType: CommandType.StoredProcedure);

            foreach (var item in courses)
            {
                Console.WriteLine($"{item.Title}");
            }

        }

        static void ExecuteScalar(SqlConnection connection)
        {
            var category = new Category();
            category.Title = "Amazon AWS teste";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            var insertQuery = @"
                INSERT INTO
                    [Category]
                OUTPUT inserted.[Id]
                VALUES
                (
                    NEWID(),
                    @Title,
                    @Url,
                    @Summary,
                    @Order,
                    @Description,
                    @Featured
                )";

            var id = connection.ExecuteScalar<Guid>(insertQuery, new
            {
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            });

            Console.WriteLine($"A categoria cadastrada foi : {id}");
        }

        static void ReadView(SqlConnection connection)
        {
            var viewQuery = "SELECT * FROM [vwCourses]";

            var courses = connection.Query(viewQuery);
            foreach (var item in courses)
            {
                Console.WriteLine($"{item.Title}");
            }
        }
        static void OneToOne(SqlConnection connection)
        {
            var sql = @"
                SELECT
                    *
                FROM
                    [CareerItem]
                INNER JOIN
                   [Course] ON [CareerItem].[CourseId] = [Course].[Id]
            ";

            var items = connection.Query(sql);

            foreach (var item in items)
            {
                Console.WriteLine($"{item.DurationInMinutes}");
            }
        }
    }
}