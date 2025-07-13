using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models;

public class Book
{
    [Key]
    public Guid Id { get; } = Guid.NewGuid();
    public required string Title { get; set; }
    public required string Author { get; set; }
    public required string Genre { get; set; } = "Others";
    public required double Price { get; set; } = 0.00;
    public required uint Quantity { get; set; } = 0U;
}