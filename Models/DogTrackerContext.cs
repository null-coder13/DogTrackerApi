using Microsoft.EntityFrameworkCore;

namespace DogTrackerApi.Models;

public class DogTrackerContext : DbContext
{
    public DogTrackerContext(DbContextOptions<DogTrackerContext> options)
        : base(options)
    {
    }

    public DbSet<Entry> Entries { get; set; } = null!;
}
