using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PGN_Db_Operations
{
    public class Program
    {
        //IWebHostBuilder _webHost = null;
        const string _configurationPath = "/Configuration";
        const string port = "Port";

        public static void Main(string[] args)
        {

            var webHost = CreateHostBuilder(args);
            webHost.Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args)
        {
            var configurationDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+_configurationPath;
            IConfiguration configuration = null;
            return WebHost.CreateDefaultBuilder()
                .ConfigureAppConfiguration((webHostBuilderContext,
                configurationBuilder) =>
                {
                    configurationBuilder.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + _configurationPath);
                    configurationBuilder.AddJsonFile("appsettings.json", reloadOnChange: true,optional: true);
                    configuration = configurationBuilder.Build();
                })
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Any, int.Parse(configuration["Port"]), listenOptions =>
                     {
                     });


                })
                .UseStartup<Startup>();
        }
    }
}
