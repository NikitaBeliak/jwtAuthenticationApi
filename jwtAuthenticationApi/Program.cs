using Domain.dbContext;
using Domain.Entitys.Interfaces;
using Infrastracture.Helpers;
using Infrastracture.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddDbContext<Context>(o => o.UseMySql(builder.Configuration.GetConnectionString("BloggingDatabase"),
    new MySqlServerVersion(new System.Version(8, 0, 22))));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddControllers();
builder.Services.AddMvc();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Auth",
        Description = "ASP.NET Core Web API"
    });

    OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });

});

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();
app.UseCors("MyPolicy");
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
