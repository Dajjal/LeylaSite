using Ardalis.Specification;
using AutoMapper;
using Core.Application.Generics;
using Infrastructure.Contexts;
using Infrastructure.Generics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.ServiceExt;

public static class SiteServicesExtension
{
    public static IServiceCollection RegisterSiteServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region Контекст Базы Данных

        services.AddDbContext<SiteDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("SiteConnection"),
                builder => builder.MigrationsAssembly(typeof(SiteDbContext).Assembly.FullName)));

        /*services.AddDbContext<SiteDbContext>(options =>
                options
                    .UseNpgsql(configuration.GetConnectionString("SiteConnection"),
                        builder => { builder.MigrationsAssembly(typeof(SiteDbContext).Assembly.FullName); })
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking),
            ServiceLifetime.Transient);*/

        #endregion
        
        

        /*services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                //var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfig:"));
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(new byte[] { }),
                    ValidateIssuer = false, // for dev
                    ValidateAudience = false, // for dev
                    RequireExpirationTime = false, // for dev
                    ValidateLifetime = true
                };
            });
            

        services
            .AddIdentityApiEndpoints<IdentityUser>()
            .AddEntityFrameworkStores<SiteDbContext>();*/
        
            

        #region Внедрение зависимостей

        // Универсальный репозитори
        services.AddScoped(typeof(IRepositoryBase<>), typeof(GenericRepositoryImpl<>));
        // Универсальный сервис
        services.AddScoped(typeof(IGenericService<>), typeof(GenericServiceImpl<>));

        #endregion

        // Автомаппер
        services.AddScoped(typeof(IMapperBase), typeof(Mapper));
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}