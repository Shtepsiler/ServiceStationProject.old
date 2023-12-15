using Ocelot.DependencyInjection;
using Ocelot.Cache.CacheManager;
using Ocelot.Middleware;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.Provider.Polly;
using API.GATEWAY.Config;

var builder = WebApplication.CreateBuilder(args);
var routes = "./Routes";
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("./Routes/ocelot.SwaggerEndPoints.json", optional: false, reloadOnChange: true);

// Add services to the container.
builder.Configuration.AddOcelotWithSwaggerSupport(p=>p.Folder = routes);

/*builder.Services.AddOcelot(builder.Configuration)
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();
    }).AddPolly();*/
builder.Services.AddOcelot(builder.Configuration).AddPolly();

builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddOcelot(routes, builder.Environment)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwaggerForOcelotUI(options =>
{
    options.PathToSwaggerGenerator = "/swagger/docs";
    options.ReConfigureUpstreamSwaggerJson = AlterUpstream.AlterUpstreamSwaggerJson;

}).UseOcelot().Wait();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
