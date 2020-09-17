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
using Persistence.Repositories.Interfaces;
using Persistence.Repositories.Implementations;
using Persistence;

namespace Presentation
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
            services.AddSwaggerGen();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<IContextFactory>(factory => new ContextFactory(factory.GetService<IConfiguration>().GetSection("EndoscopesTrackingDatabase").GetSection("ConnectionString").Value));
            services.AddTransient<ICustomerRepository>(repository => new CustomerRepository(repository.GetService<IContextFactory>()));
            services.AddTransient<IMachineRepository>(repository => new MachineRepository(repository.GetService<IContextFactory>()));
            services.AddTransient<IProcessRepository>(repository => new ProcessRepository(repository.GetService<IContextFactory>()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("v1/swagger.json", "EndoscopesTrackingAPI");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(o => o.AllowAnyOrigin());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
