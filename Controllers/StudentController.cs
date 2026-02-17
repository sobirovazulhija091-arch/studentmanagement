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
    public async Task<Response<List<Student>>> GetAsync()
    {
       return await studentService.GetAsync();
    }
    [HttpGet("studentid")]
    public async Task<Response<Student>> GetByIdAsync(int studentid)
    {
          return await studentService.GetByIdAsync(studentid);
    }
     [HttpPut]
    public async Task<Response<string>> UpdateAsync(int studentid,UpdateStudentDto updateStudentDto)
    {
        return await studentService.UpdateAsync(studentid,updateStudentDto);
    }
    [HttpPatch]
    public async Task<Response<string>> UpdateGroupIdAsync(int studentid,int newgroupid)
    {
        return await studentService.UpdateGroupIdAsync(studentid,newgroupid);
    }
}
