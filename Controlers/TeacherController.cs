using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TeacherController(ITeacherService teacherService):ControllerBase
{
    [HttpPost]
      public async Task<Response<string>> AddAsync(TeacherDto teacherDto)
    {
       return await teacherService.AddAsync(teacherDto);   
    }
[HttpDelete]
    public async Task<Response<string>> DeleteAsync(int teacherid)
    {
        return await teacherService.DeleteAsync(teacherid);
    }
[HttpGet]
    public async Task<List<Teacher>> GetAsync()
    {
          return await teacherService.GetAsync();
    }
[HttpGet("teacherid")]
    public async Task<Response<Teacher>> GetByIdAsync(int teacherid)
    {
        return await teacherService.GetByIdAsync(teacherid);
    }
[HttpPatch]
    public async Task<Response<string>> UpdateActiveAsync(int teacherid,bool active)
    {
         return await teacherService.UpdateActiveAsync(teacherid,active);
    }
[HttpPut]
    public async Task<Response<string>> UpdateAsync(UpdateTeacherDto updateTeacherDto)
    {
        return await teacherService.UpdateAsync(updateTeacherDto);
    }
}