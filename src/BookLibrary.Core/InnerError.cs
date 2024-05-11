using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Core;

public abstract class InnerError
{
    public string Code { get; set; }

    [Required]
    public abstract string ErrorType { get; }
}