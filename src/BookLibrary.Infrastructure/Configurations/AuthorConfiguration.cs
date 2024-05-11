using BookLibrary.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace BookLibrary.Infrastructure.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id).ValueGeneratedOnAdd();

        builder.Property(f => f.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasData(new List<Author>
        {
            new Author
            {
                Id = 1,
                Name = "Dan Heath, Chip, Heath"
            }, 
            new Author
            {
                Id = 2,
                Name = "James Frey"
            },
            new Author
            {
                Id = 3,
                Name = "Pam Muñoz Ryan"
            },
        });
    }
}