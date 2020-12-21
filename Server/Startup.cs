using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using JINIApp.Server.Data;
using Microsoft.OpenApi.Models;
using System;

namespace JINIApp.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {            
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            string database = Configuration.GetSection("DataBase").Value;
            string connStr = "";
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddControllers().AddNewtonsoftJson();
            services.AddDbContext<JINIAppServerContext>(options =>
            {
                if (env == "Development")
                {
                    
                    if (database == "PostgreSQL")
                    {
                        connStr = Configuration.GetConnectionString("JINIAppPostgreSQLContext");
                        options.UseNpgsql(connStr);
                    }
                    else
                    {
                        connStr = Configuration.GetConnectionString("JINIAppLocalDBContext");
                        options.UseSqlServer(connStr);
                    }

                }
                else
                {
                    var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
                    // Parse connection URL to connection string for Npgsql
                    connUrl = connUrl.Replace("postgres://", string.Empty);
                    var pgUserPass = connUrl.Split("@")[0];
                    var pgHostPortDb = connUrl.Split("@")[1];
                    var pgHostPort = pgHostPortDb.Split("/")[0];
                    var pgDb = pgHostPortDb.Split("/")[1];
                    var pgUser = pgUserPass.Split(":")[0];
                    var pgPass = pgUserPass.Split(":")[1];
                    var pgHost = pgHostPort.Split(":")[0];
                    var pgPort = pgHostPort.Split(":")[1];
                    connStr = $"Host={pgHost};Port={pgPort};Username={pgUser};Password={pgPass};Database={pgDb};Pooling=true;SSL Mode=Require;TrustServerCertificate=True;;SearchPath=jiniapp";
                    options.UseNpgsql(connStr);
                }
            });           

        }        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
