using Microsoft.EntityFrameworkCore;
using BookLibrary.Core.Entities;
using BookLibrary.Infrastructure.Configurations;

namespace BookLibrary.Infrastructure;

public class BookLibraryDbContext(DbContextOptions<BookLibraryDbContext> options)
    : DbContext(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Book> Authors { get; set; }

    public DbSet<Book> Publishers { get; set; }

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