
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<TestTaskDbContext>(options =>
{
    options.UseInMemoryDatabase("InMemoryDb");
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1",
    new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Test Task",
        Description = "AtmosClear test Task ",
        Version = "v1"
    });
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
