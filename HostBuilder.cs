using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AuthConsole;

public static class HostBuilder
{
    public static IHost BuildHost(string[] args)
    {
        if (args.Length > 0 && args[0].StartsWith("cs=") == false)
        {
            throw new ArgumentException("Connectionstring Argument: cs=[string]");
        }

        var builder = Host.CreateDefaultBuilder(args);
        string? connectionString = null;
        if (args.Length == 0)
        {
            Console.WriteLine("Database Path:");
            connectionString = Console.ReadLine();
            if (connectionString is null)
                throw new ArgumentNullException(nameof(connectionString), "Connectionstring ist leer!");
        }


        builder.ConfigureServices((host, services) =>
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite($"Data Source={connectionString ?? host.Configuration["cs"]}"))
                .AddSingleton<UserService>()
                .AddDefaultIdentity<IdentityUser>(options => { options.User.RequireUniqueEmail = true; })
                .AddEntityFrameworkStores<ApplicationDbContext>()
        );
        return builder.Build();
    }
}