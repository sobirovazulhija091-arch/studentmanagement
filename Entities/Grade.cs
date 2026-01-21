using System.ComponentModel.DataAnnotations;

public class Grade:BaseEntite
{
    public int StudentId{get;set;}
    public int SubjectId{get;set;}
    public int TeacherId{get;set;}
    public Teacher? Teacher{get;set;}
    public Student? Student{get;set;}
     public Subject? Subject{get;set;} 
     [MaxLength(50)]
    public string GradeType{get;set;}=null!;
     public int GradeValue{get;set;}
    public DateTime GradedAt{get;set;}=DateTime.UtcNow;
       [MaxLength(150)]
    public string Comment{get;set;}=null!;
}