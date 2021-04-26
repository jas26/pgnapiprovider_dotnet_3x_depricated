using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using DynamoDBLibraries.DynamoDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PGN_Db_Operations
{
    public class Startup
    {
        //public IConfigurationRoot Configuration { get; set; }
        public IConfiguration Configuration { get; set; }
        const string _configurationPath = "/Configuration";

        //public IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory() + _configurationPath)
                .AddJsonFile("appsettings.json")
                .Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());

            Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID", Configuration["AWS:AccessKey"]);
            Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", Configuration["AWS:SecretKey"]);
            Environment.SetEnvironmentVariable("AWS_REGION", Configuration["AWS:Region"]);
            services.AddAWSService<IAmazonDynamoDB>();
            services.AddSingleton<IPostData, PostData>();
            services.AddSingleton<IGetProfile, GetProfile>();
        }  

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMvc();

        }
    }
}
