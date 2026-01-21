using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

public class Student:BaseEntite
{
    [MaxLength(120)]
    public string Fullname{get;set;}=null!;
    public int Birthdate{get;set;}
    [MaxLength(20)]
     public int Phone{get;set;}
     public bool IsActive{get;set;}=true;
     public int GroupId{get;set;}
     public Group? Group{get;set;}
     public List<Enrollment> Enrollments{get;set;}=[];
     public List<Grade> Grades{get;set;}=[];
};