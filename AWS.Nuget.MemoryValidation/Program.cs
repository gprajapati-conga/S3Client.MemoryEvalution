using System.Diagnostics.CodeAnalysis;

namespace AWS.Nuget.MemoryValidation
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostingContext, config) =>
           {
               config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                       .AddJsonFile("configoverride/appsettings.json", optional: true, reloadOnChange: true)
                       .AddEnvironmentVariables();
           })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
