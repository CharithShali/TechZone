using Ecommerce_Website.Models;

namespace Ecommerce_Website.Services
{
    public interface ICategoryService
    {
        public Task<List<ProductCategory>> GetAll();
        public Task<ProductCategory> Add(ProductCategory productCategory);

        public Task<List<ProductCategory>> GetByCategory(string category);

    }
}
