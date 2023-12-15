using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TaskManagerForMechanic.DAL;
using TaskManagerForMechanic.WEB.GraphQl;
using TaskManagerForMechanic.WEB.GraphQl.DataLoader;
using TaskManagerForMechanic.WEB.GraphQl.Types;
using TaskManagerForMechanic.WEB.MessageBroker;
using TaskManagerForMechanic.WEB.MessageBroker.Concumers;
using TaskManagerForMechanic.WEB.MessageBroker.EventBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddPooledDbContextFactory<TaskManagerDbContext>(options =>
{
    var dbhost = Environment.GetEnvironmentVariable("DB_HOST");
    var dbname = Environment.GetEnvironmentVariable("DB_NAME");
    var dbpass = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");


    string connectionString = $"Data Source={dbhost};User ID=sa;Password={dbpass};Initial Catalog={dbname};Encrypt=True;Trust Server Certificate=True;";
    //  string connectionString = builder.Configuration.GetConnectionString("MSSQLConnection");
    options.UseSqlServer(connectionString);
   

});
builder.Services.AddDbContext<TaskManagerDbContext>(options =>
{
    var dbhost = Environment.GetEnvironmentVariable("DB_HOST");
    var dbname = Environment.GetEnvironmentVariable("DB_NAME");
    var dbpass = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");


    string connectionString = $"Data Source={dbhost};User ID=sa;Password={dbpass};Initial Catalog={dbname};Encrypt=True;Trust Server Certificate=True;";
    //  string connectionString = builder.Configuration.GetConnectionString("MSSQLConnection");
    options.UseSqlServer(connectionString);


});


builder.Services.Configure<MessageBrokerSettings>(
          builder.Configuration.GetSection("MessageBroker"));

builder.Services.AddSingleton(sp =>
sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

builder.Services.AddTransient<IEventBus, EventBus>();
builder.Services.AddMassTransit(busconf =>
{
    busconf.SetKebabCaseEndpointNameFormatter();
    busconf.AddConsumer<JobConsumer>();
    busconf.AddConsumer<MechanicTaskConsumer>();




    busconf.UsingRabbitMq((cont, conf) =>
    {
        MessageBrokerSettings settings = cont.GetRequiredService<MessageBrokerSettings>();

        conf.Host(new Uri(settings.Host), h =>
        {
            h.Username(settings.Username);
            h.Password(settings.Password);

        });

        conf.ReceiveEndpoint(nameof(GeneralBusMessages.Message.Job), e =>
        {
            e.ConfigureConsumer(cont, typeof(JobConsumer));
 
        }); 
        conf.ReceiveEndpoint(nameof(GeneralBusMessages.Message.MechanicsTasks), e =>
        {
            e.ConfigureConsumer(cont, typeof(MechanicTaskConsumer));
        });
    });


});



builder.Services.AddGraphQLServer()
     .AddType<JobType>()
     .AddType<MechanicType>()
     .AddType<MechanicTaskType>()
     .AddDataLoader<JobByIdDataLoader>()
     .AddDataLoader<MechanicByIdDataloader>()
     .AddDataLoader<MechanicTaskByIdDataLoader>()




    .AddQueryType<Query>()
    .AddMutationType<Mutations>()
    .AddSubscriptionType<Subscriptions>()
    
    
    .AddInMemorySubscriptions()
    
    
    .AddSorting()
    
    ;




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseWebSockets();

app.UseRouting().UseEndpoints(endpoints =>endpoints.MapGraphQL());

app.UseAuthorization();

app.MapRazorPages();

app.Run();
