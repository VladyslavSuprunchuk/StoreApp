using Microsoft.EntityFrameworkCore;
using StoreApp.DatabaseProvider;
using StoreApp.DatabaseProvider.Models;
using StoreApp.Services.Interfaces;

namespace StoreApp.Services.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;

        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Customer> GetUserByEmailAsync(string email)
        {
            var user = await _dataContext.Customers.FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            await _dataContext.Customers.AddAsync(customer);
            await _dataContext.SaveChangesAsync();

            return customer;
        }
    }
}
