using System.Net;
using Dapper;

public class GroupService(ApplicationDbcontext dbcontext):IGroupService
{
    private readonly ApplicationDbcontext _dbcontext=dbcontext;

    public async Task<Response<string>> AddAsync(GroupDto groupDto)
    {
        using var conn= _dbcontext.Connection();
        var query="insert into groups(name,startdate,enddate,isactive,curatorteacherid) values(@name,@startdate,@enddate,@isactive,@curatorteacherid)";
        var res=await conn.ExecuteAsync(query,new{name=groupDto.Name,startdate=groupDto.Stratdate,enddate=groupDto.Enddate,isactive=groupDto.IsActive,curatorteacherid=groupDto.Curatorteacherid});
        return res==0? new Response<string>(HttpStatusCode.NotFound,"notfound")
        :new Response<string>(HttpStatusCode.OK,"ok");       
    }

    public async Task<Response<string>> DeleteAsync(int groupid)
    {
        using var conn = _dbcontext.Connection();
        var query="delete  from groups where id=@Id";
        var res = await conn.ExecuteAsync(query,new{Id=groupid});
        return res==0? new Response<string>(HttpStatusCode.NotFound,"notfound")
        :new Response<string>(HttpStatusCode.OK,"ok");
    }

    public async  Task<List<Group>> GetAsync()
    {
          using var conn = _dbcontext.Connection();
        var query="select * from groups";
        var res = await conn.QueryAsync<Group>(query);
        return res.ToList();
    }

    public async Task<Response<Group>> GetByIdAsync(int groupid)
    {
        using var conn = _dbcontext.Connection();
        var query="select * from groups where id=@Id";
        var res = await conn.QueryFirstOrDefaultAsync<Group>(query,new{Id=groupid});
         return res==null? new Response<Group>(HttpStatusCode.NotFound,"notfound")
        :new Response<Group>(HttpStatusCode.OK,"ok");
    }

    public async Task<List<Group>> GetListOfStudentsAsync()
    {
          using var conn = _dbcontext.Connection();
        var query="select s.*,g.name from groups g join students s on s.groupid=g.id";
        var res = await conn.QueryAsync<Group>(query);
        return res.ToList();
    }

    public async Task<Response<string>> UpdateActiveAsync(int groupid)
    {
        using var conn = _dbcontext.Connection();
        var query="update  groups set isactive=@Isactive  where id=@Id";
        var res = await conn.ExecuteAsync(query,new{Id=groupid});
         return res==0? new Response<string>(HttpStatusCode.NotFound,"notfound")
        :new Response<string>(HttpStatusCode.OK,"ok");
    }

    public async Task<Response<string>> UpdateAsync(UpdateGroupDto updateGroupDto)
    {
        using var conn = _dbcontext.Connection();
        var query=@"update  groups set name=@Name,startdate=@Startdate,enddate=@Enddate,
        isactive=@Isactive,curatorteacherid=@Curatorteacherid  where id=@Id";
        var res = await conn.ExecuteAsync(query,updateGroupDto);
         return res==0? new Response<string>(HttpStatusCode.NotFound,"notfound")
        :new Response<string>(HttpStatusCode.OK,"ok");
    }
}