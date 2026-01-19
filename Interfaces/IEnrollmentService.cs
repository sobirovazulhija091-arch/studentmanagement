public interface IEnrollmentService
{
     Task<Response<string>> AddAsync(EnrollmentDto enrollmentDto);
     Task<Response<List<Enrollment>>> GetAsync();//stdent whit subject
     Task<Response<string>> UpdateActiveAsync(int enrollmentid,bool active); 
}