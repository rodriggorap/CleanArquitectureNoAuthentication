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

    public async Task<IEnumerable<CategoryDTO>> GetCategories()
    {
        var categoriesEntity = await _categoryRepository.GetCategories();
        return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategories(int pagina, int tamanhoPagina)
    {
        var categoriesEntity = await _categoryRepository.GetCategories(pagina, tamanhoPagina);
        return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
    }

    public async Task<CategoryDTO> GetById(int? id)
    {
        var categoryEntity = await _categoryRepository.GetById(id);
        return _mapper.Map<CategoryDTO>(categoryEntity);
    }

    public async Task<CategoryDTO> Add(CategoryDTO categoryDTO)
    {
        var categoryEntity = _mapper.Map<Category>(categoryDTO);
        categoryEntity = await _categoryRepository.Create(categoryEntity);
        return _mapper.Map<CategoryDTO>(categoryEntity);
    }

    public async Task<CategoryDTO> Update(CategoryDTO categoryDTO)
    {
        var categoryEntity = _mapper.Map<Category>(categoryDTO);
        categoryEntity = await _categoryRepository.Update(categoryEntity);
        return _mapper.Map<CategoryDTO>(categoryEntity);
    }

    public async Task<CategoryDTO> Remove(int id)
    {
        var categoryEntity = _categoryRepository.GetById(id).Result;
        categoryEntity = await _categoryRepository.Remove(categoryEntity);
        return _mapper.Map<CategoryDTO>(categoryEntity);
    }
}
