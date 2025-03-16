using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace TechWeb.Models;

public class Product
{
    [Key]
    public int Product_ID { get; set; }
    
    [Required]
    public string Title { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    public float Offer { get; set; }
    [Required] 
    public bool Stock { get; set; }
    [Required]
    public int Rating { get; set; }
    [Required]
    public string Category { get; set; }
    [Column]
    public string Image { get; set; }
    [Column]
    public string Review { get; set; }
    
}