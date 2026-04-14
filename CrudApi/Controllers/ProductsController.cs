using CrudApi.DTOs;
using CrudApi.Interfaces;
using CrudApi.Models;
using CrudApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public IEnumerable<ProductDto> GetAll()
    {
        return productService
            .GetAll()
            .Select(p => new ProductDto(p.Id, p.Name, p.Price));
    }

    [HttpGet("{id}")]
    public ActionResult<ProductDto> GetById(int id)
    {
        var product = productService.GetById(id);
        if(product == null) return NotFound();
        var responseObjectDto = new ProductDto(product.Id, product.Name, product.Price);
        return Ok(responseObjectDto);
    }

    [HttpPost]
    public ActionResult Post([FromBody] CreateProductDto createProductDto)
    {
        if(createProductDto.Price < 0 || createProductDto.Name.Trim().Length == 0) return BadRequest();
        
        var product = new Product(0, createProductDto.Name, createProductDto.Price);
        var responseProduct = productService.Create(product);
        var responseProductDto = new ProductDto(responseProduct.Id, responseProduct.Name, responseProduct.Price);
        return CreatedAtAction(nameof(GetById), new { id = responseProductDto.Id }, responseProductDto);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        return productService.DeleteById(id) ? NoContent() : NotFound();
    }
    [HttpPut("{id}")]
    public ActionResult Put([FromBody] UpdateProductDto updateProductDto, int id)
    {
        if(updateProductDto.Price < 0 || updateProductDto.Name.Trim().Length == 0) return BadRequest();
        
        var newProduct = new Product(0, updateProductDto.Name, updateProductDto.Price);
        return productService.Update(newProduct, id) ? NoContent() : NotFound();
    }
}