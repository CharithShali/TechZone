using Ecommerce_Website.Models;

namespace Ecommerce_Website.Services
{
    public interface IProductService
    {
        public Task<List<Product>> GetAll();
        public Task<Product> GetById(int id);
        public Task<Product> Add(Product product);
        public Task<Product> Edit(int id,Product product);
        public Task<List<Product>> GetByCategory(int id);
        public Task<List<Product>> searchbyName(string name);





    }
}
