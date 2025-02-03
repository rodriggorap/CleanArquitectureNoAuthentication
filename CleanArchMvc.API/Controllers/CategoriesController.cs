using System.ComponentModel.DataAnnotations;
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
    public ActionResult<IEnumerable<CategoryDTO>> Get()
    {
        var categories = _categoryService.GetCategories();

        return Ok(categories);
    }

    [HttpGet("paginado")]
    public ActionResult<IEnumerable<CategoryDTO>> Get(
         [FromQuery] string? name,
         [FromQuery][Required] int pagina,
         [FromQuery][Required] int tamanhoPagina)
    {
        var categories = _categoryService.GetCategories(name!, pagina, tamanhoPagina);

        return Ok(categories);
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    public ActionResult<CategoryDTO> Get(int id)
    {
        var category = _categoryService.GetById(id);

        if (category == null)
        {
            return NotFound("Category not found");
        }

        return Ok(category);
    }

    [HttpPost]
    public ActionResult<CategoryDTO> Post([FromBody] CategoryDTO categoryDTO)
    {
        var categories = _categoryService.GetCategories();

        var category = categories
            .Where(c => c.Name.Equals(categoryDTO.Name, StringComparison.OrdinalIgnoreCase));

        if (category.Any())
        {
            return BadRequest("Categoria já cadastrada...");
        }

        var categoryDtoCreate = _categoryService.Add(categoryDTO);

        return new CreatedAtRouteResult("GetCategory", new {id = categoryDtoCreate.Id}, categoryDtoCreate);
    }

    [HttpPut]
    public ActionResult<CategoryDTO> Put(int id, [FromBody] CategoryDTO categoryDTO)
    {
        if (id != categoryDTO.Id)
        {
            return BadRequest("Dados inválidos...");
        }

        var categories = _categoryService.GetCategories();
        
        var category = categories
            .Where(c => c.Name.Equals(categoryDTO.Name, StringComparison.OrdinalIgnoreCase)
            && c.Id != categoryDTO.Id);

        if (category.Any())
        {
            return BadRequest("Categoria já cadastrada...");
        }

        var categoryDtoUpdate = _categoryService.Update(categoryDTO);

        return Ok(categoryDtoUpdate);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<CategoryDTO> Delete(int id)
    {
        var category = _categoryService.GetById(id);
        if (category == null)
        {
            return NotFound("Category not found");
        }

        var categoryDtoDelete = _categoryService.Remove(id);

        return Ok(categoryDtoDelete);
    }
}
