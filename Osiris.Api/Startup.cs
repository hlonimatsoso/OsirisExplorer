using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Osiris.Api.DogApi;
using Osiris.DogApi;
using Osiris.Interfaces;

namespace Osiris.Api
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
            services.AddControllers();
            services.AddHttpClient("DogsAPI", client =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("DogApi:baseUrl").Value);
                client.DefaultRequestHeaders.Add("x-api-key", Configuration.GetSection("DogApi:key").Value);
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: "BlazorPolicy",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:63315", "https://localhost:44336", "https://localhost:5001","http://localhost:5000")
                                .WithMethods("GET");
                    });
            });

            services.AddMemoryCache();

            services.Configure<DogApiSettings>(options => Configuration.GetSection("DogApi").Bind(options));

            services.AddTransient<IDogService, DogService>();

            services.AddTransient<ICacheIsKing, OsirisCache>();

            services.AddSwaggerGen();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Osiris API Explorer");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors("BlazorPolicy");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
