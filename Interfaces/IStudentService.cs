public interface IStudentService
{
     Task<Response<string>> AddAsync(StudentDto studentDto);
     Task<Response<List<Student>>> GetAsync();//name,email,phone,groupid,isactivite
     Task<Response<Student>> GetByIdAsync(int studentid);//gruop name join
     Task<Response<string>> UpdateAsync(int studentid,UpdateStudentDto updateStudentDto);
     Task<Response<string>> UpdateGroupIdAsync(int studentid,int newgroupid);
     Task<Response<string>> DeleteAsync(int studentid);
     
}