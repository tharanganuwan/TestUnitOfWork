using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestUnitOfWorkCRUD.Core.ApplicationServices;
using TestUnitOfWorkCRUD.Core.ApplicationServices.Services;
using TestUnitOfWorkCRUD.Core.DomainServices;
using TestUnitOfWorkCRUD.Infrastructure;
using TestUnitOfWorkCRUD.Infrastructure.Repositories;

namespace TestUnitOfWorkCRUD.API
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestUnitOfWorkCRUD.API", Version = "v1" });
            });

            services.AddDbContext<DBContextCore>(con => con.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DBContextCore>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGlobalUnitOfWorkService, GlobalUnitOfWorkService>();
            services.AddScoped<IGlobalUnitOfWorkRepository, GlobalUnitOfWorkRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestUnitOfWorkCRUD.API v1"));
            }

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
