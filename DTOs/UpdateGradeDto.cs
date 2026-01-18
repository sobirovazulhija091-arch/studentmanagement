public class UpdateGradeDto
{
    public int Id{get;set;}
    public int StudentId{get;set;}
    public int SubjectId{get;set;}
    public int TeacherId{get;set;}
    public int GradeValue{get;set;}
    public string GradeType{get;set;}=null!;
    public string Comment{get;set;}=null!;
}