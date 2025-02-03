using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces;

public interface ICategoryService
{
    IEnumerable<CategoryDTO> GetCategories();
    IEnumerable<CategoryDTO> GetCategories(string nome, int pagina, int tamanhoPagina);
    CategoryDTO GetById(int? id);
    CategoryDTO Add(CategoryDTO categoryDTO);
    CategoryDTO Update(CategoryDTO categoryDTO);
    CategoryDTO Remove(int id);
}
