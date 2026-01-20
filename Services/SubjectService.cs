using System.Net;
using Dapper;
using Microsoft.EntityFrameworkCore;
public class SubjectService(ApplicationDbcontext dbcontext):ISubjectService
{
  private readonly ApplicationDbcontext _dbcontext = dbcontext;

    public async Task<Response<string>> AddAsync(SubjectDto subjectDto)
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

    public async Task<Response<string>> DeleteAsync(int subjectid)
    {
          var res = await _dbcontext.Subjects.FindAsync(subjectid);
           _dbcontext.Subjects.Remove(res);
          await _dbcontext.SaveChangesAsync();
         return new Response<string>(HttpStatusCode.OK,"ok");
    }

    public async Task<Response<List<Subject>>> GetAsync()
    {
        return new Response<List<Subject>>(HttpStatusCode.OK,"ok",await _dbcontext.Subjects.ToListAsync());
    }

    public async Task<Response<Subject>> GetByIdAsync(int subjectid)
    {
       var res = await _dbcontext.Subjects.FindAsync(subjectid);
        return new Response<Subject>(HttpStatusCode.OK,"ok",res);
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