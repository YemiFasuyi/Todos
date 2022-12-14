using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Todos.Persistance;

namespace Todos.API.Tests;

internal class TestApplicationFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var root = new InMemoryDatabaseRoot();

        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<TodoDbContext>));
            services.AddDbContext<TodoDbContext>(options =>
                options.UseInMemoryDatabase("TodoDbContext", root));
        });

        return base.CreateHost(builder);
    }
}