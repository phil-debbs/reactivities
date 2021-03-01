using Application.Activities;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Persistence;

namespace API.Extensions
{
    /*
    Extends the ConfigureServices method inside the Startup class so that additional codes can be kept here to keep the startup class from being messy

    */
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config )
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });

            //inject dbcontext
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            //added to allow CORS access from external urls. 
            //Add the "Cors" services
            services.AddCors(opt => {
                opt.AddPolicy("CorsPolicy", policy =>{
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
                });
            }) ;     

            //add Mediator as a service    
            services.AddMediatR(typeof(List.Handler).Assembly);

            //add AutoMapper
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            return services;
        }
    }
}