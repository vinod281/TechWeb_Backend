using Microsoft.AspNetCore.Mvc;
using TechWeb.Data;

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

    [HttpGet]
    public async Task<IActionResult> GetProductImages()
    {
        return Ok();
    }
}