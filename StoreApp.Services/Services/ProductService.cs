using Microsoft.EntityFrameworkCore;
using StoreApp.DatabaseProvider;
using StoreApp.DatabaseProvider.Models;
using StoreApp.Services.Interfaces;

namespace StoreApp.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;

        public ProductService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _dataContext.AddAsync(product);
            await _dataContext.SaveChangesAsync();

            return product;
        }

        public async Task<Product> GetByIdAsync(int productId)
        {
            var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == productId);

            return product;
        }

        public List<Product> GetAll()
        {
            var products = _dataContext.Products.ToList();

            return products;
        }
    }
}
