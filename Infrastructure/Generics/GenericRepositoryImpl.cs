using Ardalis.Specification.EntityFrameworkCore;
using Core.Domain.Abstracts;
using Infrastructure.Contexts;

namespace Infrastructure.Generics;

public class GenericRepositoryImpl<TEntity>(SiteDbContext dbContext)
    : RepositoryBase<TEntity>(dbContext) where TEntity : AbstractGuidModel;