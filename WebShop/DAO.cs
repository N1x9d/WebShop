using CNative;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Npgsql;
using NuGet.DependencyResolver;
using System.Data.Common;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using WebShop.Models;

namespace WebShop
{
    public static class DAO
    {
        private const string _connString = "Server=localhost;Port=5432;User Id=postgres;Password=root;Database=postgres;";
        private static NpgsqlConnection GetConnection() 
        {
            var connection = new NpgsqlConnection(_connString);
            connection.Open();
            return connection;
        }

        public static async Task<Product> GetProduct(Guid id)
        {
            try
            {
                using var cmd = new NpgsqlCommand();
                cmd.Connection = GetConnection();
                cmd.CommandText = $"SELECT * FROM product WHERE id='{id}'";
                NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

                await reader.ReadAsync();
                var product = new Product(
                id: (Guid)reader["id"],
                productType: (ProductType)reader["producttypeid"],
                brand: reader["brand"].ToString(),
                model: reader["model"].ToString(),
                price: (decimal)reader["price"],
                sale: (int)reader["sale"],
                addDate: (DateTime)reader["adddate"],
                description: reader["description"].ToString(),
                inStock: (bool)reader["instock"]
                );
                await DAO.SetProductImages(product);
                return product;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static async Task<List<Product>> GetProducts(ProductType type)
        {
            try
            {
                using var cmd = new NpgsqlCommand();
                cmd.Connection = GetConnection();
                cmd.CommandText = $"SELECT * FROM product WHERE producttypeid='{(int)type}'";
                NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
                var result = new List<Product>();
                while (await reader.ReadAsync())
                {
                    var product = new Product(
                    id: (Guid)reader["id"],
                    productType: (ProductType)reader["producttypeid"],
                    brand: reader["brand"].ToString(),
                    model: reader["model"].ToString(),
                    price: (decimal)reader["price"],
                    sale: (int)reader["sale"],
                    addDate: (DateTime)reader["adddate"],
                    description: reader["description"].ToString(),
                    inStock: (bool)reader["instock"]
                    );
                    await DAO.SetProductImages(product);
                    result.Add(product);

                }
                return result;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public static async Task<List<Category>> GetTypesInfo()
        {
            try
            {
                using var cmd = new NpgsqlCommand();
                cmd.Connection = GetConnection();
                cmd.CommandText = $"SELECT producttypeid, COUNT(*) FROM product GROUP BY producttypeid";
                NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
                var result = new List<Category>();
                while (await reader.ReadAsync())
                {
                    result.Add(new Category
                    {
                        Count = (long)reader["count"],
                        Type = (ProductType)reader["producttypeid"]
                    });
                }

                return result;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public static async Task SetProductImages(Product product)
        {
            using var cmd = new NpgsqlCommand();
            cmd.Connection = GetConnection();
            cmd.CommandText = $"SELECT url FROM images WHERE productid='{product.Id}'";
            NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            product.ImagesUrl = new List<string>();
            while(await reader.ReadAsync())
            {
                product.ImagesUrl.Add(reader["url"].ToString());
            }
        }
    }
}
