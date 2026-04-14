using CrudApi.Interfaces;
using CrudApi.Models;
using Dapper;
using Npgsql;

namespace CrudApi.Repositories;

public class ProductRepository: IProductRepository
{
    private readonly NpgsqlDataSource _dataSource;
    public ProductRepository(NpgsqlDataSource npgsqlDataSource)
    {
        _dataSource = npgsqlDataSource;
    }
    public IEnumerable<Product> GetAll()
    {
        using(var connection = _dataSource.OpenConnection())
        {
            var sqlRequest = "SELECT * FROM products";
            var result = connection.Query<Product>(sqlRequest).ToList();
            return result;
        }
    }

    public Product? GetById(int id)
    {
        using (var connection = _dataSource.OpenConnection())
        {
            var sqlRequest = "SELECT * FROM products WHERE id = @id";
            var result = connection.QueryFirstOrDefault<Product>(sqlRequest, new {id});
            return result;
        }
    }

    public Product Create(Product product)
    {   
        using (var connection = _dataSource.OpenConnection())
        {
            var sqlRequest = "INSERT INTO products (name, price) VALUES (@name, @price) " +
                                    "RETURNING id";
            
            var newId = connection.QueryFirstOrDefault<int>(sqlRequest, new { product.Name, product.Price });
            
            return product with {Id = newId};
        }
    }

    public bool Update(Product inputProduct, int id)
    {
        using (var connection = _dataSource.OpenConnection())
        {
            var sqlRequest = "UPDATE products SET name = @name, price = @price WHERE id = @id";
            var result = connection.Execute(sqlRequest, new {inputProduct.Name, inputProduct.Price, id});
            
            return result > 0;
        }
    }

    public bool DeleteById(int id)
    {
        using (var connection = _dataSource.OpenConnection())
        {
            var sqlRequest = "DELETE FROM products WHERE id = @id";
            var result = connection.Execute(sqlRequest, new {id});
            
            return result > 0;
        }
    }
}