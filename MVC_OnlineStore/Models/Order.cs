using System.ComponentModel.DataAnnotations;

namespace MVC_OnlineStore.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalPrice { get; set; }
        [Required]
        [MaxLength(150)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [MaxLength(300)]
        public string CustomerAddress { get; set; } = string.Empty;


        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
