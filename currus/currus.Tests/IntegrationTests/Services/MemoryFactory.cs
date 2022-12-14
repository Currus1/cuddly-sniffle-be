using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using currus.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.Hosting;
using FluentAssertions.Common;
using Microsoft.AspNetCore.TestHost;
using Autofac.Extensions.DependencyInjection;
using System.Reflection.Metadata.Ecma335;
using currus.Models;
using currus.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace currus.Tests.Services
{
    class MemoryFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            IConfiguration configuration;

            var inMemorySettings = new Dictionary<string, string> {
            {"JwtConfig:Secret", "AAAAAAAAAAAAAAAAAAAA"},
            };

            configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            builder.ConfigureTestServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                            typeof(DbContextOptions<ApplicationDbContext>));
                if (descriptor != null) services.Remove(descriptor);

                var connectionString = "Data Source=localhost;Initial Catalog=CurrusDBTest; Integrated Security=True;MultipleActiveResultSets=True";
                services.AddDbContext<ApplicationDbContext>(options => { options.UseSqlServer(connectionString) ; });

                var serviceProvider = services.BuildServiceProvider();
                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<ApplicationDbContext>();
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                }

                services.AddScoped<ITripDbRepository, TripDbRepository>();
            });
        }
    }
}