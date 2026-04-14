using CrudApi.DTOs;
using CrudApi.Interfaces;
using CrudApi.Models;

namespace CrudApi.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }
    public IEnumerable<Product> GetAll()
    {
        return _repository.GetAll();
    }
    
    public Product? GetById(int id)
    {
        return _repository.GetById(id);
    }
    
    public Product Create(Product product)
    {
        return _repository.Create(product);
    }
    
    public bool Update(Product inputProduct, int id)
    {
        return _repository.Update(inputProduct, id);
    }
    
    public bool DeleteById(int id)
    {
        return _repository.DeleteById(id);
    }

    
}