using Ecommerce_Website.Context;
using Ecommerce_Website.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Website.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly TechZoneContext _context;

        public CategoryService(TechZoneContext techZoneContext)
        {
            _context = techZoneContext;
        }
        public async Task<ProductCategory> Add(ProductCategory productCategory)
        {
            await _context.ProductCategories.AddAsync(productCategory);
            await _context.SaveChangesAsync();
            return productCategory;
            
        }

        public async Task<List<ProductCategory>> GetAll()
        {
            var categories = await _context.ProductCategories.ToListAsync();
            return categories;
        }

        public async Task<List<ProductCategory>> GetByCategory(string categoryObj)
        {
            var category = await _context.ProductCategories.Where(c => c.Category == categoryObj).ToListAsync();
            return category;
        }
    }
}
