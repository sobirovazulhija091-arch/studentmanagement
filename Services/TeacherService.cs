// using System.Net;
// using Dapper;
// public class TeacherService(ApplicationDbcontext dbcontext):ITeacherService
// {
//     private ApplicationDbcontext _dbcontext=dbcontext;

//     public async Task<Response<string>> AddAsync(TeacherDto teacherDto)
//     {
//           using var conn= _dbcontext.Connection();
//         var query=@"insert into teachers(fullname,phone,isactive,hiredat) 
//          values(@fullname,@phone,@isactive,@hiredat)";
//         var res=await conn.ExecuteAsync(query,new{fullname=teacherDto.Fullname,phone=teacherDto.Phone,isactive=teacherDto.IsActive,hiredat=teacherDto.HiredAt});
//         return res==0? new Response<string>(HttpStatusCode.InternalServerError,"Error")
//         :new Response<string>(HttpStatusCode.OK,"ok");      
//     }

//     public async Task<Response<string>> DeleteAsync(int teacherid)
//     {
//         using var conn = _dbcontext.Connection();
//         var query="delete  from teachers where id=@Id";
//         var res = await conn.ExecuteAsync(query,new{Id=teacherid});
//         return res==0? new Response<string>(HttpStatusCode.NotFound,"notfound")
//         :new Response<string>(HttpStatusCode.OK,"ok");
//     }

//     public async Task<List<Teacher>> GetAsync()
//     {
//           using var conn = _dbcontext.Connection();
//         var query="select * from teachers";
//         var res = await conn.QueryAsync<Teacher>(query);
//         return res.ToList();
//     }

//     public async Task<Response<Teacher>> GetByIdAsync(int teacherid)
//     {
//          using var conn = _dbcontext.Connection();
//         var query="select * from teachers where id=@Id";
//         var res = await conn.QueryFirstOrDefaultAsync<Teacher>(query,new{Id=teacherid});
//          return res==null? new Response<Teacher>(HttpStatusCode.NotFound,"notfound")
//         :new Response<Teacher>(HttpStatusCode.OK,"ok");
//     }

//     public async Task<Response<string>> UpdateActiveAsync(int teacherid,bool active)
//     {
//         using var conn = _dbcontext.Connection();
//         var query="update  teachers set isactive=@Isactive  where id=@Id";
//         var res = await conn.ExecuteAsync(query,new{Isactive=active,Id=teacherid});
//          return res==0? new Response<string>(HttpStatusCode.NotFound,"notfound")
//         :new Response<string>(HttpStatusCode.OK,"ok");
//     }

//     public async Task<Response<string>> UpdateAsync(UpdateTeacherDto updateTeacherDto)
//     {
//          using var conn = _dbcontext.Connection();
//         var query=@"update  teachers set fullname=@Fullname,
//         bithdate=@Bithdate,phone=@Phone,hiredat=@Hiredat isactive=@Isactive  where id=@Id";
//         var res = await conn.ExecuteAsync(query,updateTeacherDto);
//          return res==0? new Response<string>(HttpStatusCode.NotFound,"notfound")
//         :new Response<string>(HttpStatusCode.OK,"ok");
//     }
// }