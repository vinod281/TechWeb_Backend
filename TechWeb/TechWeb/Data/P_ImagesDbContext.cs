using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
using TechWeb.Models;

namespace TechWeb.Data;

public class PImagesDbContext: DbContext
{
    public PImagesDbContext(DbContextOptions<PImagesDbContext> options):base(options)
    {
        
    }
    
    public DbSet<Product_Image> P_Images { get; set; }
    public DbSet<Product> Products { get; set; }
}