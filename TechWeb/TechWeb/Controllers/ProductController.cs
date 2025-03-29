using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechWeb.Data;
using TechWeb.Migrations;
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

    [HttpPost("AddProduct-text-only")]   // add product texts
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        
            return Ok(product);
        
    }

    [HttpGet("GetAllProducts")]  // get all the products
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var Products = await _context.Products.ToListAsync();
        return Ok(Products);
    }
    
    

    [HttpPost("AddProduct")]   // add product with image uploading
    public async Task<IActionResult> AddProduct([FromForm] Product product)
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

                string imagePath = filePath + "//"+fileName;

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
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        return Ok(new{success = result,product});
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsNew()
    {
        return await _context.Products
            .Select(x =>new Product()
            {
                Product_ID = x.Product_ID,
                Title = x.Title,
                Category = x.Category,
                Price = x.Price,
                Offer = x.Offer,
                Stock = x.Stock,
                Rating = x.Rating,
                Review = x.Review,
                Image = string.Format("{0}://{1}{2}/Images/{3}/{4}",Request.Scheme,Request.Host,Request.PathBase,x.Image,x.Image),
                
                
            })
            .ToListAsync();
    }

    
    [NonAction]
    private string GetFilePath(string productCode) // function for get file path
    {
        return this._env.WebRootPath +"//Uploads//Product//"+productCode;
    }
    
}