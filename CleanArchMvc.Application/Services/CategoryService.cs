using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services;

public class CategoryService : ICategoryService
{
    private ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public IEnumerable<CategoryDTO> GetCategories()
    {
        var categoriesEntity = _categoryRepository.GetCategories();
        return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
    }

    public IEnumerable<CategoryDTO> GetCategories(string nome, int pagina, int tamanhoPagina)
    {
        var categoriesEntity = _categoryRepository.GetCategories(nome, pagina, tamanhoPagina);
        return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
    }

    public CategoryDTO GetById(int? id)
    {
        var categoryEntity = _categoryRepository.GetById(id);
        return _mapper.Map<CategoryDTO>(categoryEntity);
    }

    public CategoryDTO Add(CategoryDTO categoryDTO)
    {
        var categoryEntity = _mapper.Map<Category>(categoryDTO);
        categoryEntity = _categoryRepository.Create(categoryEntity);
        return _mapper.Map<CategoryDTO>(categoryEntity);
    }

    public CategoryDTO Update(CategoryDTO categoryDTO)
    {
        var categoryEntity = _mapper.Map<Category>(categoryDTO);
        categoryEntity = _categoryRepository.Update(categoryEntity);
        return _mapper.Map<CategoryDTO>(categoryEntity);
    }

    public CategoryDTO Remove(int id)
    {
        var categoryEntity = _categoryRepository.GetById(id);
        categoryEntity = _categoryRepository.Remove(categoryEntity);
        return _mapper.Map<CategoryDTO>(categoryEntity);
    }
}
