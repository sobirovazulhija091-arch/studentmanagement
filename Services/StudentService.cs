
using Dapper;
using System.Net;
public class StudentService(ApplicationDbcontext dbcontext):IStudentService
{ 
    private readonly ApplicationDbcontext _dbcontext=dbcontext;

    public async Task<Response<string>> AddAsync(StudentDto studentDto)
    {
      
        using var conn = _dbcontext.Connection();
        var query="insert into students(fullname,birthdate,phone,isactive,groupid) values(@fullname,@birthdate,@phone,@isactive,@groupid)";
        var res = await conn.ExecuteAsync(query,studentDto);
         return res==0? new Response<string>(HttpStatusCode.NotFound,"notfound")
        :new Response<string>(HttpStatusCode.OK,"ok");
    }

    public async Task<Response<string>> DeleteAsync(int studentid)
    {
       using var conn = _dbcontext.Connection();
        var query="delete  from students where id=@Id";
        var res = await conn.ExecuteAsync(query,new{Id=studentid});
        return res==0? new Response<string>(HttpStatusCode.NotFound,"notfound")
        :new Response<string>(HttpStatusCode.OK,"ok");
    }

    public async Task<List<Student>> GetAsync()
    {
        using var conn = _dbcontext.Connection();
        var query="select * from students";
        var res = await conn.QueryAsync<Student>(query);
        return res.ToList();
    }

    public async Task<Response<Student>> GetByIdAsync(int studentid)
    {
       using var conn = _dbcontext.Connection();
        var query="select * from students where id=@Id";
        var res = await conn.QueryFirstOrDefaultAsync<Student>(query,new{Id=studentid});
         return res==null? new Response<Student>(HttpStatusCode.NotFound,"notfound")
        :new Response<Student>(HttpStatusCode.OK,"ok");
    }

    public async Task<Response<string>> UpdateAsync(UpdateStudentDto updateStudentDto)
    {
        using var conn = _dbcontext.Connection();
        var query="update  students set fullname=@Fullname,birthdate=@Birthdate,phone=@Phone,isactive=@Isactive,groupid=@GroupId where id=@Id";
        var res = await conn.ExecuteAsync(query,updateStudentDto);
         return res==0? new Response<string>(HttpStatusCode.NotFound,"notfound")
        :new Response<string>(HttpStatusCode.OK,"ok");
    }

    public async Task<Response<string>> UpdateGroupIdAsync(int studentid)
    {
        using var conn = _dbcontext.Connection();
        var query="update  students set groupid=@GroupId where id=@Id";
        var res = await conn.ExecuteAsync(query,new{Id=studentid});
        return res==0? new Response<string>(HttpStatusCode.NotFound,"notfound")
        :new Response<string>(HttpStatusCode.OK,"ok");
    }
}