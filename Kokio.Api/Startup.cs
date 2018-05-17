using System.Collections.Generic;
using Kokio.Api.App;
using Kokio.Api.App.Projects;
using Kokio.Api.App.ProjectReleases;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Kokio.Api.App.TestCases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace kokio_server
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
            });

            services
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    var settings = options.SerializerSettings;
                    settings.Converters = new List<JsonConverter> { new ObjectIdConverter() };
                });

            // Auth0 Configuration
            string domain = $"https://{Configuration["Auth0:Domain"]}/";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = Configuration["Auth0:ApiIdentifier"];
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:projects", policy => 
                    policy.Requirements.Add(new HasScopeRequirement("read:projects", domain)));
                options.AddPolicy("modify:projects", policy => 
                    policy.Requirements.Add(new HasScopeRequirement("modify:projects", domain)));
                options.AddPolicy("delete:projects", policy => 
                    policy.Requirements.Add(new HasScopeRequirement("delete:projects", domain)));
            });

            // register the scope authorization handler
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            services.AddSingleton(Configuration);

            // Supply access to the Settings.cs file for application settings
            services.Configure<Settings>(options =>
            {
                //options.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                options.ConnectionString = Configuration["ConnectionStrings:Mongo:ConnectionString"];
                options.DefaultDatabase = Configuration["MongoConnection:Database"];
            });

            // Dependency Injection Registrations
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IProjectReleaseRepository, ProjectReleaseRepository>();
            services.AddTransient<ITestCaseRepository, TestCaseRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //TODO: app.UseExceptionHandler("...rel path to error page...");
            }

            app.UseStaticFiles();

            // Auth0 Configuration
            app.UseAuthentication();

            app.UseCors("AllowAllHeaders");

            app.UseMvc();
        }
    }
}
