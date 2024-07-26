using Core.Domain.Models.Instagram;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class SiteDbContext(DbContextOptions<SiteDbContext> options) : DbContext(options)
{
    public DbSet<InstagramPostModel> InstagramPostModels { get; init; }
}