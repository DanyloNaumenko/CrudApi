using CrudApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController: ControllerBase
{
    private readonly Product[] _products = [
        new Product(1, "First", 10),
        new Product(2, "Second", 20),
        new Product(3, "Third", 30)
    ];
    
    [HttpGet]
    public IEnumerable<Product> Get()
    {
        return _products;
    }

    [HttpGet("{id}")]
    public ActionResult<Product> Get(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        return product == null ? NotFound() : Ok(product);
    }
}