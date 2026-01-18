public class Grade:BaseEntite
{
    public int StudentId{get;set;}
    public int SubjectId{get;set;}
    public int TeacherId{get;set;}
    public int GradeValue{get;set;}
    public string GradeType{get;set;}=null!;
    public DateTime GradedAt{get;set;}=DateTime.UtcNow;
    public string Comment{get;set;}=null!;
}