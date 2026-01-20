public interface ISubjectService
{
     Task<Response<string>> AddAsync(SubjectDto subjectDto);
     Task<Response<List<Subject>>> GetAsync();//count
     Task<Response<Subject>> GetByIdAsync(int subjectid);
     Task<Response<string>> UpdateAsync(int subjectid,UpdateSubjectDto updateSubjectDto);
     Task<Response<string>> DeleteAsync(int subjectid);
     
}