using System.Security.Claims;
using System.Text;
using System.Text.Json;
using backend.Services;
using Backend.Models;
using Backend.Models.Context;
using Backend.Repositories;
using Backend.Services;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

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
        Environment.GetEnvironmentVariable("ApplicationDatabaseConnection")!
    )
);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = false;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Policy.Engineer.ToPolicyName(), policy =>
        policy.RequireClaim(ClaimTypes.Role, ((int)Role.Engineer).ToString()));

    options.AddPolicy(Policy.Manager.ToPolicyName(), policy =>
        policy.RequireClaim(ClaimTypes.Role, ((int)Role.Manager).ToString()));

    options.AddPolicy(Policy.Observer.ToPolicyName(), policy =>
        policy.RequireClaim(ClaimTypes.Role, ((int)Role.Observer).ToString()));

    options.AddPolicy(Policy.Admin.ToPolicyName(), policy =>
        policy.RequireClaim(ClaimTypes.Role, ((int)Role.Admin).ToString()));
});

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetValue<string>("JwtSettings:SecretKey")!
            )),

            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["JwtSettings:Audience"],

            ValidateLifetime = true,
        };

        string cookieName = builder.Configuration["JwtSettings:JwtCookieName"]!;
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies[cookieName];
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<JwtCookieService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDefectRepository, DefectRepository>();
builder.Services.AddScoped<IHistoryRepository, HistoryRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IPriorityRepository, PriorityRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDefectService, DefectService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IPriorityService, PriorityService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // http://localhost:5234/swagger/index.html
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Defect Managment Project API");
    });
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

PrepareDb.Prepare(app);

app.Run();