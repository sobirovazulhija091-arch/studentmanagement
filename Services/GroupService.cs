using System.Net;
using Dapper;
using Microsoft.EntityFrameworkCore;

public class GroupService(ApplicationDbcontext dbcontext):IGroupService
{
    private readonly ApplicationDbcontext _dbcontext=dbcontext;

    public async Task<Response<string>> AddAsync(GroupDto groupDto)
    {
         Group group = new Group
         {
             Name=groupDto.Name,
             Stratdate=groupDto.Stratdate,
             Enddate=groupDto.Enddate,
             IsActive=groupDto.IsActive,
             Curatorteacherid=groupDto.Curatorteacherid
         };
         _dbcontext.Groups.Add(group);
        await _dbcontext.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK,"ok");       
    }

    public async Task<Response<string>> DeleteAsync(int groupid)
    {
         var res = await _dbcontext.Groups.FindAsync(groupid);
         _dbcontext.Groups.Remove(res);
          await _dbcontext.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK,"ok");
    }

    public async  Task<Response<List<Group>>> GetAsync()
    {
        return new Response<List<Group>>(HttpStatusCode.OK,"ok",await _dbcontext.Groups.ToListAsync());
    }

    public async Task<Response<Group>> GetByIdAsync(int groupid)
    {
        var res = await _dbcontext.Groups.FindAsync(groupid);
        return new Response<Group>(HttpStatusCode.OK,"ok",res);
    }

    public async Task<Response<List<Group>>> GetListOfStudentsAsync()
    {
         var res = await _dbcontext.Groups.Include(a=>a.Students).ToListAsync();
        return new Response<List<Group>>(HttpStatusCode.OK,"ok",res);
    }

    public async Task<Response<string>> UpdateActiveAsync(int groupid,bool active)
    {
        var group = await _dbcontext.Groups.FirstOrDefaultAsync(a=>a.Id==groupid);
        if (group==null)
        {
             return new Response<string>(HttpStatusCode.NotFound,"Not Found");
        }
          group.IsActive=active;
          await _dbcontext.SaveChangesAsync();
          return new Response<string>(
        HttpStatusCode.OK,
        "Updated successfully"
    );
    }

    public async Task<Response<string>> UpdateAsync(int groupid,UpdateGroupDto updateGroupDto)
    {
            var g = await _dbcontext.Groups.FindAsync(groupid);
            g.Name=updateGroupDto.Name;
            g.Stratdate=updateGroupDto.Stratdate;
            g.Enddate=updateGroupDto.Enddate;
        g.IsActive=updateGroupDto.IsActive;
        g.Curatorteacherid=updateGroupDto.Curatorteacherid;
        
      await _dbcontext.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK,"ok");
    }
}