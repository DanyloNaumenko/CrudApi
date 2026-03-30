using CrudApi.Models;
using CrudApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(ProductService productService) : ControllerBase
{
    [HttpGet]
    public IEnumerable<Product> GetAll()
    {
        return productService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = productService.GetById(id);
        return product != null ? Ok(product) : NotFound();
    }

    [HttpPost]
    public ActionResult Post([FromBody] Product product)
    {
        var result = productService.Create(product);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        return productService.DeleteById(id) ? NoContent() : NotFound();
    }
    [HttpPut("{id}")]
    public ActionResult Put([FromBody] Product product, int id)
    {
        return productService.Update(product, id) ? NoContent() : NotFound();
    }
}