public interface IStudentService
{
     Task<Response<string>> AddAsync(StudentDto studentDto);
     Task<List<Student>> GetAsync();//name,email,phone,groupid,isactivite
     Task<Response<Student>> GetByIdAsync(int studentid);//gruop name join
     Task<Response<string>> UpdateAsync(UpdateStudentDto updateStudentDto);
     Task<Response<string>> UpdateGroupIdAsync(int studentid);
     Task<Response<string>> DeleteAsync(int studentid);
     
}