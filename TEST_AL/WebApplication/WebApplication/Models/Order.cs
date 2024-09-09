namespace WebApplication.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustumerName { get; set; }
        public int FoodItem { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderTime { get; set; }
        public Order(Order Order)
        {
            OrderId = Order.OrderId;
            CustumerName = Order.CustumerName;
            FoodItem = Order.FoodItem;
            Quantity = Order.Quantity;
            OrderTime = Order.OrderTime;
        }
    }
}
