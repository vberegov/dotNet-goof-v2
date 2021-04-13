using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  [ApiController] //responsible for mapping parameters that are passed into the methods
  [Route("api/[controller]")]
  public class ProductsController : ControllerBase
  {
    private readonly IProductRepository _repo;
    public ProductsController(IProductRepository repo)
    {
      _repo = repo;

    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts() //running async method
    {
      var products = await _repo.GetProductsAsync(); // query going to our db, return results

      return Ok(products);
    }

    //api controller is doing validation that root parameter is an int
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
      return await _repo.GetProductByIdAsync(id);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
      return Ok(await _repo.GetProductBrandsAsync());
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
      return Ok(await _repo.GetProductTypesAsync());
    }
  }
}