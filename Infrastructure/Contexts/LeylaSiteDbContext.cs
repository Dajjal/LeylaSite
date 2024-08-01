using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class LeylaSiteDbContext(DbContextOptions<LeylaSiteDbContext> options) : DbContext(options)
{
}