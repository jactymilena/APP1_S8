using WebApplication.Models;

namespace WebApplication.Repository
{
    public interface IFoodRepository
    {
        void InsertOrder(Order order);
        List<Order> FetchAllOrdersForCustomer();
        Order FetchSpecificOrderDetails(int orderId);
        void UpdateOrder(Order order);
        void UpdateFoodQuantityInOrder(int orderId, int quantity);
        void DeleteOrder(int orderId);
    }
}
