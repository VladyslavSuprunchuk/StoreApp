using StoreApp.DatabaseProvider.Models;

namespace StoreApp.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(Product product);

        Task<Product> GetByIdAsync(int productId);

        List<Product> GetAll();
    }
}
