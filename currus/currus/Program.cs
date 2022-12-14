using Autofac.Extensions.DependencyInjection;
using currus;
using System.Diagnostics.CodeAnalysis;

var host = Host.CreateDefaultBuilder(args)
.UseServiceProviderFactory(new AutofacServiceProviderFactory())
.ConfigureWebHostDefaults(webHostBuilder =>
{
    webHostBuilder
    .UseContentRoot(Directory.GetCurrentDirectory())
    .UseIISIntegration()
    .UseStartup<Startup>();
})
.Build();

host.Run();

[ExcludeFromCodeCoverage]
public partial class Program { }