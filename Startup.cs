using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Ex3V2.Middlewares;
using Ex3V2.Models;
using static System.Net.WebRequestMethods;
using Ex3V2.Services;

namespace Ex3V2
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
            services.AddTransient<IDbService, DbService>();
            services.AddControllers();

               services.AddSwaggerGen(conf =>
            {
                conf.SwaggerDoc("v1",   new OpenApiInfo {Title = "API", Version = "v1"});
            });
        }
        }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app,
                          IWebHostEnvironment env,
                          IDbService dbService)
        {

            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

             app.UseSwagger();
            app.UseSwaggerUI(conf =>
            {
                conf.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
            });

            app.UseMiddleware<LoggingMiddleware>();

            app.Use(async (context, next) =>
            {
                if (!context.Request.Headers.ContainsKey("Index"))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Nie podano indeksu w nagłówku");
                    return;
                }
            
                string index = context.Request.Headers["Index"].ToString();
                
                if (!dbService.CheckIndexNumber(index))
                {
                    //Wydaje mi sie że 404 lepiej tu pasuje niz 401 (mogę być w błędzie)
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsync("Student o podanym indeksie nie istnieje");
                    return;
                }
                
                await next();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
}
