using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
    {
        var products = await _productService.GetProducts();

        return Ok(products.OrderBy(c => c.Name));
    }

    [HttpGet("{id:int}", Name = "GetProduct")]
    public async Task<ActionResult<ProductDTO>> Get(int id)
    {
        var product = await _productService.GetById(id);

        if (product == null)
        {
            return NotFound("Category not found");
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDTO>> Post([FromBody] ProductDTO productDTO)
    {
        var products = await _productService.GetProducts();

        var product = products
            .Where(p => p.Name.Equals(productDTO.Name, StringComparison.OrdinalIgnoreCase));

        if (product.Any())
        {
            return BadRequest("Categoria já cadastrada...");
        }

        var productCreateDto = await _productService.Add(productDTO);

        return new CreatedAtRouteResult("GetProduct", new {id = productCreateDto.Id}, productCreateDto);
    }

    [HttpPut]
    public async Task<ActionResult<ProductDTO>> Put(int id, [FromBody] ProductDTO productDTO)
    {
        if (id != productDTO.Id)
        {
            return BadRequest("Dados inválidos...");
        }

        var products = await _productService.GetProducts();
      
        var product = products
            .Where(p => p.Name.Equals(productDTO.Name, StringComparison.OrdinalIgnoreCase)
            && p.Id != productDTO.Id);

        if (product.Any())
        {
            return BadRequest("Produto já cadastrado...");
        }

        var productUpdateDto = await _productService.Update(productDTO);

        return Ok(productUpdateDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ProductDTO>> Delete(int id)
    {
        var product = await _productService.GetById(id);
        if (product == null)
        {
            return NotFound();
        }

        var productRemoveDto = await _productService.Remove(id);

        return Ok(productRemoveDto);
    }
}
