using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibrary.Core.Entities;

public class Book
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int TotalCopies { get; set; } = 0;

    public int CopiesInUse { get; set; } = 0;

    public string Type { get; set; }

    public string ISBN { get; set; }

    public string Category { get; set; }

    [ForeignKey(nameof(Author))]
    public int AuthorId { get; set; }

    [ForeignKey(nameof(Publisher))]
    public int PublisherId { get; set; }

    public virtual Author Author { get; set; }

    public virtual Publisher Publisher { get; set; }

    [NotMapped]
    public int TotalItemCount { get; set; } = 0;
}