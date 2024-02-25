using StoreApp.DatabaseProvider.Models;

namespace StoreApp.Services.Interfaces
{
    public interface IStripeService
    {
        string CreatePayment(List<ProductOrder> productOrders, int orderId);
    }
}
