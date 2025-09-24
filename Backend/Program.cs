using backend.Services;
using Backend.Models;
using Backend.Models.Context;
using Backend.Repositories;
using Backend.Services;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Defect Managment Project API",
        Version = "v1"
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseNpgsql(
        Environment.GetEnvironmentVariable("APPLICATIONDATABASECONNECTION")!
    )
);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = false;
    });

builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.AddScoped<IValidationService, ValidationService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // http://localhost:5234/swagger/index.html
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Defect Managment Project API");
    });
}

app.UseRouting();
app.MapControllers();
app.UseHttpsRedirection();

PrepareDb.Prepare(app);

app.Run();