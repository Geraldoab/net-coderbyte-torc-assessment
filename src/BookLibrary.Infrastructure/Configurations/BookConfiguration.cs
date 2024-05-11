using BookLibrary.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace BookLibrary.Infrastructure.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id).ValueGeneratedOnAdd();

        builder.Property(f => f.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(f => f.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(f => f.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(f => f.TotalCopies)
            .IsRequired();

        builder.Property(f => f.CopiesInUse)
            .IsRequired();

        builder.Property(f => f.Type)
            .HasMaxLength(50);

        builder.Property(f => f.ISBN)
            .HasMaxLength(80);

        builder.Property(f => f.Category)
            .HasMaxLength(50);

        builder.HasData(new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Pride and Prejudice",
                FirstName = "Jane",
                LastName = "Austen",
                TotalCopies = 100,
                CopiesInUse = 80,
                Type = "Hardcover",
                ISBN = "1234567891",
                Category = "Fiction"
            },
            new Book
            {
                Id = 2,
                Title = "To Kill a Mockingbird",
                FirstName = "Harper",
                LastName = "Lee",
                TotalCopies = 75,
                CopiesInUse = 65,
                Type = "Paperback",
                ISBN = "1234567892",
                Category = "Fiction"
            },
            new Book
            {
                Id = 3,
                Title = "The Catcher in the Rye",
                FirstName = "J.D.",
                LastName = "Salinger",
                TotalCopies = 50,
                CopiesInUse = 45,
                Type = "Hardcover",
                ISBN = "1234567893",
                Category = "Fiction"
            },
            new Book
            {
                Id = 4,
                Title = "The Great Gatsby",
                FirstName = "F. Scott",
                LastName = "Fitzgerald",
                TotalCopies = 50,
                CopiesInUse = 20,
                Type = "Hardcover",
                ISBN = "1234567894",
                Category = "Non-Fiction"
            },
            new Book
            {
                Id = 5,
                Title = "The Alchemist",
                FirstName = "Paulo",
                LastName = "Coelho",
                TotalCopies = 50,
                CopiesInUse = 35,
                Type = "Hardcover",
                ISBN = "1234567895",
                Category = "Biography"
            },
            new Book
            {
                Id = 6,
                Title = "The Book Thief",
                FirstName = "Markus",
                LastName = "Zusak",
                TotalCopies = 75,
                CopiesInUse = 11,
                Type = "Hardcover",
                ISBN = "1234567896",
                Category = "Mystery"
            },
            new Book
            {
                Id = 7,
                Title = "The Chronicles of Narnia",
                FirstName = "C.S.",
                LastName = "Lewis",
                TotalCopies = 100,
                CopiesInUse = 14,
                Type = "Paperback",
                ISBN = "1234567897",
                Category = "Sci-Fi"
            },
            new Book
            {
                Id = 8,
                Title = "The Da Vinci Code",
                FirstName = "Dan",
                LastName = "Brown",
                TotalCopies = 50,
                CopiesInUse = 40,
                Type = "Paperback",
                ISBN = "1234567898",
                Category = "Sci-Fi"
            },
            new Book
            {
                Id = 9,
                Title = "The Grapes of Wrath",
                FirstName = "John",
                LastName = "Steinbeck",
                TotalCopies = 50,
                CopiesInUse = 35,
                Type = "Hardcover",
                ISBN = "1234567899",
                Category = "Fiction"
            },
            new Book
            {
                Id = 10,
                Title = "The Hitchhiker's Guide to the Galaxy",
                FirstName = "Douglas",
                LastName = "Adams",
                TotalCopies = 50,
                CopiesInUse = 35,
                Type = "Paperback",
                ISBN = "1234567900",
                Category = "Non-Fiction"
            },
            new Book
            {
                Id = 11,
                Title = "Moby-Dick",
                FirstName = "Herman",
                LastName = "Melville",
                TotalCopies = 30,
                CopiesInUse = 8,
                Type = "Hardcover",
                ISBN = "8901234567",
                Category = "Fiction"
            },
            new Book
            {
                Id = 12,
                Title = "To Kill a Mockingbird",
                FirstName = "Harper",
                LastName = "Lee",
                TotalCopies = 20,
                CopiesInUse = 0,
                Type = "Paperback",
                ISBN = "9012345678",
                Category = "Non-Fiction"
            },
            new Book
            {
                Id = 13,
                Title = "The Catcher in the Rye",
                FirstName = "J.D.",
                LastName = "Salinger",
                TotalCopies = 10,
                CopiesInUse = 1,
                Type = "Hardcover",
                ISBN = "0123456789",
                Category = "Non-Fiction"
            },
        });
    }
}