using ProductApi.Models;
using ProductApi.Repository;
using ProductApi.Helpers;
using ProductApi.Repository.IRepository;
namespace ProductApi.Services
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
                _repository = repository;
        }
        public async Task<Product> CreateProductAsync(Product product)
        {
            var existingIds = (await _repository.GetAllAsync()).Select(p => p.ProductId);
            product.ProductId= UniqueIdGenerator.GenerateUniqueId(existingIds);
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();
            return product;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _repository.GetAllAsync(); 
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            return await (_repository.GetByIdAsync(id));
        }
        public async Task<bool> UpdateProductAsync(Product product)
        {
            var existing=await _repository.GetByIdAsync(product.ProductId);
            if(existing==null) return false;
            existing.Name= product.Name;
            existing.Description= product.Description;
            existing.Price= product.Price;
            existing.StockAvailable= product.StockAvailable;
            await _repository.UpdateAsync(existing);
            await _repository.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var product=await _repository.GetByIdAsync(id);
            if(product==null) return false;
            await _repository.DeleteAsync(product);
            await _repository.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AdjustStock(int id, int quantity, bool isAdd)
        {
            var product=await _repository.GetByIdAsync(id);
            if(product==null) return false;
            product.StockAvailable += isAdd ? quantity : -quantity;
            if(product.StockAvailable < 0) return false;
            await _repository.UpdateAsync(product);
            await _repository.SaveChangesAsync();
            return true;

        }
    }
}
