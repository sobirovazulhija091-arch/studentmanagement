public class Enrollment:BaseEntite
{
   public int  StudentId{get;set;}
  public int SubjectId{get;set;}
   public DateTime EnrolledAt{get;set;}=DateTime.UtcNow;
  public bool IsActive{get;set;}=true;
}