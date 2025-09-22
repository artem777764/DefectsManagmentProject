using Backend.Models;
using Backend.Models.Context;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseNpgsql(
        Environment.GetEnvironmentVariable("APPLICATIONDATABASECONNECTION")!
    )
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

PrepareDb.Prepare(app);

app.Run();