public interface ISubjectService
{
     Task<Response<string>> AddAsync(SubjectDto subjectDto);
     Task<List<Subject>> GetAsync();//count
     Task<Response<Subject>> GetByIdAsync(int subjectid);
     Task<List<Subject>> GetListOfStudentsAsync(int subjectid);//list students
     Task<Response<string>> UpdateAsync(UpdateSubjectDto updateSubjectDto);
     Task<Response<string>> DeleteAsync(int subjectid);
     
}