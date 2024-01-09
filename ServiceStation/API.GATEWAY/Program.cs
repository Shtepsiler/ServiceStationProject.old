using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var routes = "./Routes";
builder.Configuration.AddOcelotWithSwaggerSupport(p => p.Folder = routes);

//builder.Configuration.AddJsonFile("configuration.json", optional: false, reloadOnChange: true);
builder.Services.AddSwaggerForOcelot(builder.Configuration);
/*builder.Services.AddCors();
*/
builder.Services.AddOcelot();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(e =>
    {
      /*  e.SwaggerEndpoint("/swagger/v1/swagger.json", "gateway");
        e.SwaggerEndpoint("/client/swagger/v1/swagger.json", "client");
        e.SwaggerEndpoint("/manager/swagger/v1/swagger.json", "manager");*/
            
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";
});
await app.UseOcelot();

app.Run();
