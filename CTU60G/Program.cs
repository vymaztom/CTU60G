using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Configuration;

namespace CTU60G
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) CreateWindowsHostBuilder(args).Build().Run();
                else if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) CreateLinuxHostBuilder(args).Build().Run();
                    else throw new SystemException("Unsupported system.");
        }

        public static IHostBuilder CreateWindowsHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {

                    IConfiguration configuration = hostContext.Configuration;
                    WorkerOptions options = configuration.GetSection("Config").Get<WorkerOptions>();
                    services.AddSingleton(options);
                    services.AddHostedService<Worker>();

                }).ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    configApp.AddJsonFile(
                       $"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                       optional: true);
                    configApp.AddEnvironmentVariables(prefix: "PREFIX_");
                    configApp.AddCommandLine(args);
                });

        
        public static IHostBuilder CreateLinuxHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSystemd()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
    }
}
