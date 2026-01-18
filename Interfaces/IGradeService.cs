public interface IGradeService
{
    Task<Response<string>> AddAsync(GradeDto gradeDto);
    Task<List<Grade>> GetAsync();
    Task<List<Grade>> GetGradeAsync();
    Task<List<Grade>> GetGradeAverageAsync();

}