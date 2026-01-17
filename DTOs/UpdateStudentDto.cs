public class UpdateStudentDto
{
    public int Id{get;set;}
    public string Fullname{get;set;}=null!;
    public int Birthdate{get;set;}
    public int Phone{get;set;}
    public bool IsActive{get;set;}=true;
    public int GroupId{get;set;}
}