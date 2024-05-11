using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Api.Models;

public record BookDataTransferObject(
    [Required]int Id,
    [Required]string Title, 
    [Required]string FirstName, 
    [Required]string LastName, 
    [Required]int TotalCopies,
    [Required]int CopiesInUse,
    [Required]string Type,
    [Required]string ISBN,
    [Required]string Category);