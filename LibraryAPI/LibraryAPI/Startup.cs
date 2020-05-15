using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Core.ApplicationServices;
using LibraryApp.Core.ApplicationServices.Services;
using LibraryApp.Core.DomainServices;
using LibraryApp.Core.Entity.Entities;
using LibraryApp.InfaStructure.Data;
using LibraryApp.InfaStructure.Data.SQLRepositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LibraryAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            if (Environment.IsDevelopment())
            {
                // In-memory database:
                services.AddDbContext<LibraryAppContext>(opt => opt.UseInMemoryDatabase("Library"));
            }
            else
            {
                // Azure SQL database:
                services.AddDbContext<LibraryAppContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            }

            services.AddScoped<IRepositories<Users>, UserRepositories>();
            services.AddScoped<IService<Users>, UserService>();

            services.AddScoped<IRepositories<Books>, BooksRepositories>();
            services.AddScoped<IService<Books>, BookService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope())
                {

                    var ctx = scope.ServiceProvider.GetService<LibraryAppContext>();
                    ctx.Database.EnsureCreated();
                    DBInitializer.SeedDb(ctx);
                }
            }
            else
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {

                    var services = scope.ServiceProvider;
                    var dbContext = services.GetService<LibraryAppContext>();
                    dbContext.Database.EnsureCreated();
                }
                app.UseHsts();
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
