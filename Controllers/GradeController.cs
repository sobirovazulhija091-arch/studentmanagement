using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

[ApiController]
[Route("api/[controller]")]
public class GradeController(IGradeService gradeService,IMemoryCache cache):ControllerBase
{
     private readonly IMemoryCache _cache=cache;
    private readonly IGradeService gradeService=gradeService;
    [HttpPost]
     public async Task<Response<string>> AddAsync(GradeDto gradeDto)
    {
        return await gradeService.AddAsync(gradeDto);
    }
[HttpGet]
    public async   Task<Response<List<Grade>>> GetAsync()
    {
       return await gradeService.GetAsync();
    }
[HttpGet("grade")]
    public async   Task<Response<List<Grade>>> GetGradeAsync()
    {
       return await gradeService.GetGradeAsync();
    }
}