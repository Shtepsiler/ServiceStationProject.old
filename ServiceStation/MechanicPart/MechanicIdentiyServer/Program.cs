using IdentityServer4.Models;
using IdentityServer4.Test;
using MechanicIdentiyServer.Config;
using MechanicIdentiyServer.Data;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddIdentityServer()
 .AddInMemoryClients(Config.Clients)
 //.AddInMemoryIdentityResources(Config.IdentityResources)
 //.AddInMemoryApiResources(Config.ApiResources)
 .AddInMemoryApiScopes(Config.ApiScopes)
// .AddTestUsers(Config.TestUsers)
 .AddDeveloperSigningCredential();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapDefaultControllerRoute();
    });
});
app.Run();
