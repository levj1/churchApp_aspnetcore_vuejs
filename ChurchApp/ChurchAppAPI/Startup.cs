using System;
using System.Text;
using AutoMapper;
using ChurchAppAPI.Entities;
using ChurchAppAPI.Extensions.Mapping;
using ChurchAppAPI.Extensions.Seed;
using ChurchAppAPI.Models;
using ChurchAppAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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
            // Get JWT Token settings from JwtSettings.json file
            JwtSettings settings = GetJwtSettings();

            // Register JWT as the authentication service
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(settings.Key)),
                    ValidateIssuer = true,
                    ValidIssuer = settings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = settings.Audience,

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(settings.MinutesToExpiration)
                };
            });


            services.AddSingleton<JwtSettings>(settings);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(
            options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            //services.AddDbContext<ChurchAppContext>(options => options.UseSqlServer("Server=.; database=church-app;Trusted_Connection=true;"));
            if (_env.IsProduction())
            {
                services.AddDbContext<ChurchAppContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SmarterAspConnection"),
                    b => b.MigrationsAssembly("ChurchAppAPI")));
            }
            else
            {
                services.AddDbContext<ChurchAppContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("ChurchAppAPI")));
            }


            services.AddDefaultIdentity<AppUser>()
                .AddEntityFrameworkStores<ChurchAppContext>();
            services.AddTransient<IPersonRepository, PersonRepository>();

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
        public void Configure(IApplicationBuilder app, ChurchAppContext churchAppContext)
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

            AddressTypeSeed.EnsureSeedDataForContext(churchAppContext);
            DonationTypeSeed.EnsureSeedDataForContext(churchAppContext);
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseMvc();
        }

        public JwtSettings GetJwtSettings()
        {
            JwtSettings settings = new JwtSettings()
            {
                Key = Configuration["JwtSettings:Key"],
                Audience = Configuration["JwtSettings:Audience"],
                Issuer = Configuration["JwtSettings:Issuer"],
                MinutesToExpiration = Convert.ToInt32(Configuration["JwtSettings:MinutesToExpiration"])
            };

            return settings;
        }
    }
}
