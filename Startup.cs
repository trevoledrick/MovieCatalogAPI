using System;
using System.IO;
using System.Reflection;
using MovieholicAPI.Models;
using MovieholicAPI.Services.CharacterServices;
using MovieholicAPI.Services.FranchiseServices;
using MovieholicAPI.Services.MovieServices;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace MovieholicAPI
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
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<MoviesContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("MoviesContextConnectionString")));

            services.AddScoped(typeof(IFranchiseService), typeof(FranchiseService));
            services.AddScoped(typeof(IMovieService), typeof(MovieService));
            services.AddScoped(typeof(ICharacterService), typeof(CharacterService));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Movieholic API",
                    Version = "v1",
                    Description = "A web api that hosts data of Franchises, Movies & Movie Characters",
                    Contact = new OpenApiContact
                    {
                        Name = "Trevo Ledrick",
                        Url = new Uri("https://www.linkedin.com/in/trevo-ledrick-7348a0185")
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieholicAPI v1"));
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