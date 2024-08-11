using Ardalis.Specification.EntityFrameworkCore;
using Core.Domain.Abstracts;
using Infrastructure.Contexts;

namespace Infrastructure.Generics;

/// <summary>
///     Универсальный репозиторий для работы с базой данных.
///     Этот класс предоставляет основные операции для сущностей, наследуемых от <see cref="AbstractGuidModel" />.
/// </summary>
/// <typeparam name="TEntity">
///     Тип сущности, который соответствует таблице в базе данных и наследуется от <see cref="AbstractGuidModel" />.
/// </typeparam>
public class GenericRepository<TEntity>(LeylaSiteDbContext dbContext) : RepositoryBase<TEntity>(dbContext)
    where TEntity : AbstractGuidModel;