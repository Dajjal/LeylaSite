using Ardalis.Specification;
using AutoMapper;
using Core.Application.Generics;
using Infrastructure.Contexts;
using Infrastructure.Generics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ServiceExt;

public static class LeylaSiteServicesExtension
{
    public static IServiceCollection RegisterLeylaSiteServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Контекст Базы Данных
        services.AddDbContext<LeylaSiteDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("ConnectionString"),
                builder => builder.MigrationsAssembly(typeof(LeylaSiteDbContext).Assembly.FullName)));

        #region Внедрение сервисов и репозитори

        // Универсальный репозитори
        services.AddScoped(typeof(IRepositoryBase<>), typeof(GenericRepositoryImpl<>));
        // Универсальный сервис с логическим удалением
        services.AddScoped(typeof(IGenericService<>), typeof(GenericLogicalDeleteServiceImpl<>));
        // Универсальный сервис с физическим удалением в Базе Данных
        services.AddScoped(typeof(IGenericService<>), typeof(GenericPhysicalDeleteServiceImpl<>));

        #endregion

        // Автомаппер
        services.AddScoped(typeof(IMapperBase), typeof(Mapper));
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}