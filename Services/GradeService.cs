using System.Net;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

public class GradeService(ApplicationDbContext dbcontext,IMemoryCache cache,ILogger<GradeService> logger):IGradeService
{
       private ApplicationDbContext _dbcontext=dbcontext;
       private readonly IMemoryCache _cache=cache;
       private readonly ILogger<GradeService> _logger=logger;
       private const string CacheKey = "grades_all";

    public async Task<Response<string>> AddAsync(GradeDto gradeDto)
    {
        try
        {
               Grade grade = new Grade
          {
            StudentId=gradeDto.StudentId,
             SubjectId=gradeDto.SubjectId,
             TeacherId=gradeDto.TeacherId,
             GradeValue=gradeDto.GradeValue,
             GradeType=gradeDto.GradeType,
             Comment=gradeDto.Comment
         };
         _dbcontext.Grades.Add(grade);
        await _dbcontext.SaveChangesAsync();
         return new Response<string>(HttpStatusCode.OK,"ok");    
        }
        catch (System.Exception)
        {
            
             return new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }    
    }
    public async Task<Response<List<Grade>>> GetAsync()
    {
        // try
        // {
        // return new Response<List<Grade>>(HttpStatusCode.OK,"ok",await _dbcontext.Grades.ToListAsync());
        // }
        // catch (System.Exception)
        // {
        // return new Response<List<Grade>>(HttpStatusCode.InternalServerError,"Internal Server Error");
        // }
     var grades = await _cache.GetOrCreateAsync(CacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
            entry.SlidingExpiration = TimeSpan.FromSeconds(1);
            entry.Priority =CacheItemPriority.High;
            
            _logger.LogInformation("Cache miss for grades. Fetching from database...");
            await Task.Delay(1000); 
           return await _dbcontext.Grades.ToListAsync();
    });
    return new Response<List<Grade>>(HttpStatusCode.OK, "ok", grades);
    }
    public async Task<Response<List<Grade>>> GetGradeAsync()
    {
        try
        {
             var res = await _dbcontext.Grades.Include(a=>a.Subject).Include(g => g.Student).ToListAsync();
        return new Response<List<Grade>>(HttpStatusCode.OK,"ok",res);
        }
        catch (System.Exception)
        {
        return new Response<List<Grade>>(HttpStatusCode.NotFound,"Not Found");
        }
    }
}

