using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Controllers;
using GraphQL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace GraphQL.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : GraphController
    {
        private readonly DataContext _context;
        public ProductController(DataContext context)
        {
            _context = context;
        }
        [QueryRoot("getProducts")]
        public async Task<IEnumerable<Product>> GetProducts(int skip = 0, int take = 10)
        {
            return await _context.Set<Product>()
                .Skip(skip)
                .Take(take)
                .ToArrayAsync();
        }
        [QueryRoot("getProduct")]
        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new ArgumentException("Product not found");
        }
        [MutationRoot("createProduct")]
        public async Task<Product> CreateProduct(Product product)
        {
            var productEntry = await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return productEntry.Entity;
        }
        [MutationRoot("updateProduct")]
        public async Task<Product> UpdateProduct(int id, Product product)
        {
            if (!await _context.Products.AnyAsync(p => p.Id == id))
                throw new ArgumentException("Product not found");
            product.Id = id;
            var productEntry = _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return productEntry.Entity;
        }
        [MutationRoot("deleteProduct")]
        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id) 
                ?? throw new ArgumentException("Product not found");
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
