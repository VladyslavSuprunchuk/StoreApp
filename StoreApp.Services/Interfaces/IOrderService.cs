using StoreApp.DatabaseProvider.Models;

namespace StoreApp.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Order order, Customer customer, List<ProductOrder> productOrders);

        Task<bool> UpdateOrderStatusAsync(int orderId);
    }
}
