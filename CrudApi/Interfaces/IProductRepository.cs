using CrudApi.Models;

namespace CrudApi.Interfaces;

public interface IProductRepository
{
    IEnumerable<Product> GetAll();
    Product? GetById(int id);
    Product Create(Product product);
    bool Update(Product inputProduct, int id);
    bool DeleteById(int id);
}