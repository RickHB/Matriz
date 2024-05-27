using Metricos.Context;
using Metricos.Controllers;
using Metricos.IRepository;
using Metricos.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = Environment.GetEnvironmentVariable("ConexionSQL");

builder.Services.AddDbContext<SqlDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IPhases_CourseRepository, Phases_CourseRepository>();
builder.Services.AddScoped<IAsignarRepository, AsignarRepository>();

var app = builder.Build();

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
