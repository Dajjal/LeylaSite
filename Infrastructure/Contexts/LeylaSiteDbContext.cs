using Core.Domain.Models.Store.Site;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

/// <summary>
///     Контекст базы данных для сайта Leyla.
///     Этот класс управляет взаимодействием с таблицами базы данных,
///     содержащими информацию о товарах и категориях.
/// </summary>
public class LeylaSiteDbContext : DbContext
{
    /// <summary>
    ///     Инициализирует новый экземпляр класса <see cref="LeylaSiteDbContext" />.
    /// </summary>
    /// <param name="options">Настройки контекста для <see cref="DbContext" />.</param>
    public LeylaSiteDbContext(DbContextOptions<LeylaSiteDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    ///     Таблица товаров.
    /// </summary>
    public DbSet<ProductModel> Products { get; init; } = null!;

    /// <summary>
    ///     Таблица категорий.
    /// </summary>
    public DbSet<CategoryModel> Categories { get; init; } = null!;

    /// <summary>
    ///     Метод конфигурации моделей при создании схемы базы данных.
    /// </summary>
    /// <param name="modelBuilder">Построитель моделей, используемый для конфигурации сущностей.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настройка связи между сущностями ProductModel и CategoryModel
        modelBuilder.Entity<ProductModel>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        // Здесь можно добавить дополнительные настройки моделей
    }
}