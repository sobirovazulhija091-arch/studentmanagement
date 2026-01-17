public interface IGroupService
{
     Task<Response<string>> AddAsync(GroupDto groupDto);
     Task<List<Group>> GetAsync();//count
     Task<Response<Group>> GetByIdAsync(int groupid);
       Task<List<Group>> GetListOfStudentsAsync();//list students
     Task<Response<string>> UpdateAsync(UpdateGroupDto updateGroupDto);
     Task<Response<string>> UpdateActiveAsync(int groupid);
     Task<Response<string>> DeleteAsync(int groupid);
     
}