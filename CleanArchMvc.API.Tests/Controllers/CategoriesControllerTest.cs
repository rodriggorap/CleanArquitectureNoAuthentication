using Bogus;
using System.Security.Cryptography;
using CleanArchMvc.API.Controllers;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Moq;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using CleanArchMvc.Application.Test._Builders;

namespace CleanArchMvc.API.Tests.Controllers;

public class CategoriesControllerTest
{
    private readonly CategoryDTO _categoryDTO;
    private readonly List<CategoryDTO> _categoriesDTO;
    private readonly Mock<ICategoryService> _mockCategoryService;
    private readonly CategoriesController _categoriesController;

    public CategoriesControllerTest()
    {
        _mockCategoryService = new Mock<ICategoryService>();
        _categoriesController = new CategoriesController( _mockCategoryService.Object );

        _categoryDTO = CategoryDTOBuilder.Novo().Build();

        _categoriesDTO = new List<CategoryDTO>
        {
            CategoryDTOBuilder.Novo().Build(),
            CategoryDTOBuilder.Novo().Build()
        };
    }

    [Fact]
    public void Get_DeveRetornarCategorias()
    {
        // Arrange
        _mockCategoryService.Setup(s => s.GetCategories()).Returns(_categoriesDTO);

        // Act
        var result = _categoriesController.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result); // Verifica se o resultado é um OkObjectResult
        var returnValue = Assert.IsAssignableFrom<IEnumerable<CategoryDTO>>(okResult.Value); // Verifica o tipo do valor retornado
        Assert.Equal(2, returnValue.Count()); // Verifica se o número de categorias é o esperado
    }

    [Fact]
    public void Get_ReturnsOk_DeveRetornarCategoriasPaginadas()
    {
        // Arrange
        int pagina = 1;
        int tamanho = 2;
        string nome = "teste";

        _mockCategoryService.Setup(s => s.GetCategories(nome, pagina, tamanho)).Returns(_categoriesDTO);

        // Act
        var result = _categoriesController.Get(nome, pagina, tamanho);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result); 
        var returnValue = Assert.IsAssignableFrom<IEnumerable<CategoryDTO>>(okResult.Value); 
        Assert.Equal(2, returnValue.Count());
    }

    [Fact]
    public void Get_ReturnsOk_DeveRetornarUmaCategoria()
    {
        //Arrange
        _mockCategoryService.Setup(s => s.GetById(_categoryDTO.Id)).Returns(_categoryDTO);

        // Act
        var result = _categoriesController.Get(_categoryDTO.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsAssignableFrom<CategoryDTO>(okResult.Value);
        Assert.Equal(_categoryDTO.Id, returnValue.Id);
    }

    [Fact]
    public void Get_ReturnsNotFound_NãoDeveRetornarUmaCategoria()
    {
        //Arrange
        _mockCategoryService.Setup(s => s.GetById(_categoryDTO.Id)).Returns((CategoryDTO)null);

        // Act
        var result = _categoriesController.Get(_categoryDTO.Id);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal("Category not found", notFoundResult.Value);
    }

    [Fact]
    public void Post_ReturnsCreatedAtRoute_DeveCadastrarUmaCategoria()
    {
        //Arrange
        var categories = new List<CategoryDTO>();

        _mockCategoryService.Setup(s => s.GetCategories()).Returns(categories);
        _mockCategoryService.Setup(s => s.Add(_categoryDTO)).Returns(_categoryDTO);

        // Act
        var result = _categoriesController.Post(_categoryDTO);

        // Assert
        var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result.Result);
        Assert.Equal("GetCategory", createdAtRouteResult.RouteName);
        Assert.Equal(_categoryDTO.Id, ((CategoryDTO)createdAtRouteResult.Value).Id);
        Assert.Equal(_categoryDTO.Name, ((CategoryDTO)createdAtRouteResult.Value).Name);
    }

    [Fact]
    public void Post_ReturnsBadRequest_CategoriaJaCadastrada()
    {
        //Arrange
        _mockCategoryService.Setup(s => s.GetCategories()).Returns(_categoriesDTO);

        // Act
        var result = _categoriesController.Post(_categoryDTO);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Categoria já cadastrada...", badRequestResult.Value);
    }

    [Fact]
    public void Put_ReturnsBadRequest_DadosInvalidos()
    {
        //Arrange
        int Id = 1;

        // Act
        var result = _categoriesController.Put(Id, _categoryDTO);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Dados inválidos...", badRequestResult.Value);
    }

    [Fact]
    public void Put_ReturnsBadRequest_CategoriaJaCadastrada()
    {
        //Arrange
        int id = 5;
        var category = CategoryDTOBuilder.Novo().ComId(id).Build();
        _mockCategoryService.Setup(s => s.GetCategories()).Returns(_categoriesDTO);

        // Act
        var result = _categoriesController.Put(category.Id, category);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Categoria já cadastrada...", badRequestResult.Value);
    }

    [Fact]
    public void Put_ReturnsOk_DeveAtualizarCategoria()
    {
        //Arrange
        _mockCategoryService.Setup(s => s.GetCategories()).Returns(_categoriesDTO);
        _mockCategoryService.Setup(s => s.Update(_categoryDTO)).Returns(_categoryDTO);

        // Act
        var result = _categoriesController.Put(_categoryDTO.Id, _categoryDTO);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsAssignableFrom<CategoryDTO>(okResult.Value);
        Assert.Equal(_categoryDTO.Id, returnValue.Id);
    }
    /*
    [Fact]
    public async Task Delete_ReturnsOk_DeveRemoverUmaCategoria()
    {
        //Arrange
        _mockCategoryService.Setup(s => s.GetById(_categoryDTO.Id)).ReturnsAsync(_categoryDTO);
        _mockCategoryService.Setup(s => s.Remove(_categoryDTO.Id)).ReturnsAsync(_categoryDTO);

        // Act
        var result = await _categoriesController.Delete(_categoryDTO.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsAssignableFrom<CategoryDTO>(okResult.Value);
        Assert.Equal(_categoryDTO.Id, returnValue.Id);
    }
    */
    [Fact]
    public void Delete_ReturnsOk_DeveRemoverUmaCategoria()
    {
        //Arrange
        _mockCategoryService.Setup(s => s.GetById(_categoryDTO.Id)).Returns(_categoryDTO);
        _mockCategoryService.Setup(s => s.Remove(_categoryDTO.Id)).Returns(_categoryDTO);

        // Act
        var result = _categoriesController.Delete(_categoryDTO.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsAssignableFrom<CategoryDTO>(okResult.Value);
        Assert.Equal(_categoryDTO.Id, returnValue.Id);
    }

    [Fact]
    public void Delete_ReturnsNotFound_CategoriaNaoEncontrada()
    {
        //Arrange
        _mockCategoryService.Setup(s => s.GetById(_categoryDTO.Id)).Returns((CategoryDTO)null);

        // Act
        var result = _categoriesController.Get(_categoryDTO.Id);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal("Category not found", notFoundResult.Value);
    }
}