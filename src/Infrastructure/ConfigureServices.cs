﻿using System.Data.Common;
using EchoPost.Application.Common.Interfaces;
using EchoPost.Application.Posts.Services;
using EchoPost.Infrastructure.Files;
using EchoPost.Infrastructure.Identity;
using EchoPost.Infrastructure.Persistence;
using EchoPost.Infrastructure.Persistence.Interceptors;
using EchoPost.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("EchoPostDb"));
        }
        else
        {
            string connectionString = configuration.GetConnectionString("AZURE_MYSQL_CONNECTIONSTRING");
            // MYSQL
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString,
                ServerVersion.AutoDetect(connectionString),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
            );
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        Console.WriteLine("Infrastructure => configure webservices:");
        Console.WriteLine(configuration.GetValue<string>("IdentityServer:IssuerUri"));
        services.AddIdentityServer(options => options.IssuerUri = configuration.GetValue<string>("IdentityServer:IssuerUri"))
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
        services.AddTransient<ITwitterApiService, TwitterApiService>();
        services.AddTransient<IPostingService, PostingService>();

        services.AddAuthentication()
            .AddIdentityServerJwt()

;
        services.AddAuthorization(options =>
            options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

        return services;
    }
}
