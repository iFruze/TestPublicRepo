using Lab6TestTask.Data;
using Lab6TestTask.Models;
using Lab6TestTask.Services.Interfaces;

namespace Lab6TestTask.Services.Implementations;

/// <summary>
/// WarehouseService.
/// Implement methods here.
/// </summary>
public class WarehouseService : IWarehouseService
{
    private readonly ApplicationDbContext _dbContext;

    public WarehouseService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Warehouse> GetWarehouseAsync()
    {
        var wares = _dbContext.Warehouses.ToList();
        var values = new Dictionary<Warehouse, decimal>();

        foreach(var item in wares)
        {
            var products = _dbContext.Products.Where(p => p.WarehouseId == item.WarehouseId && p.Status == Enums.ProductStatus.ReadyForDistribution).ToList();
            decimal value = 0;
            foreach(var i in products)
            {
                value += i.Price;
            }
            values.Add(item, value);
        }
        Warehouse ware = values.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value).Keys.First();
        var res = new Warehouse();
        res.WarehouseId = ware.WarehouseId;
        res.Name = ware.Name;
        res.Location = ware.Location;
        return res;
    }

    public async Task<IEnumerable<Warehouse>> GetWarehousesAsync()
    {
        var wares = _dbContext.Warehouses.ToList();
        var result = new List<Warehouse>();

        foreach (var item in wares)
        {
            var products = _dbContext.Products.Where(p => p.WarehouseId == item.WarehouseId && (p.ReceivedDate.Month >= 4 && p.ReceivedDate.Month <= 6)).ToList();
            if(products.Any())
            {
                Warehouse ware = new Warehouse();
                ware.WarehouseId = item.WarehouseId;
                ware.Name = item.Name;
                ware.Location = item.Location;
                result.Add(ware);
            }
        }
        return result;
    }
}
