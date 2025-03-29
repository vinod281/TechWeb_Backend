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

    [HttpGet("{productID}")]
    public async Task<ActionResult<IEnumerable<Product_Image>>> GetImagesByProductId(int productID)
    {
        var Product_Images = await _context.P_Images.Where(p => p.ProductID == productID)
            .Select(p => new Product_Image
            {
                ImageId = p.ImageId,
                ProductID = p.ProductID,
                ImageName = string.Format("{0}://{1}{2}/Images/{3}/{4}",Request.Scheme,Request.Host,Request.PathBase,p.ImageName,p.ImageName)
            }
            )
            .ToListAsync();

        if (Product_Images == null)
        {
            return NotFound("No images found for the given Product ID.");
        }
        
        return Ok(Product_Images);
    }
    
    [HttpGet("GetProductsWithImages")]
    public async Task<ActionResult<IEnumerable<object>>> GetProductsWithImages()
    {
        var result = await (from p in _context.Products
                join i in _context.P_Images
                    on p.Product_ID equals i.ProductID // Ensure correct key match
                select new
                {
                    ProductID = p.Product_ID,
                    ProductTitle = p.Title,
                    ProductPrice = p.Price,
                    ProductOffer = p.Offer,
                    ProductReview = p.Review,
                    ProductStock = p.Stock,
                    ImageURL = string.Format("{0}://{1}{2}/Images/{3}/{4}",Request.Scheme,Request.Host,Request.PathBase,i.ImageName,i.ImageName)
                })
            .FirstOrDefaultAsync();

        return Ok(result);
    }


    

    
}