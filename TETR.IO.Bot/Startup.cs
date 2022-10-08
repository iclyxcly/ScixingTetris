using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TETR.IO.Bot
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins("https://tetr.io").WithMethods("POST", "OPTION").WithHeaders("Content-Type");
                });
            });
            services.AddRazorPages();
            services.AddCarter();
            // options => {
            //     options.OpenApi.ServerUrls = new[] { "http://127.0.0.1:47327" };
            // }
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors();
            app.UseEndpoints(builder => builder.MapCarter());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
    public class HomeModule : CarterModule
    {
        public HomeModule()
        {
            Get("/", async (req, res) => await res.WriteAsync("Hello from Carter!"));
            Get("/Tetr", async (req, res) => await res.WriteAsync("Hello from Tetr!"));
        }
    }
}
