using Microsoft.EntityFrameworkCore;
using BookLibrary.Core.Entities;
using BookLibrary.Infrastructure.Configurations;

namespace BookLibrary.Infrastructure;

public class BookLibraryDbContext(DbContextOptions<BookLibraryDbContext> options)
    : DbContext(options)
{
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookConfiguration());
    }
}