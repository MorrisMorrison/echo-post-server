using EchoPost.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebUIServices();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddCors(options => options.AddPolicy(name: "echopost-angular-client", policy => policy.WithOrigins("https://echopost-angular-client.azurewebsites.net"/)));
var app = builder.Build();

// Configure the HTTP request pipeline.
// Initialise and seed database
using var scope = app.Services.CreateScope();
var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
await initialiser.InitialiseAsync();
await initialiser.SeedAsync();
// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
// app.UseHsts();

app.UseHealthChecks("/health");
// app.UseHttpsRedirection();
// app.UseStaticFiles();

app.UseSwaggerUi3(settings =>
{
    settings.Path = "/api";
    settings.DocumentPath = "/api/specification.json";
});
app.UseRouting();

app.UseCors("echopost-angular-client");

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapRazorPages();

// app.MapFallbackToFile("index.html");

app.Run();