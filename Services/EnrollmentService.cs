using System.Net;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
public class EnrollmentService(ApplicationDbContext dbcontext,IMemoryCache cache,ILogger<EnrollmentService> logger):IEnrollmentService
{
    private readonly ApplicationDbContext _dbcontext=dbcontext;
     private readonly IMemoryCache _cache=cache;
       private readonly ILogger<EnrollmentService> _logger=logger;
       private const string CacheKey = "enrollments_all";
    public async Task<Response<string>> AddAsync(EnrollmentDto enrollmentDto)
    {
          try
          {
             Enrollment enrollment=new Enrollment
        {
            StudentId=enrollmentDto.StudentId,
            SubjectId=enrollmentDto.SubjectId,
            IsActive=enrollmentDto.IsActive
        };
           _dbcontext.Enrollments.Add(enrollment);
           await _dbcontext.SaveChangesAsync();      
            return new Response<string>(HttpStatusCode.OK,"ok");  
          }
          catch (System.Exception)
          {
            
             return new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error"); 
          }    
          }
    public async Task<Response<List<Enrollment>>> GetAsync()
    {
        // try
        // {
        //      return new Response<List<Enrollment>>(HttpStatusCode.OK,"ok",await  _dbcontext.Enrollments.ToListAsync());
        // }
        // catch (System.Exception ex)
        // {
        //     Console.WriteLine(ex);
        //     return new Response<List<Enrollment>>(HttpStatusCode.InternalServerError,"Internal Server Error");
        // }
      var enrolmetn =  await _cache.GetOrCreateAsync(
        CacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
            entry.SlidingExpiration = TimeSpan.FromSeconds(1);
            entry.Priority =CacheItemPriority.High;
            
            _logger.LogInformation("Cache miss for enrollments. Fetching from database...");
            await Task.Delay(1000); 
            return await _dbcontext.Enrollments.ToListAsync();}
      );
   return new Response<List<Enrollment>>(HttpStatusCode.OK, "ok", enrolmetn);
    }
     public async Task<Response<string>> UpdateActiveAsync(int enrollmentId, bool active)
    {
    try
    {
        var enrollment = await _dbcontext.Enrollments.FirstOrDefaultAsync(e => e.Id == enrollmentId);
    if (enrollment == null)
    {
        return new Response<string>(
            HttpStatusCode.NotFound,
            "Enrollment not found"
        );
    }
    enrollment.IsActive = active;
    await _dbcontext.SaveChangesAsync();

    return new Response<string>(
        HttpStatusCode.OK,
        "Updated successfully"
     );   
    }
    catch (System.Exception ex)
    {
         Console.WriteLine(ex);
          return new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
    }
    }

}