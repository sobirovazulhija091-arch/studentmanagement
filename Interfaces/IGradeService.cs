public interface IGradeService
{
    Task<Response<string>> AddAsync(GradeDto gradeDto);
    Task<Response<List<Grade>>> GetAsync();
    Task<Response<List<Grade>>> GetGradeAsync();
    //  Task<Response<List<Grade>>> GetGradeAverageAsync();

}