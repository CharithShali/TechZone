
using Ecommerce_Website.Context;
using Ecommerce_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Website.Services
{
    public class ProductServices : IProductService
    {
        private readonly TechZoneContext _context;

        public ProductServices(TechZoneContext techZoneContext)
        {
            _context = techZoneContext;
        }
        public async Task<Product>Add(Product proObj)
        {
          
         

            Product product = new Product();

            product.ProductId = proObj.ProductId;
            product.ImageName = proObj.ImageName;
            product.Description = proObj.Description;
            product.Price = proObj.Price;
            product.CategoryId=proObj.CategoryId;
            product.Quantity = proObj.Quantity;
            product.Title = proObj.Title;
            product.UserId = proObj.UserId;

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;

        }

        public async Task<List<Product>> GetAll()
        {
            var result= await _context.Products.ToListAsync();
            return result;
        }

        public async Task<Product> GetById(int id)
        {
            var product = await _context.Products.Where(c => c.ProductId == id).SingleOrDefaultAsync();
            return product;
        }
        public async Task<Product> Edit(int id,Product productObj)
        {
            var product = await _context.Products.FindAsync(id);
            product.Quantity=productObj.Quantity;
            product.Title=productObj.Title;
            product.Price=productObj.Price;
            product.ImageName=productObj.ImageName;
            product.CategoryId = productObj.CategoryId;

            await _context.SaveChangesAsync();
            return(product);

        }

        public async Task<List<Product>> GetByCategory(int id)
        {
            
            var products = await _context.Products.Where(c => c.CategoryId == id).ToListAsync();
            return products;
        }

        public async Task<List<Product>> searchbyName(string name)
        {
            var products = await _context.Products.Where(p => p.Title.ToLower().Contains(name.ToLower())).ToListAsync();
            return products;
        }
    }
}
