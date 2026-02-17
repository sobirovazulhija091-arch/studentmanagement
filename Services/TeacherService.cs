using System.Net;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
public class TeacherService(ApplicationDbContext dbcontext,IMemoryCache cache,ILogger<TeacherService> logger):ITeacherService
{
    private ApplicationDbContext _dbcontext=dbcontext;
    private readonly IMemoryCache _cache=cache;
    private readonly ILogger<TeacherService> _logger=logger;
    private const string CacheKey = "teachers_all";

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
        // try
        // {
        //            return new Response<List<Teacher>>(HttpStatusCode.OK,"ok",await _dbcontext.Teachers.ToListAsync());
        // }
        // catch (System.Exception)
        // {
        //         return new Response<List<Teacher>>(HttpStatusCode.InternalServerError,"Internal Server Error");
        // }
        var teacher = await _cache.GetOrCreateAsync(CacheKey ,async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(40);
            entry.SlidingExpiration = TimeSpan.FromSeconds(1000);
            entry.Priority = CacheItemPriority.High;
                _logger.LogInformation("Cache miss for teachers. Fetching from database...");
            await Task.Delay(1000); 
            return await _dbcontext.Teachers.ToListAsync();
        });
        return new Response<List<Teacher>>(HttpStatusCode.OK,"ok",teacher);
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