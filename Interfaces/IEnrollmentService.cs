public interface IEnrollmentService
{
     Task<Response<string>> AddAsync(EnrollmentDto enrollmentDto);
     Task<List<Enrollment>> GetAsync();//stdent whit subject
     Task<Response<string>> UpdateActiveAsync(int enrollmentid);
     Task<Response<string>> DeleteAsync(int enrollmentid);  
}