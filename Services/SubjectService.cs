using System.Net;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
public class SubjectService(ApplicationDbContext dbcontext,IMemoryCache cache,ILogger<SubjectService> logger):ISubjectService
{
  private readonly ApplicationDbContext _dbcontext = dbcontext;
  private readonly IMemoryCache _cache=cache;
  private readonly ILogger<SubjectService> _logger=logger;
  private const string CacheKey = "subjects_all";

    public async Task<Response<string>> AddAsync(SubjectDto subjectDto)
    {
        try
        {
            Subject subject = new Subject
          {
              Name=subjectDto.Name,
              Description=subjectDto.Description,
              Hours=subjectDto.Hours
          };
           _dbcontext.Subjects.Add(subject);
          await _dbcontext.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK,"ok");  
        }
        catch (System.Exception)
        {
        return new Response<string>(HttpStatusCode.InternalServerError,"InternalServerError");  
        }
    }

    public async Task<Response<string>> DeleteAsync(int subjectid)
    {
          try
        {
              var res = await _dbcontext.Subjects.FindAsync(subjectid);
           _dbcontext.Subjects.Remove(res);
          await _dbcontext.SaveChangesAsync();
         return new Response<string>(HttpStatusCode.OK,"ok");
        }
        catch (System.Exception)
        {
        return new Response<string>(HttpStatusCode.NotFound,"Not Found");  
        }
    }

    public async Task<Response<List<Subject>>> GetAsync()
    {
        //  try
        // {
        //       return new Response<List<Subject>>(HttpStatusCode.OK,"ok",await _dbcontext.Subjects.ToListAsync());
        // }
        // catch (System.Exception)
        // {
        // return new Response<List<Subject>>(HttpStatusCode.NotFound,"Not Found");  
        // }
        var subjects = await  _cache.GetOrCreateAsync(
        CacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
            entry.SlidingExpiration = TimeSpan.FromSeconds(1);
            entry.Priority =CacheItemPriority.High;
            
            _logger.LogInformation("Cache miss for subjects. Fetching from database...");
            await Task.Delay(1000); 
            return await _dbcontext.Subjects.ToListAsync();
        });
        return new Response<List<Subject>>(HttpStatusCode.OK, "ok", subjects);
    }

    public async Task<Response<Subject>> GetByIdAsync(int subjectid)
    {
        try
        {
            var res = await _dbcontext.Subjects.FindAsync(subjectid);
        return new Response<Subject>(HttpStatusCode.OK,"ok",res);
        }
        catch (System.Exception)
        {
        return new Response<Subject>(HttpStatusCode.NotFound,"Not Found");  
        }
    }
    public async Task<Response<string>> UpdateAsync(int subjectid,UpdateSubjectDto updateSubjectDto)
    {
        var sub = await _dbcontext.Subjects.FindAsync(subjectid);
         sub.Name=updateSubjectDto.Name;
         sub.Description=updateSubjectDto.Description;
         sub.Hours=updateSubjectDto.Hours;
             
         return new Response<string>(HttpStatusCode.OK,"Update succssefully");
    }
}
