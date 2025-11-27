using RDS.Server.Common.Server;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddServiceContainer();
builder.AddDataContexts();
builder.AddSecurity();
//builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddRepositories();
builder.AddServices();

var app = builder.Build();
app.LoadConfiguration();
app.ConfigureEnvironment();
app.ConfigureComponents();

if (app.Environment.IsDevelopment())
{
    await app.SeedDatabaseAsync();
}

app.Run();