namespace MVC_OnlineStore.ViewModels
{
    public class CreateOrderViewModel
    {
        public string CustomerName { get; set; } = string.Empty;

        public string CustomerAddress { get; set; } = string.Empty;

        public List<OrderItemViewModel> Items { get; set; } = new();
    }
}