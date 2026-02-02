using System.Net;
using Dapper;
using Microsoft.EntityFrameworkCore;

public class GradeService(ApplicationDbcontext dbcontext):IGradeService
{
       private ApplicationDbcontext _dbcontext=dbcontext;
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
        try
        {
        return new Response<List<Grade>>(HttpStatusCode.OK,"ok",await _dbcontext.Grades.ToListAsync());
        }
        catch (System.Exception)
        {
        return new Response<List<Grade>>(HttpStatusCode.InternalServerError,"Internal Server Error");
            
        }
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

    // public async Task<List<Grade>> GetGradeAverageAsync()
    // {
    //      using var conn = _dbcontext.Connection();
    //     var query="select g.avg(gradevalue),st.fullname from grades g join students st on st.id=g.studentid ";
    //     var res = await conn.QueryAsync<Grade>(query);
    //     return res.ToList();
    // }
}

