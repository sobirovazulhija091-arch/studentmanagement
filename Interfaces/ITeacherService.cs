public interface ITeacherService
{
     Task<Response<string>> AddAsync(TeacherDto teacherDto);
     Task<Response<List<Teacher>>> GetAsync();
     Task<Response<Teacher>> GetByIdAsync(int teacherid);
     Task<Response<string>> UpdateAsync(int teacherid,UpdateTeacherDto updateTeacherDto);
     Task<Response<string>> UpdateActiveAsync(int teacherid,bool active);
     Task<Response<string>> DeleteAsync(int teacherid);
}