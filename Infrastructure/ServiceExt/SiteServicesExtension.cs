using Ardalis.Specification;
using AutoMapper;
using Core.Application.Generics;
using Infrastructure.Contexts;
using Infrastructure.Generics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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