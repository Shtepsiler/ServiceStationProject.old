using Application;
using Application.EventBusConsumers;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Data;
using Infrastructure.Persistence.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using WebApplication.MessageBroker;
using WebApplication.MessageBroker.EventBus;
var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ITokenService, TokenService>();


builder.Services.Configure<MessageBrokerSettings>(
          builder.Configuration.GetSection("MessageBroker"));

builder.Services.AddSingleton(sp =>
sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

builder.Services.AddTransient<IEventBus, EventBus>();
builder.Services.AddMassTransit(busconf =>
{
    busconf.SetKebabCaseEndpointNameFormatter();
    busconf.AddConsumer<ModelConsumer>();
    busconf.UsingRabbitMq((cont, conf) =>
    {
        MessageBrokerSettings settings = cont.GetRequiredService<MessageBrokerSettings>();

        conf.Host(new Uri(settings.Host), h =>
        {
            h.Username(settings.Username);
            h.Password(settings.Password);

        });

        conf.ReceiveEndpoint(nameof(GeneralBusMessages.Message.Model), e =>
            {
                e.ConfigureConsumer(cont, typeof(ModelConsumer));
            });
    });


});


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
    o.SwaggerDoc("v1", new OpenApiInfo() { Title = "Manager API" });
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
    AbsoluteExpiration = DateTime.Now.AddSeconds(30),
    Priority = CacheItemPriority.High,
    SlidingExpiration = TimeSpan.FromSeconds(20)
});

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
