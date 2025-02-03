using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Domain.Interfaces;

public interface ICategoryRepository
{
    IEnumerable<Category> GetCategories();
    IEnumerable<Category> GetCategories(string nome, int pagina, int tamanhoPagina);
    Category GetById(int? id);
    Category Create(Category category);
    Category Update(Category category);
    Category Remove(Category category);
}
