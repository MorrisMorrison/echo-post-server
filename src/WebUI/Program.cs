using EchoPost.Infrastructure.Persistence;
using Microsoft.AspNetCore.CookiePolicy;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebUIServices();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddCors(options => options.AddPolicy(name: "echopost-angular-client", policy => policy.WithOrigins("https://echopost-angular-client.azurewebsites.net", "http://localhost:44447").AllowAnyHeader()));
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
app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapRazorPages();
app.UseStaticFiles();

app.Run();