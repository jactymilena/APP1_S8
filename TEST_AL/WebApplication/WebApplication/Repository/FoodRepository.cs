using WebApplication.Models;
using System.Linq;

namespace WebApplication.Repository
{
    public class FoodRepository : IFoodRepository
    {
        List<Order> orders = new List<Order>();
        public void InsertOrder(Order order)
        {
            orders.Add(order);
        }

        public List<Order> FetchAllOrdersForCustomer()
        {
            return orders;
        }

        public Order FetchSpecificOrderDetails(int orderId)
        {
            return orders.First(order => order.OrderId == orderId);
        }

        public void UpdateFoodQuantityInOrder(int orderId, int quantity)
        {
            Order orderToUpdate = orders.First(order => order.OrderId == orderId);

            if (orderToUpdate != null)
            {
                orderToUpdate.Quantity = quantity;
            }
        }

        public void UpdateOrder(Order order)
        {
            Order orderToUpdate = orders.First(order => order.OrderId == order.OrderId);
            if (orderToUpdate != null)
            {
                orderToUpdate = order;
            }
        }

        public void DeleteOrder(int orderId)
        {
            Order orderToDelete = orders.First(order => order.OrderId == orderId);
            if (orderToDelete != null)
            {
                orders.Remove(orderToDelete);
            }
        }
    }
}
