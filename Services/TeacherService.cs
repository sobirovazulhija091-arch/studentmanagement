using System.Net;
using Dapper;
using Microsoft.EntityFrameworkCore;
public class TeacherService(ApplicationDbcontext dbcontext):ITeacherService
{
    private ApplicationDbcontext _dbcontext=dbcontext;

    public async Task<Response<string>> AddAsync(TeacherDto teacherDto)
    {
        try
        {
             Teacher teacher = new Teacher
         {
             Fullname=teacherDto.Fullname,
             Phone=teacherDto.Phone,
             IsActive=teacherDto.IsActive,
            HiredAt=teacherDto.HiredAt
         };
         _dbcontext.Teachers.Add(teacher);
          await _dbcontext.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.Created,"Created successfully");      
        }
        catch (System.Exception)
        {
        return new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error");      
        }
    }

    public async Task<Response<string>> DeleteAsync(int teacherid)
    {
        try
        {
         var res = await _dbcontext.Teachers.FindAsync(teacherid);
         _dbcontext.Teachers.Remove(res);
          await _dbcontext.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK,"ok");   
        }
        catch (System.Exception)
        {
        return new Response<string>(HttpStatusCode.NoContent,"Success, but no data in the response");   
        }
    }

    public async Task<Response<List<Teacher>>> GetAsync()
    {
        try
        {
                   return new Response<List<Teacher>>(HttpStatusCode.OK,"ok",await _dbcontext.Teachers.ToListAsync());
        }
        catch (System.Exception)
        {
                return new Response<List<Teacher>>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }
    }

    public async Task<Response<Teacher>> GetByIdAsync(int teacherid)
    {
    try
    {
         var res = await _dbcontext.Teachers.FindAsync(teacherid);
        return new Response<Teacher>(HttpStatusCode.OK,"ok",res);
    }
    catch (System.Exception)
    {
        return new Response<Teacher>(HttpStatusCode.NotFound,"Not Found");
    }
    }

    public async Task<Response<string>> UpdateActiveAsync(int teacherid,bool active)
    {
    try
    {
         var teacher = await _dbcontext.Teachers.FirstOrDefaultAsync(a=>a.Id==teacherid);
        if (teacher==null)
        {
             return new Response<string>(HttpStatusCode.NotFound,"Not Found");
        }
          teacher.IsActive=active;
          await _dbcontext.SaveChangesAsync();
          return new Response<string>(HttpStatusCode.OK,"Updated successfully");
    }
    catch (System.Exception)
    {
             return new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
    }
    }

    public async Task<Response<string>> UpdateAsync(int teacherid,UpdateTeacherDto updateTeacherDto)
    {
         var teach = await _dbcontext.Teachers.FindAsync(teacherid);
        teach.Fullname=updateTeacherDto.Fullname;
        teach.Phone=updateTeacherDto.Phone;
        teach.HiredAt=updateTeacherDto.HiredAt;
        teach.IsActive=updateTeacherDto.IsActive;
       await _dbcontext.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK,"ok");
    }
}