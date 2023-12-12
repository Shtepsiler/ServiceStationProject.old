using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Data;
using Application;
using System.Reflection;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Application.DTOs.Respponces;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence.Services;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ITokenService, TokenService>();


var securityScheme = new OpenApiSecurityScheme()
{
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "JSON Web Token based security",
};

var securityReq = new OpenApiSecurityRequirement()
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] {}
    }
};
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo());
    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.",
    });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //  o.IncludeXmlComments(xmlPath);
    // Security
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddScoped<IServiceStationDContext, ServiceStationDContext>();
builder.Services.AddDbContext<ServiceStationDContext>(options =>
{
    string connectionString;
    var dbhost = Environment.GetEnvironmentVariable("DB_HOST");
    var dbname = Environment.GetEnvironmentVariable("DB_NAME");
    var dbpass = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");


    connectionString = $"Data Source={dbhost};User ID=sa;Password={dbpass};Initial Catalog={dbname};Encrypt=True;Trust Server Certificate=True;";

    // connectionString = builder.Configuration.GetConnectionString("MSSQLConnection");

    options.UseSqlServer(connectionString);

});

builder.Services.AddAuthentication();
builder.Services.AddIdentityCore<Manager>()
                   .AddRoles<IdentityRole<int>>()
                   .AddSignInManager<SignInManager<Manager>>()
                   .AddDefaultTokenProviders()
                   .AddEntityFrameworkStores<ServiceStationDContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new()
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(builder.Configuration["JwtSecurityKey"])),
                            ClockSkew = TimeSpan.FromHours(1),
                        };
                    });




builder.Services.AddMemoryCache(opt => new MemoryCacheEntryOptions()
{
    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
    Priority = CacheItemPriority.High,
    SlidingExpiration = TimeSpan.FromMinutes(2)
});

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
