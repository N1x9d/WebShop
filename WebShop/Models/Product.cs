using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models
{
    public enum ProductType
    {
        Camera = 1,
        Laptop = 2,
        SmartPhone = 3,
        Accessory = 4
    }

    public class Product
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public ProductType ProductType { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public int Sale { get; set; }
        public DateTimeOffset AddDate { get; set; }
        public string Description { get; set; }
        public bool InStock { get; set; }

        public List<Images> Images { get; set; } = new List<Images>();
    }
}
