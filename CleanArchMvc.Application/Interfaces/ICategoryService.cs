using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDTO>> GetCategories();
    Task<IEnumerable<CategoryDTO>> GetCategories(int pagina, int tamanhoPagina);
    Task<CategoryDTO> GetById(int? id);
    Task<CategoryDTO> Add(CategoryDTO categoryDTO);
    Task<CategoryDTO> Update(CategoryDTO categoryDTO);
    Task<CategoryDTO> Remove(int id);
}
