using Core.Domain.Models.Core;
using Core.Domain.Models.Instagram;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class SiteDbContext(DbContextOptions<SiteDbContext> options) : IdentityDbContext<UserModel>(options)
{
    public DbSet<InstagramPostModel> InstagramPostModels { get; init; }
}