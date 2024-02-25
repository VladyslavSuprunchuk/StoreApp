using Microsoft.EntityFrameworkCore;
using StoreApp.DatabaseProvider;
using StoreApp.DatabaseProvider.Models;
using StoreApp.Services.Interfaces;

namespace StoreApp.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;

        public OrderService(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        public async Task<Order> CreateOrderAsync(Order order, Customer customer, List<ProductOrder> productOrders)
        {
            var allProductsExist = productOrders.All(order => _dataContext.Products.Any(product => product.Id == order.ProductId));

            if (!allProductsExist)
            {
                throw new Exception("Invalid products requested");
            }

            var user = await _userService.GetUserByEmailAsync(customer.Email);
            user ??= await _userService.CreateCustomerAsync(customer);
            order.CustomerId = user.Id;
            order.IsPaid = false;

            foreach (var item in productOrders)
            {
                order.ProductOrders.Add(item);
            }

            _dataContext.Orders.Add(order);
            await _dataContext.SaveChangesAsync();
            order = _dataContext.Orders.
                    Include(x => x.ProductOrders).
                    ThenInclude(x => x.Product).
                    FirstOrDefault(x => x.Id == order.Id)!;

            return order!;
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId)
        {
            var order = await _dataContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            var orderExist = order is not null;

            if (orderExist)
            {
                order!.IsPaid = true;
                await _dataContext.SaveChangesAsync();
            }

            return orderExist;
        } 
    }
}
