using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models
{
    public class Images
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string url { get; set; }
        public Product Product { get; set; }
    }
}
