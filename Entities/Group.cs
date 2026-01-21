public class Group:BaseEntite

{
     public string Name{get;set;}=null!;
     public string? Stratdate{get;set;}
     public string? Enddate{get;set;}
     public bool IsActive{get;set;}=true;
     public int Curatorteacherid {get;set;}
     public List<Student> Students{get;set;}=[];
}