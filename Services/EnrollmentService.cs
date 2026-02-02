using System.Net;
using Dapper;
using Microsoft.EntityFrameworkCore;
public class EnrollmentService(ApplicationDbcontext dbcontext):IEnrollmentService
{
    private readonly ApplicationDbcontext _dbcontext=dbcontext;
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
        try
        {
             return new Response<List<Enrollment>>(HttpStatusCode.OK,"ok",await  _dbcontext.Enrollments.ToListAsync());
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            return new Response<List<Enrollment>>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }


    }//stdent whit subject
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