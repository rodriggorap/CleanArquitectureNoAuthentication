using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
    {
        var categories = await _categoryService.GetCategories();

        return Ok(categories);
    }

    [HttpGet("paginado")]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get(int pagina, int tamanhoPagina)
    {
        var categories = await _categoryService.GetCategories(pagina, tamanhoPagina);

        return Ok(categories);
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    public async Task<ActionResult<CategoryDTO>> Get(int id)
    {
        var category = await _categoryService.GetById(id);

        if (category == null)
        {
            return NotFound("Category not found");
        }

        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDTO>> Post([FromBody] CategoryDTO categoryDTO)
    {
        var categories = await _categoryService.GetCategories();

        var category = categories
            .Where(c => c.Name.Equals(categoryDTO.Name, StringComparison.OrdinalIgnoreCase));

        if (category.Any())
        {
            return BadRequest("Categoria já cadastrada...");
        }

        var categoryDtoCreate = await _categoryService.Add(categoryDTO);

        return new CreatedAtRouteResult("GetCategory", new {id = categoryDtoCreate.Id}, categoryDtoCreate);
    }

    [HttpPut]
    public async Task<ActionResult<CategoryDTO>> Put(int id, [FromBody] CategoryDTO categoryDTO)
    {
        if (id != categoryDTO.Id)
        {
            return BadRequest("Dados inválidos...");
        }

        var categories = await _categoryService.GetCategories();
        
        var category = categories
            .Where(c => c.Name.Equals(categoryDTO.Name, StringComparison.OrdinalIgnoreCase)
            && c.Id != categoryDTO.Id);

        if (category.Any())
        {
            return BadRequest("Categoria já cadastrada...");
        }

        var categoryDtoUpdate = await _categoryService.Update(categoryDTO);

        return Ok(categoryDtoUpdate);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CategoryDTO>> Delete(int id)
    {
        var category = await _categoryService.GetById(id);
        if (category == null)
        {
            return NotFound();
        }

        var categoryDtoDelete = await _categoryService.Remove(id);

        return Ok(categoryDtoDelete);
    }
}
