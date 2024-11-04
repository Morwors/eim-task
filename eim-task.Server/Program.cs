using eim_task.Server.Data;
using eim_task.Server.Repositories.Implementations;
using eim_task.Server.Repositories.Interfaces;
using eim_task.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using YourProject.Services;

var builder = WebApplication.CreateBuilder(args);

var dbType = builder.Configuration["DatabaseSettings:Type"];

if (dbType == "SQL")
{
    var connectionString = builder.Configuration["DatabaseSettings:ConnectionString"];
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddScoped<ITaskRepository, SqlTaskRepository>();

}
else if (dbType == "File")
{
    builder.Services.AddSingleton<ITaskRepository, FileTaskRepository>();
}
builder.Services.AddScoped<IJSTaskService, JSTaskService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.Run();
