using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechWeb.Data;
using TechWeb.Models;
namespace TechWeb.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ProductController : Controller
{
    
    private readonly ProductDbContext _context;
    private readonly IWebHostEnvironment _env;
    
    public ProductController(ProductDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        this._env = env;
    }

    [HttpPost("AddProduct")]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        
            return Ok(product);
        
    }

    [HttpGet("GetAllProducts")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var Products = await _context.Products.ToListAsync();
        return Ok(Products);
    }

    [HttpPost("UploadImage")]
    public async Task<IActionResult> UploadImage()
    {
        bool result = false;

        try
        {
            var uploadedFiles = Request.Form.Files;
            
            foreach (IFormFile source in uploadedFiles)
            {
                string fileName = source.FileName;
                string filePath = GetFilePath(fileName);

                if (!System.IO.Directory.Exists(filePath))
                {
                    System.IO.Directory.CreateDirectory(filePath);
                }

                string imagePath = filePath + "//image.png";

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                using (FileStream stream = System.IO.File.Create(imagePath))
                {
                    await source.CopyToAsync(stream);
                    result = true;
                }
            }

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        return Ok(result);
    }

    
    [NonAction]
    private string GetFilePath(string ProductCode)
    {
        return this._env.WebRootPath +"//Uploads//Product//"+ProductCode;
    }
    
}