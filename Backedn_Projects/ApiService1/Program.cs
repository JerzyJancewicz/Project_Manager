using ApiService1.Context;
using ApiService1.Mappers;
using ApiService1.Repositories;
using ApiService1.Seeders;
using ApiService1.Services;
using Microsoft.EntityFrameworkCore;
using System.Web.Http;
using System.Web.Http.Cors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ApiServiceDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ProjectSeeder>();
builder.Services.AddAutoMapper(typeof(ProjectMapper));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var scope = app.Services.CreateScope();
var seed = scope.ServiceProvider.GetRequiredService<ProjectSeeder>();
await seed.Seed();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
