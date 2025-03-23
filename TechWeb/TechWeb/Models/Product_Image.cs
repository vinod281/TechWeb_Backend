using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechWeb.Models;

public class Product_Image
{
    [Key]
    public int ImageId { get; set; }
    
    [ForeignKey("Product")]
    public int ProductID { get; set; }
    
    [Column(TypeName = "varchar(100)")]
    public string ImageName { get; set; }
    
    
}