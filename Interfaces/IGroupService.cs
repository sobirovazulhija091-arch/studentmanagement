public interface IGroupService
{
     Task<Response<string>> AddAsync(GroupDto groupDto);
     Task<Response<List<Group>>> GetAsync();//count
     Task<Response<Group>> GetByIdAsync(int groupid);
      Task<Response<List<Group>>> GetListOfStudentsAsync();//list students
     Task<Response<string>> UpdateAsync(int groupid,UpdateGroupDto updateGroupDto);
     Task<Response<string>> UpdateActiveAsync(int groupid,bool active);
     Task<Response<string>> DeleteAsync(int groupid);
     
}