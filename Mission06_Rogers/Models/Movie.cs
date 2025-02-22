using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission06_Rogers.Models;

public class Movie // model for movie form
{
    [Key]
    [Required]
    public int MovieId { get; set; }
    
    [ForeignKey("CategoryId")]
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
    
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "Year is required")]
    [Range(1888, int.MaxValue, ErrorMessage = "Year must be 1888 or later")]
    public int Year { get; set; }
    public string? Director { get; set; }
    public string? Rating { get; set; }
    
    [Required(ErrorMessage = "Edited field is required")]
    public bool Edited { get; set; }
    public string? LentTo { get; set; }
    
    [Required(ErrorMessage = "Copied to Plex is required")]
    public bool CopiedToPlex { get; set; }
    
    [MaxLength(25)]
    public string? Notes { get; set; }
}