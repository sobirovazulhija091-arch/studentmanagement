using System.Net;
using Dapper;
public class EnrollmentService(ApplicationDbcontext dbcontext):IEnrollmentService
{
    private readonly ApplicationDbcontext _dbcontext=dbcontext;
    public async Task<Response<string>> AddAsync(EnrollmentDto enrollmentDto)
    {
         using var conn= _dbcontext.Connection();
        var query=@"insert into enrollments(studentid,subjectid,isactive) 
         values(@studentid,@subjectid,@isactive)";
        var res=await conn.ExecuteAsync(query,new{studentid=enrollmentDto.StudentId,
        subjectid=enrollmentDto.SubjectId,isactive=enrollmentDto.IsActive});
        return res==0? new Response<string>(HttpStatusCode.InternalServerError,"Error")
        :new Response<string>(HttpStatusCode.OK,"ok");  
    }
    public async Task<List<Enrollment>> GetAsync()
    {
           using var conn = _dbcontext.Connection();
        var query=@"select  e.*,sa.*,st.* from enrollments e join sabjects sa on sa.id=e.sabjectid join 
               students st on st.id=e.studentid where isactive=true";
        var res = await conn.QueryAsync<Enrollment>(query);
        return res.ToList();
    }//stdent whit subject
   public async Task<Response<string>> UpdateActiveAsync(int enrollmentid)
    {
        using var conn = _dbcontext.Connection();
        var query="update  enrollments set isactive=@Isactive  where id=@Id";
        var res = await conn.ExecuteAsync(query,new{Id=enrollmentid});
         return res==0? new Response<string>(HttpStatusCode.NotFound,"notfound")
        :new Response<string>(HttpStatusCode.OK,"ok");
    }

}