using EchoPost.Infrastructure.Persistence;
using Microsoft.AspNetCore.CookiePolicy;

Console.WriteLine("--- STARTING ECHOPOST WITH ---");
Console.WriteLine(args);
Console.WriteLine("--- BUILD APPLICATION ---");
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
Console.WriteLine("--- SETUP APPLICATION SERVICES ---");
builder.Services.AddApplicationServices();
Console.WriteLine("--- SETUP INFRASTRUCTURE SERVICES ---");
builder.Services.AddInfrastructureServices(builder.Configuration);
Console.WriteLine("--- SETUP WEBUI SERVICES ---");
builder.Services.AddWebUIServices();
Console.WriteLine("--- SET ENVIRONMENT VARIABLES ---");
builder.Configuration.AddEnvironmentVariables();
Console.WriteLine();
Console.WriteLine("--- WITH CONFIGURATION ---");

string connectionString = builder.Configuration.GetConnectionString("AZURE_MYSQL_CONNECTIONSTRING");
string env = builder.Configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT");
string urls = builder.Configuration.GetValue<string>("ASPNETCORE_URLS");

Console.WriteLine("AZURE_MYSQL_CONNECTIONSTRING:" + connectionString);
Console.WriteLine("ASPNETCORE_ENVIRONMENT:" + env);
Console.WriteLine("ASPNETCORE_URLS:" + urls);
Console.WriteLine();


builder.Services.AddCors(options => options.AddPolicy(name: "echopost-angular-client", policy => policy.WithOrigins("https://echopost-angular-client.azurewebsites.net", "http://localhost:44447")
.AllowAnyHeader()
.AllowAnyMethod()));
var app = builder.Build();

using var scope = app.Services.CreateScope();
var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
await initialiser.InitialiseAsync();
await initialiser.SeedAsync();

app.UseHealthChecks("/health");

app.UseSwaggerUi3(settings =>
{
    settings.Path = "/api";
    settings.DocumentPath = "/api/specification.json";
});
app.UseRouting();

app.UseCors("echopost-angular-client");
app.UseCookiePolicy(new CookiePolicyOptions
{
    HttpOnly = HttpOnlyPolicy.Always,
    MinimumSameSitePolicy = SameSiteMode.None,
    Secure = CookieSecurePolicy.Always
});

Console.WriteLine("--- SETUP IDENTITY ---");
app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapRazorPages();
app.UseStaticFiles();
Console.WriteLine("--- FINISHED ---");

app.Run();