using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]
public class GroupController(IGroupService groupService):ControllerBase
{
     [HttpPost]
    public async Task<Response<string>> AddAsync(GroupDto groupDto)
    {
            return await groupService.AddAsync(groupDto);
    }
    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int groupid)
    {
       return await groupService.DeleteAsync(groupid);
    }
[HttpGet]
    public async  Task<List<Group>> GetAsync()
    {
          return await groupService.GetAsync();
    }
    [HttpGet("groupid")]
    public async Task<Response<Group>> GetByIdAsync(int groupid)
    {
          return await groupService.GetByIdAsync(groupid);
    }
[HttpGet("whit students")]
    public async Task<List<Group>> GetListOfStudentsAsync()
    {
          return await groupService.GetListOfStudentsAsync();
    }
    [HttpPatch]
    public async Task<Response<string>> UpdateActiveAsync(int groupid,bool active)
    {
        return await groupService.UpdateActiveAsync(groupid,active);
    }
     [HttpPut]
    public async Task<Response<string>> UpdateAsync(UpdateGroupDto updateGroupDto)
    {
        return await groupService.UpdateAsync(updateGroupDto);
    }

}