using Ardalis.Specification;
using Core.Application.Generics;
using Infrastructure.Contexts;
using Infrastructure.Generics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ServiceExt;

/// <summary>
///     Расширения для конфигурации служб и middleware для проекта LeylaSite.
/// </summary>
public static class LeylaSiteServicesExtension
{
    /// <summary>
    ///     Регистрация всех необходимых сервисов и настроек для проекта LeylaSite.
    ///     Включает настройку контекста базы данных, репозиториев, сервисов и AutoMapper.
    /// </summary>
    /// <param name="services">
    ///     Коллекция сервисов для регистрации зависимостей.
    /// </param>
    /// <param name="configuration">
    ///     Конфигурация приложения для получения строк подключения и других настроек.
    /// </param>
    /// <returns>Обновлённая коллекция сервисов.</returns>
    public static IServiceCollection RegisterLeylaSiteServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Настройка контекста базы данных
        services.AddDbContext<LeylaSiteDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(LeylaSiteDbContext).Assembly.FullName)));

        #region Внедрение зависимостей для репозиториев и сервисов

        // Регистрация универсального репозитория
        services.AddScoped(typeof(IRepositoryBase<>), typeof(GenericRepository<>));
        // Регистрация универсального сервиса
        services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

        #endregion

        // Настройка AutoMapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}