using System.ComponentModel.DataAnnotations;

namespace MVC_OnlineStore.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }//Navigation property

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
