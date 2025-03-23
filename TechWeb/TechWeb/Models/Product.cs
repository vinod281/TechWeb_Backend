using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace TechWeb.Models;

public class Product
{
    [Key]
    public int Product_ID { get; set; }
    
    [Required]
    [Column(TypeName = "varchar(255)")]
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
    [Column(TypeName = "varchar(255)")]
    public string Category { get; set; }
    
    [Column(TypeName = "nvarchar(1000)")]
    public string Review { get; set; }
    
    [Column(TypeName = "varchar(1000)")]
    public string Image { get; set; }
    
    
    
}