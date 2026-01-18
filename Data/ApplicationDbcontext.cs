using Npgsql;
public class ApplicationDbcontext
{
     private string connString="Host=localhost;Port=5432;Database=management;Username=postgres;Password=1234";
     public NpgsqlConnection Connection()=> new NpgsqlConnection(connString);
}