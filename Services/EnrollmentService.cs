using System.Net;
using Dapper;
using Microsoft.EntityFrameworkCore;
public class EnrollmentService(ApplicationDbcontext dbcontext):IEnrollmentService
{
    private readonly ApplicationDbcontext _dbcontext=dbcontext;
    public async Task<Response<string>> AddAsync(EnrollmentDto enrollmentDto)
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
    public async Task<Response<List<Enrollment>>> GetAsync()
    {
             return new Response<List<Enrollment>>(HttpStatusCode.OK,"ok",await  _dbcontext.Enrollments.ToListAsync());

    }//stdent whit subject
  
        public async Task<Response<string>> UpdateActiveAsync(int enrollmentId, bool active)
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

}