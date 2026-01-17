public class Grader:BaseEntite
{
    public int StudentId{get;set;}
    public int SubjectId{get;set;}
    public int TeacherId{get;set;}
    public int GradeValue{get;set;}
    public GradType GradeType{get;set;}
    public DateTime GradedAt{get;set;}=DateTime.UtcNow;
    public string Comment{get;set;}=null!;
}