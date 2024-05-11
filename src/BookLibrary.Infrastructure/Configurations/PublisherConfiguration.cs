using BookLibrary.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace BookLibrary.Infrastructure.Configurations;

public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
{
    public void Configure(EntityTypeBuilder<Publisher> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id).ValueGeneratedOnAdd();

        builder.Property(f => f.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasData(new List<Publisher>
        {
            new Publisher
            {
                Id = 1,
                Name = "New York Times"
            }, 
            new Publisher
            {
                Id = 2,
                Name = "Imon & Schuster Inc"
            },
            new Publisher
            {
                Id = 3,
                Name = "Scholastic Inc"
            },
        });
    }
}