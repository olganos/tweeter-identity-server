using Duende.IdentityServer;
using IdentityServer.Data;
using IdentityServer.Models;
using IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdentityServer;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();

        builder.Services.AddScoped<IEmailSender, DummyEmailSender>();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                ?? builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        builder.Services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.GetClients(Environment.GetEnvironmentVariable("CLIENT_REACT_BFF_BASEURL")
                ?? builder.Configuration.GetValue<string>("Clients:ClientReactBffBaseurl")))
            .AddAspNetIdentity<ApplicationUser>()
            .AddProfileService<CustomProfileService>();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();

        app.MapRazorPages()
            .RequireAuthorization();

        return app;
    }
}