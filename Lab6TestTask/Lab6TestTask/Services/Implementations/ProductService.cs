using Lab6TestTask.Data;
using Lab6TestTask.Models;
using Lab6TestTask.Services.Interfaces;

namespace Lab6TestTask.Services.Implementations;

/// <summary>
/// ProductService.
/// Implement methods here.
/// </summary>
public class ProductService : IProductService
{
    private readonly ApplicationDbContext _dbContext;

    public ProductService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product> GetProductAsync() => _dbContext.Products.Where(pr => pr.Status == Enums.ProductStatus.Reserved).OrderByDescending(pr => pr.Price).First();
    

    public async Task<IEnumerable<Product>> GetProductsAsync() => _dbContext.Products.Where(p => p.Quantity > 1000 && p.ReceivedDate.Year == 2025).ToList();
}
