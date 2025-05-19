using CNative;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
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
        

        public static async Task<Product> GetProduct(Guid id)
        {
            try
            {
                using (WebshopdbContext db = new WebshopdbContext())
                {
                    return db.Products.Where(c => c.Id == id).Include(c => c.Images).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async void AddNewProduct(Product product)
        {
            try
            {
                using (WebshopdbContext db = new WebshopdbContext())
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                }
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
                using (WebshopdbContext db = new WebshopdbContext())
                {
                    return db.Products.Where(c => c.ProductType == type).Include(c => c.Images).ToList();
                }
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
                using (WebshopdbContext db = new WebshopdbContext())
                {
                    return db.Categories.FromSqlInterpolated($"select * from public.getcategories()").ToList();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        //public static async Task SetProductImages(Product product)
        //{
        //    using var cmd = new NpgsqlCommand();
        //    cmd.Connection = GetConnection();
        //    cmd.CommandText = $"SELECT url FROM images WHERE productid='{product.Id}'";
        //    NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
        //    product.ImagesUrl = new List<string>();
        //    while(await reader.ReadAsync())
        //    {
        //        product.ImagesUrl.Add(reader["url"].ToString());
        //    }
        //}
    }
}
