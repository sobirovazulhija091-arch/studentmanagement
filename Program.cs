var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IStudentService,StudentService>();
builder.Services.AddScoped<IGroupService,GroupService>();
builder.Services.AddControllers();
builder.Services.AddScoped<ApplicationDbcontext>();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddLogging(cofig=>{cofig.AddConsole();});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
    app.UseSwagger();
}
app.UseMiddleware<RequestTimeMiddleware>();
    app.MapOpenApi();
 app.MapControllers();
app.Run();
