public class Group:BaseEntite
{
     public string Name{get;set;}=null!;
     public int Stratdate{get;set;}
     public int Enddate{get;set;}
     public bool IsActive{get;set;}=true;
     public int Curatorteacherid {get;set;}
}