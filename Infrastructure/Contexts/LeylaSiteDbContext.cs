using Core.Domain.Models.Store.Admin;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

/// <summary>
///     Контекст Базы Данных
/// </summary>
/// <param name="options">Настройки контекст для DbContext</param>
public class LeylaSiteDbContext(DbContextOptions<LeylaSiteDbContext> options) : DbContext(options)
{
    /// <summary>
    ///     Таблица товаров
    /// </summary>
    public DbSet<ProductModel> Products { get; init; }
}