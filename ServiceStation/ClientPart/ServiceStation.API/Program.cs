using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServiceStation.API.MessageBroker;
using ServiceStation.API.MessageBroker.EventBus;
//using ServiceStation.API.Proto_Services;
using ServiceStation.BLL.Configurations;
using ServiceStation.BLL.DTO.Requests;
using ServiceStation.BLL.Factories;
using ServiceStation.BLL.Factories.Interfaces;
using ServiceStation.BLL.Mapping;
using ServiceStation.BLL.Services;
using ServiceStation.BLL.Services.Interfaces;
using ServiceStation.BLL.Validation;
using ServiceStation.DAL.Data;
using ServiceStation.DAL.Entities;
using ServiceStation.DAL.Repositories;
using ServiceStation.DAL.Repositories.Contracts;
using System.Reflection;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        builder.Services.Configure<MessageBrokerSettings>(
            builder.Configuration.GetSection("MessageBroker"));

        builder.Services.AddSingleton(sp =>
        sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

        builder.Services.AddMassTransit(busconf =>
        {
            busconf.SetKebabCaseEndpointNameFormatter();
            busconf.UsingRabbitMq((cont, conf) =>
            {
                MessageBrokerSettings settings = cont.GetRequiredService<MessageBrokerSettings>();

                conf.Host(new Uri(settings.Host), h =>
                {
                    h.Username(settings.Username);
                    h.Password(settings.Password);

                });
            });
        });

        builder.Services.AddScoped<ServiceStation.BLL.EventBus.IEventBus, ServiceStation.BLL.EventBus.EventBus>();
        builder.Services.AddScoped<IEventBus, EventBus>();



        builder.Services.AddEndpointsApiExplorer();
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



        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = Environment.GetEnvironmentVariable("REDIS");
            options.InstanceName = "CLientPart";
        });
        builder.Services.AddSwaggerGen(o =>
        {

            o.SwaggerDoc("v1", new OpenApiInfo() { Title = "Client Api" });
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
        builder.Services.AddIdentityCore<Client>()
                           .AddRoles<IdentityRole<int>>()
                           .AddSignInManager<SignInManager<Client>>()
                           .AddDefaultTokenProviders()
                           .AddEntityFrameworkStores<ServiceStationDContext>();

        builder.Services.AddTransient<JwtTokenConfiguration>();
        builder.Services.AddTransient<IJwtSecurityTokenFactory, JwtSecurityTokenFactory>();

        builder.Services.AddScoped<IJobRepository, JobRepository>();
        builder.Services.AddScoped<IModelRepository, ModelRepository>();
        builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
        builder.Services.AddScoped<ITokenRepository, TokenRepository>();
        builder.Services.AddScoped<IMechanicRepository, MechanicRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

        builder.Services.AddScoped<IJobService, JobService>();
        builder.Services.AddScoped<IClientService, ClientService>();
        builder.Services.AddScoped<IIdentityService, IdentityService>();
        builder.Services.AddScoped<IModelService, ModelService>();
        builder.Services.AddScoped<IManagerService, ManagerService>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IMechanicService, MechanicService>();
        builder.Services.AddScoped<IUnitOfBisnes, UnitOfBisnes>();

        builder.Services.AddTransient<EmailSender>();

        builder.Services.AddScoped<IValidator<ClientSignInRequest>, ClientSignInRequestValidator>();
        builder.Services.AddScoped<IValidator<ClientSignUpRequest>, ClientSingUpRequestValidator>();

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



        builder.Services.AddGrpc()/*.AddJsonTranscoding()*/;

        var app = builder.Build();
        /*var context = app.Services.GetService<ServiceStationDContext>();
        context.Database.EnsureCreated();*/
        // Configure the HTTP request pipeline.
        /*if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }*/


        //app.MapGrpcService<ClientSerrvice>();

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}