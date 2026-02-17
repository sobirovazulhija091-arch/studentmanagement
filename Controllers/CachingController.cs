using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/caching")]
public class CachingController(ICachingService service):ControllerBase
{
    private readonly ICachingService _service=service;
    [HttpGet("top-products")]
     public async Task<IReadOnlyList<ProductDto>> GetTopProductsAsync(CancellationToken ct)
    {
        return await _service.GetTopProductsAsync(ct);
    }
    [HttpPost("invalidate-top-products")]
    public void InvalidateTopProductsCache()
    {
          _service.InvalidateTopProductsCache();
          return;
    }
}