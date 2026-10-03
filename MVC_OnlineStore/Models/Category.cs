using System.ComponentModel.DataAnnotations;

namespace MVC_OnlineStore.Models
{
    public class Category
    {
        //Phones laptops Accessories
        public int Id { get; set; }//pk

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
