using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Api.Models;

public class BookSearchDataTransferObject
{
    public int Id { get; set; }
    public string? BookTitle { get; set; }
    public string? Publisher { get; set; }
    public string? Authors { get; set; }
    public string? Type { get; set; }
    public string? ISBN { get; set; }
    public string? Category { get; set; }
    public string? AvailableCopies { get; set; }
}

public record BookDataTransferObject(
    [Required] string Title,
    [Required] string FirstName,
    [Required] string LastName,
    [Required] string Type,
    [Required] string ISBN,
    [Required] string Category,
    [Required] int AuthorId,
    [Required] int PublisherId,
    int TotalCopies = 0,
    int id = 0);

public record AuthorDataTransferObject([Required] int Id, [Required] string Name);
public record PublisherDataTransferObject([Required] int Id, [Required] string Name);