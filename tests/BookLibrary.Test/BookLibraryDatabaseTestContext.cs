using Microsoft.EntityFrameworkCore;
using BookLibrary.Infrastructure;

namespace BookLibrary.Test;

public class BookLibraryDatabaseTestContext() : BookLibraryDbContext(
    new DbContextOptionsBuilder<BookLibraryDbContext>()
        .UseInMemoryDatabase(nameof(BookLibraryDatabaseTestContext))
        .Options)
{
    public void CreateDatabase()
    {
        Database.EnsureCreated();
    }
    
    public void ResetTracking()
    {
        ChangeTracker.Clear();
    }
    
    public void DisposeDatabase()
    {
        Database.EnsureDeleted();
    }
}