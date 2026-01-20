
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
public class StudentService(ApplicationDbcontext dbcontext):IStudentService
{ 
    private readonly ApplicationDbcontext _dbcontext=dbcontext;

    public async Task<Response<string>> AddAsync(StudentDto studentDto)
    {
       Student student = new Student
       {
           Fullname=studentDto.Fullname,
           Birthdate=studentDto.Birthdate,
           Phone=studentDto.Phone,
           IsActive=studentDto.IsActive,
           GroupId=studentDto.GroupId
       }; 
         _dbcontext.Students.Add(student);
        await _dbcontext.SaveChangesAsync();
         return new Response<string>(HttpStatusCode.OK,"ok");
    }

    public async Task<Response<string>> DeleteAsync(int studentid)
    {
       var res = await _dbcontext.Students.FindAsync(studentid);
         _dbcontext.Students.Remove(res);
          await _dbcontext.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK,"ok");
    }

    public async Task<Response<List<Student>>> GetAsync()
    {
        return new Response<List<Student>>(HttpStatusCode.OK,"ok",await _dbcontext.Students.ToListAsync());
    }

    public async Task<Response<Student>> GetByIdAsync(int studentid)
    {
       var res = await _dbcontext.Students.FindAsync(studentid);
        return new Response<Student>(HttpStatusCode.OK,"ok",res);
    }

    public async Task<Response<string>> UpdateAsync(int studentid,UpdateStudentDto updateStudentDto)
    {
          var s = await _dbcontext.Students.FindAsync(studentid);
            s.Fullname=updateStudentDto.Fullname;
            s.Birthdate=updateStudentDto.Birthdate;
            s.GroupId=updateStudentDto.GroupId;
           s.IsActive=updateStudentDto.IsActive;
        
      await _dbcontext.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK,"ok");
    }

    public async Task<Response<string>> UpdateGroupIdAsync(int studentid,int newgroupid)
    {
        var stu = await _dbcontext.Students.FirstOrDefaultAsync(a=>a.Id==studentid);
        if (stu==null)
        {
             return new Response<string>(HttpStatusCode.NotFound,"Not Found");
        }
          stu.GroupId=newgroupid;
          await _dbcontext.SaveChangesAsync();
          return new Response<string>(
        HttpStatusCode.OK,
        "Updated successfully"
    );
    }
}