using System.Numerics;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class StudentController(IStudentService studentService):ControllerBase
{
    [HttpPost]
    public async Task<Response<string>> AddAsync(StudentDto studentDto)
    {
       return await  studentService.AddAsync(studentDto);
    }
    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int studentid)
    {
       return await studentService.DeleteAsync(studentid);
    }
    [HttpGet]
    public async Task<List<Student>> GetAsync()
    {
       return await studentService.GetAsync();
    }
    [HttpGet("studentid")]
    public async Task<Response<Student>> GetByIdAsync(int studentid)
    {
          return await studentService.GetByIdAsync(studentid);
    }
     [HttpPut]
    public async Task<Response<string>> UpdateAsync(UpdateStudentDto updateStudentDto)
    {
        return await studentService.UpdateAsync(updateStudentDto);
    }
    [HttpPatch]
    public async Task<Response<string>> UpdateGroupIdAsync(int studentid)
    {
        return await studentService.UpdateGroupIdAsync(studentid);
    }
}
