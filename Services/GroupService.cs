using System.Net;
using Dapper;
using Microsoft.EntityFrameworkCore;

public class GroupService(ApplicationDbcontext dbcontext):IGroupService
{
    private readonly ApplicationDbcontext _dbcontext=dbcontext;

    public async Task<Response<string>> AddAsync(GroupDto groupDto)
    {
        try
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
        catch (System.Exception)
        {
              return new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error");    
        }
    }

    public async Task<Response<string>> DeleteAsync(int groupid)
    {
        try
        {
           var res = await _dbcontext.Groups.FindAsync(groupid);
         _dbcontext.Groups.Remove(res);
          await _dbcontext.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK,"ok");  
        }
        catch (System.Exception)
        {
        return new Response<string>(HttpStatusCode.NotFound,"Not Found");  
        }
    }

    public async  Task<Response<List<Group>>> GetAsync()
    {
        try
        {
                    return new Response<List<Group>>(HttpStatusCode.OK,"ok",await _dbcontext.Groups.ToListAsync());

        }
        catch (System.Exception)
        {
                    return new Response<List<Group>>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }
    }

    public async Task<Response<Group>> GetByIdAsync(int groupid)
    {
        try
        {
             var res = await _dbcontext.Groups.FindAsync(groupid);
        return new Response<Group>(HttpStatusCode.OK,"ok",res);
        }
        catch (System.Exception)
        {
        return new Response<Group>(HttpStatusCode.NotFound,"Not Found");
        }
       
    }

    public async Task<Response<List<Group>>> GetListOfStudentsAsync()
    {
        try
        {
          var res = await _dbcontext.Groups.Include(a=>a.Students).ToListAsync();
        return new Response<List<Group>>(HttpStatusCode.OK,"ok",res);   
        }
        catch (System.Exception)
        {
        return new Response<List<Group>>(HttpStatusCode.NotFound,"Not Found");
        }
    }

    public async Task<Response<string>> UpdateActiveAsync(int groupid,bool active)
    {
        try
        {
             var group = await _dbcontext.Groups.FirstOrDefaultAsync(a=>a.Id==groupid);
        if (group==null)
        {
             return new Response<string>(HttpStatusCode.NotFound,"Not Found");
        }
          group.IsActive=active;
          await _dbcontext.SaveChangesAsync();
          return new Response<string>(HttpStatusCode.OK,"Updated successfully");
        }
        catch (System.Exception)
        {
          return new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }
    }

    public async Task<Response<string>> UpdateAsync(int groupid,UpdateGroupDto updateGroupDto)
    {
           try
           {
             var g = await _dbcontext.Groups.FindAsync(groupid);
            g.Name=updateGroupDto.Name;
            g.Stratdate=updateGroupDto.Stratdate;
            g.Enddate=updateGroupDto.Enddate;
            g.IsActive=updateGroupDto.IsActive;
            g.Curatorteacherid=updateGroupDto.Curatorteacherid;
            await _dbcontext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK,"ok" );
           }
           catch (System.Exception)
           {
            return new Response<string>(HttpStatusCode.NotFound,"Not Found");
           }
    }
}