using System.ComponentModel.DataAnnotations;

namespace MiniBookService.Api.Models;

public class Book
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Author { get; set; } = string.Empty;

    [Range(0, 10000)]
    public decimal Price { get; set; }
}


