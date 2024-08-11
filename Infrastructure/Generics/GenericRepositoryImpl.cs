using Ardalis.Specification.EntityFrameworkCore;
using Core.Domain.Abstracts;
using Infrastructure.Contexts;

namespace Infrastructure.Generics;

/// <summary>
///     Универсальный репозитори для работы с Базой Данных
/// </summary>
/// <param name="dbContext">Передаётся контекст Базы Данных с которой нужно работать</param>
/// <typeparam name="TEntity">Модель описанная в Базе Данных (Таблица)</typeparam>
public class GenericRepositoryImpl<TEntity>(LeylaSiteDbContext dbContext)
    : RepositoryBase<TEntity>(dbContext) where TEntity : AbstractGuidModel;