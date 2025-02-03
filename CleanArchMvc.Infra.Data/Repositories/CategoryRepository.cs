using System.Collections.Generic;
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

    public Category Create(Category category)
    {
        _categoryContext.Add(category);
        _categoryContext.SaveChangesAsync();
        return category;
    }

    public Category GetById(int? id)
    {
        return _categoryContext.Categories.Find(id);
    }

    public IEnumerable<Category> GetCategories()
    {
        return _categoryContext.Categories.AsNoTracking().ToList();
    }

    public IEnumerable<Category> GetCategories(string nome, int pagina, int tamanhoPagina)
    {
        var skip = (pagina - 1) * tamanhoPagina;

        var categories = _categoryContext.Categories.AsNoTracking().ToList();

        if (!string.IsNullOrEmpty(nome))
            categories = categories.Where(x => x.Name!.ToLower().Contains(nome.ToLower())).ToList();

        categories = categories.Skip(skip).Take(tamanhoPagina).ToList();
        return categories;
    }

    public Category Remove(Category category)
    {
        _categoryContext.Remove(category);
        _categoryContext.SaveChanges();
        return category;
    }

    public Category Update(Category category)
    {
        _categoryContext.Update(category);
        _categoryContext.SaveChanges();
        return category;
    }
}
