using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    ApplicationDbContext _categoryContext;

    public CategoryRepository(ApplicationDbContext categoryContext)
    {
        _categoryContext = categoryContext;
    }

    public async Task<Category> Create(Category category)
    {
        _categoryContext.Add(category);
        await _categoryContext.SaveChangesAsync();
        return category;
    }

    public async Task<Category> GetById(int? id)
    {
        return await _categoryContext.Categories.FindAsync(id);
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await _categoryContext.Categories.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Category>> GetCategories(int pagina, int tamanhoPagina)
    {
        var categories =  await _categoryContext.Categories.AsNoTracking().ToListAsync();
        if (pagina == 0)
        {
            pagina = 1;
        }
        var skip = (pagina - 1) * tamanhoPagina;
        categories = categories.Skip(skip).Take(tamanhoPagina).ToList();
        return categories;
    }

    public async Task<Category> Remove(Category category)
    {
        _categoryContext.Remove(category);
        await _categoryContext.SaveChangesAsync();
        return category;
    }

    public async Task<Category> Update(Category category)
    {
        _categoryContext.Update(category);
        await _categoryContext.SaveChangesAsync();
        return category;
    }
}
