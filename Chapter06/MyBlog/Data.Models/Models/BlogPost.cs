namespace Data.Models;
using System.ComponentModel.DataAnnotations;
public class BlogPost
{
    public string? Id { get; set; }
    [MinLength(5)]
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Text { get; set; } = string.Empty;
    public DateTime PublishDate { get; set; }
    public Category? Category { get; set; }
    public List<Tag> Tags { get; set; } = new();
}
