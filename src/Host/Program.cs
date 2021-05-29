using GenericHost.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;

namespace GenericHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            string environmentName = string.Empty;
            if (args.Length > 0)
                environmentName = args[0];

            Assembly assembly = Assembly.GetExecutingAssembly();
            string location = assembly.Location;
            Uri uri = new Uri(location);
            string path = Path.GetDirectoryName(uri.LocalPath);

            //string path = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath);

            IHostBuilder host = Host.CreateDefaultBuilder(args);
            host.ConfigureHostConfiguration(config =>
            {
                config.SetBasePath(path);
                config.AddCommandLine(args);

                if (string.IsNullOrEmpty(environmentName))
                    config.AddJsonFile("hostsettings.json", optional: true);
                else
                    config.AddJsonFile($"hostsettings.{environmentName}.json", optional: true);
            });
            host.ConfigureAppConfiguration((context, config) =>
            {
            });
            host.ConfigureServices((context, services) =>
            {
                services.AddGatewayServiceLocator();
                services.AddStockServiceLocator();
                services.AddHostedService<LifetimeEventsHostedService>();
            });

            return host;
        }
    }
}