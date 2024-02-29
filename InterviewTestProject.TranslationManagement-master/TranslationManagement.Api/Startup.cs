using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

using TranslationManagement.Api.Repositories;
using TranslationManagement.Api.Services.Interfaces;
using TranslationManagement.Api.Services.Implementation;
using TranslationManagement.Api.Models;
using System.Linq;
using System.Reflection;
using TranslationManagement.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using System;

namespace TranslationManagement.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            

            string connection = Configuration.GetConnectionString("Default");
            services.AddDbContext<AppDbContext>(options =>
                options.UseLazyLoadingProxies().UseSqlite("Data Source=TranslationAppDatabase.db"));

            var _entitiesModelsAssemblies =
                new Assembly[]
                {
                    Assembly.Load("TranslationManagement.Api")
                };

            var entitiesTypes = _entitiesModelsAssemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(IEntityDb).IsAssignableFrom(x) && x != typeof(IEntityDb));

            foreach (var entityType in entitiesTypes)
            {
                {
                    services.AddScoped(typeof(IBaseRepository<>).MakeGenericType(entityType), typeof(BaseRepository<>).MakeGenericType(entityType));
                }
            }


            services.AddScoped<ITranslatorService, TranslatorService>();
            services.AddScoped<IBaseService<Translator>, TranslatorService>();

            services.AddScoped<ITranslationJobService, TranslationJobService>();
            services.AddScoped<IBaseService<TranslationJob>, TranslationJobService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TranslationManagement.Api", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TranslationManagement.Api v1"));

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
