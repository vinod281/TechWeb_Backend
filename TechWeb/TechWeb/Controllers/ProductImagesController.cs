using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechWeb.Data;
using TechWeb.Models;

namespace TechWeb.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ProductImagesController : Controller
{
    // GET
    private readonly PImagesDbContext _context;
    private readonly IWebHostEnvironment _env;
    
    public ProductImagesController(PImagesDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        this._env = env;
    }

    [HttpGet("getImages")]
    public async Task<ActionResult<IEnumerable<Product_Image>>> GetProductImages()
    {
        var Product_Images = await _context.P_Images.ToListAsync();
        return Ok(Product_Images);
    }

    [HttpPost("sentImages")]
    public async Task<ActionResult<IEnumerable<Product_Image>>> SentImages(Product_Image Product_Image)
    {
        _context.P_Images.Add(Product_Image);
        await _context.SaveChangesAsync();

        return Ok(Product_Image);
    }
}