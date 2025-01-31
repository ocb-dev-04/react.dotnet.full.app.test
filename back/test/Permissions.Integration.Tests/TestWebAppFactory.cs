using Persistence.Context;
using Testcontainers.MsSql;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Permissions.Integration.Tests;

public class TestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:latest")
        .WithPassword("qwqerty1234!!")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            ServiceDescriptor? descriptor = services.SingleOrDefault(s 
                => s.ServiceType.Equals(typeof(DbContextOptions<AppDbContext>)));

            if(descriptor is not null)
                services.Remove(descriptor);

            services.AddDbContext<AppDbContext>(options 
                => options.UseSqlServer(_dbContainer.GetConnectionString()));

        });
        base.ConfigureWebHost(builder);
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }
}
