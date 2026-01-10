using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models;

namespace ProductApi.Services;

public class ProductService
{
    private readonly AppDbContext _context;
    private readonly ICacheService _cacheService;

    public ProductService(AppDbContext context, ICacheService cacheService)
    {
        _context = context;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        // Check cache first
        var cachedProduct = await _cacheService.GetAsync<Product>($"product_{id}");
        if (cachedProduct != null)
        {
            return cachedProduct;
        }

        // If not in cache, get from database
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            // Cache the product for future requests
            await _cacheService.SetAsync($"product_{id}", product, TimeSpan.FromMinutes(30));
        }

        return product;
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // Cache the newly created product
        await _cacheService.SetAsync($"product_{product.Id}", product, TimeSpan.FromMinutes(30));

        return product;
    }

    public async Task<Product> UpdateProductAsync(int id, Product product)
    {
        var existingProduct = await _context.Products.FindAsync(id);
        if (existingProduct == null)
        {
            return null;
        }

        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;
        existingProduct.StockQuantity = product.StockQuantity;
        existingProduct.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        // Update the cache
        await _cacheService.SetAsync($"product_{id}", existingProduct, TimeSpan.FromMinutes(30));

        return existingProduct;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return false;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        // Remove from cache
        await _cacheService.RemoveAsync($"product_{id}");

        return true;
    }

    public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return await GetAllProductsAsync();
        }

        return await _context.Products
            .Where(p => p.Name.Contains(searchTerm) ||
                       p.Description.Contains(searchTerm))
            .ToListAsync();
    }
}