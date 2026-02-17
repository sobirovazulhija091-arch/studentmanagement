using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SubjectController(ISubjectService subjectService):ControllerBase
{
    [HttpPost]
     public async Task<Response<string>> AddAsync(SubjectDto subjectDto)
    {
        return await subjectService.AddAsync(subjectDto);
    }
[HttpDelete]
    public async Task<Response<string>> DeleteAsync(int subjectid)
    {
       return await subjectService.DeleteAsync(subjectid);
    }
[HttpGet]
    public async   Task<Response<List<Subject>>> GetAsync()
    {
        return await subjectService.GetAsync();  
    }
[HttpGet("subjectid")]
    public async Task<Response<Subject>> GetByIdAsync(int subjectid)
    {
        return await subjectService.GetByIdAsync(subjectid);
    }

[HttpPut]
    public async Task<Response<string>> UpdateAsync(int subjectid,UpdateSubjectDto updateSubjectDto)
    {
        return await subjectService.UpdateAsync(subjectid,updateSubjectDto);
    }
}