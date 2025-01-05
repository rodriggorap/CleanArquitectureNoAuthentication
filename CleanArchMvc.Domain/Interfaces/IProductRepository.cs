using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Domain.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProduts();
    Task<Product> GetById(int? id);
    Task<Product> GetProductCategory(int? id);
    Task<Product> Create(Product product);
    Task<Product> Update(Product product);
    Task<Product> Remove(Product product);
}
