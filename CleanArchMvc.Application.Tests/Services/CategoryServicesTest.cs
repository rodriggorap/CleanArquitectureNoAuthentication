using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using Bogus;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Services;
using CleanArchMvc.Application.Test._Builders;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using Moq;

namespace CleanArchMvc.Application.Test.Services;

public class CategoryServicesTest
{
    private readonly Category _category;
    private readonly List<Category> _categories;
    private readonly CategoryDTO _categoryDTO;
    private readonly List<CategoryDTO> _categoriesDTO;
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
    private readonly CategoryService _categoryServicesMock;
    private readonly Mock<IMapper> _mockMapper;

    private readonly int _id;
    private readonly string _name;

    public CategoryServicesTest()
    {
        var fake = new Faker();

        _id = fake.Random.Int(10, 20);
        _name = fake.Random.String();

        _categoryDTO = new CategoryDTO{ Id =_id, Name = _name };

        _categoriesDTO = new List<CategoryDTO>
        {
            new CategoryDTO { Id = _id, Name = _name },
            new CategoryDTO { Id = _id, Name = _name }
        };

        _category = new Category(_id, _name);

        _categories = new List<Category>
        {
            CategoryBuilder.Novo().ComId(_id).ComNome(_name).Build(),
            CategoryBuilder.Novo().ComId(_id).ComNome(_name).Build()
        };

        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _mockMapper = new Mock<IMapper>();
        _categoryServicesMock = new CategoryService(_categoryRepositoryMock.Object, _mockMapper.Object);
    }

    [Fact]
    public void DeveRetornarUmaCategoria()
    { 

        _categoryRepositoryMock.Setup(r => r.GetById(_id)).Returns(_category);

        _mockMapper.Setup(m => m.Map<CategoryDTO>(_category)).Returns(_categoryDTO);

        var result = _categoryServicesMock.GetById(_id);

        Assert.Equal(_category.Id, result.Id);
        Assert.IsType<CategoryDTO>(result);
    }

    [Fact]
    public void DeveRetornarCategorias()
    {   
        _categoryRepositoryMock.Setup(r => r.GetCategories()).Returns(_categories);

        _mockMapper.Setup(m => m.Map<IEnumerable<CategoryDTO>>(_categories)).Returns(_categoriesDTO);

        var result = _categoryServicesMock.GetCategories();

        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<CategoryDTO>>(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public void DeveRetornarCategoriasPaginadas()
    {
        _categoryRepositoryMock.Setup(r => r.GetCategories("testes", 1, 10)).Returns(_categories);

        _mockMapper.Setup(m => m.Map<IEnumerable<CategoryDTO>>(_categories)).Returns(_categoriesDTO);

        var result = _categoryServicesMock.GetCategories("testes", 1, 10);

        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<CategoryDTO>>(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public void DeveAdicionarUmaCategoria()
    {
        _mockMapper.Setup(m => m.Map<Category>(_categoryDTO)).Returns(_category);

        _categoryRepositoryMock.Setup(r => r.Create(_category)).Returns(_category);

        _mockMapper.Setup(m => m.Map<CategoryDTO>(_category)).Returns(_categoryDTO);

        var result = _categoryServicesMock.Add(_categoryDTO);

        Assert.Equal(_category.Id, result.Id);
        Assert.IsType<CategoryDTO>(result);
    }

    [Fact]
    public void DeveAtualizarUmaCategoria()
    {
        _mockMapper.Setup(m => m.Map<Category>(_categoryDTO)).Returns(_category);

        _categoryRepositoryMock.Setup(r => r.Update(_category)).Returns(_category);

        _mockMapper.Setup(m => m.Map<CategoryDTO>(_category)).Returns(_categoryDTO);

        var result = _categoryServicesMock.Update(_categoryDTO);

        Assert.Equal(_category.Id, result.Id);
        Assert.IsType<CategoryDTO>(result);
    }

    [Fact]
    public void DeveRemoverUmaCategoria()
    {
        _categoryRepositoryMock.Setup(r => r.GetById(_categoryDTO.Id)).Returns(_category);

        _categoryRepositoryMock.Setup(r => r.Remove(_category)).Returns(_category);

        _mockMapper.Setup(m => m.Map<CategoryDTO>(_category)).Returns(_categoryDTO);

        var result = _categoryServicesMock.Remove(_categoryDTO.Id);

        Assert.Equal(_category.Id, result.Id);
        Assert.IsType<CategoryDTO>(result);
    }
}
