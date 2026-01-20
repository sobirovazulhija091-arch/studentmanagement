public class Teacher:BaseEntite
{
     public string Fullname{get;set;}=null!;
     public int Phone{get;set;}
     public bool IsActive{get;set;}=true;
     public string HiredAt{get;set;}=null!;
     public List<Grade> Grades{get;set;}=[];
}