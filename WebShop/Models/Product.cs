namespace WebShop.Models
{
    public enum ProductType
    {
        Camera = 1,
        Laptop = 2,
        SmartPhone = 3,
        Accessory = 4
    }

    public class Product : BaseViewModel
    {
        public Product(Guid id, ProductType productType, string brand, string model, decimal price, int sale, DateTime addDate, string description, bool inStock)
        {
            Id = id;
            ProductType = productType;
            Brand = brand;
            Model = model;
            Price = price;
            Sale = sale;
            AddDate = addDate;
            Description = description;
            InStock = inStock;
        }

        public Guid Id { get; set; }
        public ProductType ProductType { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public int Sale { get; set; }
        public DateTime AddDate { get; set; }
        public string Description { get; set; }
        public bool InStock { get; set; }

        public List<string> ImagesUrl { get; set; }
    }
}
