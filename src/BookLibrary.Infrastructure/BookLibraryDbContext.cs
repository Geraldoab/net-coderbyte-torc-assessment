using Microsoft.EntityFrameworkCore;
using BookLibrary.Core.Entities;
using BookLibrary.Infrastructure.Configurations;

namespace BookLibrary.Infrastructure;

public class BookLibraryDbContext(DbContextOptions<BookLibraryDbContext> options)
    : DbContext(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    public DbSet<Publisher> Publishers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        modelBuilder.ApplyConfiguration(new PublisherConfiguration());
    }
}
