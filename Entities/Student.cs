using System.Net.NetworkInformation;

public class Student:BaseEntite
{
    public string Fullname{get;set;}=null!;
    public int Birthdate{get;set;}
     public int Phone{get;set;}
     public bool IsActive{get;set;}=true;
     public int GroupId{get;set;}
     public Group? Group{get;set;}
     public List<Enrollment> Enrollments{get;set;}=[];
     public List<Grade> Grades{get;set;}=[];
};