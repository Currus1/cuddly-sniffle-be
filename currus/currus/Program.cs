using Autofac.Extensions.DependencyInjection;
using currus;

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

public partial class Program { }