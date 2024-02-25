using StoreApp.DatabaseProvider.Models;

namespace StoreApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<Customer> GetUserByEmailAsync(string email);

        Task<Customer> CreateCustomerAsync(Customer customer);
    }
}
