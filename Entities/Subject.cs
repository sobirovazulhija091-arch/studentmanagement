using System.ComponentModel.DataAnnotations;

public class Subject:BaseEntite
{
  [MaxLength(100)]
     public string Name{get;set;}=null!;
     [MaxLength(200)]
     public string Description{get;set;}=null!;
     public string Hours{get;set;}=null!; 
     public List<Enrollment> Enrollments{get;set;}=[];
       public List<Grade> Grades{get;set;}=[];
}