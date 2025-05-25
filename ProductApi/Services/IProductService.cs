using ProductApi.Models;

namespace ProductApi.Services
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(Product product);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteAsync(int id);
        Task<bool> AdjustStock(int id, int quantity, bool isAdd);
    }
}
