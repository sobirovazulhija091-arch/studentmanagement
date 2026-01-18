using System.Net;
using Dapper;

public class GradeService(ApplicationDbcontext dbcontext):IGradeService
{
       private ApplicationDbcontext _dbcontext=dbcontext;

    public async Task<Response<string>> AddAsync(GradeDto gradeDto)
    {
         using var conn= _dbcontext.Connection();
        var query=@"insert into grades(studentid,subjectid,teacherid,gradevalue,gradetype,comment) 
         values(@studentid,@subjectid,@teacherid,@gradevalue,@gradetype,@comment)";
        var res=await conn.ExecuteAsync(query,new{studentid=gradeDto.StudentId,subjectid=gradeDto.SubjectId,
        teacherid=gradeDto.TeacherId,gradevalue=gradeDto.GradeValue,gradetype=gradeDto.GradeType,comment=gradeDto.Comment});
        return res==0? new Response<string>(HttpStatusCode.InternalServerError,"Error")
        :new Response<string>(HttpStatusCode.OK,"ok");      
    }

    public async Task<List<Grade>> GetAsync()
    {
         using var conn = _dbcontext.Connection();
        var query="select * from grades";
        var res = await conn.QueryAsync<Grade>(query);
        return res.ToList();
    }

    public async Task<List<Grade>> GetGradeAsync()
    {
         using var conn = _dbcontext.Connection();
        var query="select g.*,st.fullname,su.name from grades g join students st on st.id=g.studentid join subjects su on su.id=g.subject";
        var res = await conn.QueryAsync<Grade>(query);
        return res.ToList();
    }

    public async Task<List<Grade>> GetGradeAverageAsync()
    {
         using var conn = _dbcontext.Connection();
        var query="select g.avg(gradevalue),st.fullname from grades g join students st on st.id=g.studentid ";
        var res = await conn.QueryAsync<Grade>(query);
        return res.ToList();
    }
}

