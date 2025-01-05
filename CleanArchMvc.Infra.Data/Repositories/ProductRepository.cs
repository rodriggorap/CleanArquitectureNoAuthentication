using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories;

public class ProductRepository : IProductRepository
{
    ApplicationDbContext _productContext;

    public ProductRepository(ApplicationDbContext productContext)
    {
        _productContext = productContext;
    }

    public async Task<Product> Create(Product product)
    {
        _productContext.Products.Add(product);
        await _productContext.SaveChangesAsync();
        return product;
    }

    public async Task<Product> GetById(int? id)
    {
        return await _productContext.Products.FindAsync(id);
    }

    public async Task<Product> GetProductCategory(int? id)
    {
        return await _productContext.Products.Include(c => c.Category)
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetProduts()
    {
        return await _productContext.Products.AsNoTracking().ToListAsync();
    }

    public async Task<Product> Remove(Product product)
    {
        _productContext.Products.Remove(product);
        await _productContext.SaveChangesAsync();
        return product;
    }

    public async Task<Product> Update(Product product)
    {
        _productContext.Products.Update(product);
        await _productContext.SaveChangesAsync();
        return product;
    }
}
