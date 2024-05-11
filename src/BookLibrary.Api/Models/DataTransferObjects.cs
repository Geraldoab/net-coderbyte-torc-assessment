using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Api.Models;

public class BookDataTransferObject
{
    public string BookTitle { get; set; }
    public string Publisher { get; set; }
    public string Authors { get; set; }
    public string Type { get; set; }
    public string ISBN { get; set; }
    public string Category { get; set; }
    public string AvailableCopies { get; set; }
}