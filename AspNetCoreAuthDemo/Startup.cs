using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AspNetCoreAuthDemo.Data;
using AspNetCoreAuthDemo.Models;
using AspNetCoreAuthDemo.Services;
using AspNetCoreAuthDemo.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AspNetCoreAuthDemo.Middleware;

namespace AspNetCoreAuthDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            });

            services.AddAuthentication().AddTwitter(twitterOptions =>
            {
                twitterOptions.ConsumerKey = Configuration["Authentication:Twitter:ConsumerKey"];
                twitterOptions.ConsumerSecret = Configuration["Authentication:Twitter:ConsumerSecret"];
            });

            services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ApplicationId"];
                microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:Password"];
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddSingleton<IRequestIdFactory, RequestIdFactory>();
            services.AddScoped<IRequestId, RequestId>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory log)
        {
            log.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            //app.UseDirectoryBrowser();

            //app.UseFileServer(enableDirectoryBrowsing: env.IsDevelopment()); //Combines UseDefaultFiles(), UseStaticFiles(), UseDirectoryBrowser()

            app.UseAuthentication();

            app.UseMiddleware<RequestIdMiddleware>();

            app.UseEnvironmentDisplay();

            //app.Run(context =>
            //{
            //    context.Response.StatusCode = 400;
            //    return Task.FromResult(0);
            //});

            //app.UseStatusCodePages(subApp =>
            //{
            //    subApp.Run(async context =>
            //    {
            //        context.Response.ContentType = "text/html";
            //        await context.Response.WriteAsync("<strong> App Error </strong>");
            //        await context.Response.WriteAsync(new string(' ', 512)); //Padding for IE
            //    });
            //});

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
