using Microsoft.Extensions.Caching.Memory;
public interface ICachingService
{
    Task<IReadOnlyList<ProductDto>> GetTopProductsAsync(CancellationToken ct);
    void InvalidateTopProductsCache();
}
public class CachingService:ICachingService
{
    private readonly IMemoryCache _cache;
    private readonly ILogger< ProductDto> _logger;
    private const string TopProduct = "products_top";
    public CachingService(IMemoryCache cache, ILogger<ProductDto> logger)
    {
        _cache = cache;
        _logger = logger;
    }
    public async Task<IReadOnlyList<ProductDto>> GetTopProductsAsync(CancellationToken ct)
    {
        return await _cache.GetOrCreateAsync(TopProduct, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
            entry.SlidingExpiration = TimeSpan.FromSeconds(1);
            entry.Priority =CacheItemPriority.High;
            
            _logger.LogInformation("Cache miss for top products. Fetching from database...");
            await Task.Delay(1000, ct); 
            return new List<ProductDto>
            {
                new ProductDto { Id = Guid.NewGuid(), Name = "Product 1", Price = 10.99m },
                new ProductDto { Id = Guid.NewGuid(), Name = "Product 2", Price = 15.99m },
                new ProductDto { Id = Guid.NewGuid(), Name = "Product 3", Price = 20.99m }
            };
        }) ?? new List<ProductDto>();
    }

 public void InvalidateTopProductsCache()
    {
        _cache.Remove(TopProduct);
        _logger.LogInformation("Top products cache invalidated.",TopProduct);
    }

}

