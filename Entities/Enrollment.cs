
public class Enrollment:BaseEntite
{
   public int  StudentId{get;set;}
  public int SubjectId{get;set;}
  public Student? Student{get;set;}
  public Subject? Subject{get;set;}
   public DateTime EnrolledAt{get;set;}=DateTime.UtcNow;
  public bool IsActive{get;set;}=true;
}