using CrudApi.Models;

namespace CrudApi.Services;

public class ProductService
{
    private readonly List<Product> _products =
    [
        new Product(1, "First", 10),
        new Product(2, "Second", 20),
        new Product(3, "Third", 30)
    ];
    
    public IEnumerable<Product> GetAll()
    {
        return _products;
    }
    
    public Product? GetById(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        return product;
    }
    public Product Create(Product product)
    {
        _products.Add(product);
        return product;
    }

    public bool DeleteById(int id)
    {
        var product = GetById(id);
        return product != null && _products.Remove(product);
    }

    public bool Update(Product inputProduct, int id)
    {
        var newProduct = inputProduct with { Id = id };
        var index = _products.FindIndex(p => p.Id == id);
        if (index < 0) return false;
        _products[index] = newProduct;
        return true;
    }
}