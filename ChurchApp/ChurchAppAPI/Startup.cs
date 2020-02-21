using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChurchAppAPI.Entities;
using ChurchAppAPI.Extensions.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ChurchAppAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        private IHostingEnvironment _env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ChurchAppContext>(options => options.UseSqlServer("Server=.; database=church-app;Trusted_Connection=true;"));
            //if (_env.IsProduction())
            //{
            //    services.AddDbContext<ChurchAppContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ServerConnection"),
            //        b => b.MigrationsAssembly("ChurchAppAPI")));
            //}
            //else
            //{
            //    services.AddDbContext<ChurchAppContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
            //        b => b.MigrationsAssembly("ChurchAppAPI")));
            //}


            services.AddDefaultIdentity<AppUser>()
                .AddEntityFrameworkStores<ChurchAppContext>();

            // Auto Mapper configuration
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
