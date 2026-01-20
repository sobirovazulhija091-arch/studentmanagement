using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class GradeController(IGradeService gradeService):ControllerBase
{
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
// [HttpGet("ave")]
//     public async   Task<Response<List<Grade>>> GetGradeAverageAsync()
//     {
//         return await gradeService.GetGradeAverageAsync();
//     }
}