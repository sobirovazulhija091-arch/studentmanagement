using System.Net;
using Dapper;
public class SubjectService(ApplicationDbcontext dbcontext):ISubjectService
{
  private readonly ApplicationDbcontext _dbcontext = dbcontext;

    public async Task<Response<string>> AddAsync(SubjectDto subjectDto)
    {
           using var conn= _dbcontext.Connection();
        var query=@"insert into subjects(name,description,hours) 
         values(@name,@description,@hours)";
        var res=await conn.ExecuteAsync(query,new{name=subjectDto.Name,description=subjectDto.Description,hours=subjectDto.Hours});
        return res==0? new Response<string>(HttpStatusCode.InternalServerError,"Error")
        :new Response<string>(HttpStatusCode.OK,"ok");  
    }

    public async Task<Response<string>> DeleteAsync(int subjectid)
    {
        using var conn = _dbcontext.Connection();
        var query="delete  from subjects where id=@Id";
        var res = await conn.ExecuteAsync(query,new{Id=subjectid});
        return res==0? new Response<string>(HttpStatusCode.NotFound,"notfound")
        :new Response<string>(HttpStatusCode.OK,"ok");
    }

    public async Task<List<Subject>> GetAsync()
    {
          using var conn = _dbcontext.Connection();
        var query="select * from subjects";
        var res = await conn.QueryAsync<Subject>(query);
        return res.ToList();
    }

    public async Task<Response<Subject>> GetByIdAsync(int subjectid)
    {
        using var conn = _dbcontext.Connection();
        var query="select * from subjects where id=@Id";
        var res = await conn.QueryFirstOrDefaultAsync<Subject>(query,new{Id=subjectid});
         return res==null? new Response<Subject>(HttpStatusCode.NotFound,"notfound")
        :new Response<Subject>(HttpStatusCode.OK,"ok");
    }

    public async Task<List<Subject>> GetListOfStudentsAsync(int subjectid)
    {
        using var conn = _dbcontext.Connection();
        var query="select su.*,st.fullname from subjects su join students st on st.id=su.studentid";
        var res = await conn.QueryAsync<Subject>(query);
        return res.ToList();
    }

    public async Task<Response<string>> UpdateAsync(UpdateSubjectDto updateSubjectDto)
    {
          using var conn = _dbcontext.Connection();
        var query=@"update subjects set name=@name,description=@Description,hours=@Hours where id=@Id";
        var res = await conn.ExecuteAsync(query,updateSubjectDto);
         return res==0? new Response<string>(HttpStatusCode.NotFound,"notfound")
        :new Response<string>(HttpStatusCode.OK,"ok");
    }
}