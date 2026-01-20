public class Subject:BaseEntite
{
     public string Name{get;set;}=null!;
     public string Description{get;set;}=null!;
     public string Hours{get;set;}=null!; 
     public List<Enrollment> Enrollments{get;set;}=[];
       public List<Grade> Grades{get;set;}=[];
}