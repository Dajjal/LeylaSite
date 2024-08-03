using Ardalis.Specification.EntityFrameworkCore;
using Core.Domain.Abstracts;
using Infrastructure.Contexts;

namespace Infrastructure.Generics;

public class GenericRepositoryImpl<TEntity>(LeylaSiteDbContext dbContext)
    : RepositoryBase<TEntity>(dbContext) where TEntity : AbstractLogicalDeleteGuidModel;