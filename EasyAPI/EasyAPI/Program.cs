using EasyAPI.EndPoints;
using EasyAPI.Models;
using EasyAPI.Repositories;
using EasyAPI.Services;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PostgresContext>(options =>
    options.UseNpgsql(builder.Configuration["Postgres"])
);
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();

WebApplication app = builder.Build();

app.MapGet("/", () => "EasyAPI");
app.MapUserEndPoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
