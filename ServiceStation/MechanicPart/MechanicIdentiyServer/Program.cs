var builder = WebApplication.CreateBuilder(args);



builder.Services.AddIdentityServer();

builder.Services.AddIdentityServer()
 .AddInMemoryClients(new List<Client>())
 .AddInMemoryIdentityResources(new List<IdentityResource>())
 .AddInMemoryApiResources(new List<ApiResource>())
 .AddInMemoryApiScopes(new List<ApiScope>())
 .AddTestUsers(new List<TestUser>())
 .AddDeveloperSigningCredential();


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
