using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using CleanArchMvc.Infra.Data.Repositories;
using CleanArchMvc.Infra.Data.Tests._Builders;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CleanArchMvc.Infra.Data.Tests.Repositories;

public class CategoryRepositoryTests
{
    private readonly Category _category;
    private readonly List<Category> _categories;
    private readonly Mock<DbSet<Category>> _mockCategorySet;
    private readonly Mock<ApplicationDbContext> _categoryContextMock;
    private readonly CategoryRepository _categoryRepositoryMock;

    private readonly int _id;
    private readonly string _name;

    public CategoryRepositoryTests()
    {
        var fake = new Faker();

        _id = fake.Random.Int(10, 20);
        _name = fake.Random.String();

        _category = new Category(_id, _name);
        _categories = new List<Category>
        {
            CategoryBuilder.Novo().ComId(_id).ComNome(_name).Build(),
            CategoryBuilder.Novo().ComId(_id).ComNome(_name).Build()
        };

        _mockCategorySet = new Mock<DbSet<Category>>();
        _categoryContextMock = new Mock<ApplicationDbContext>();

        _categoryContextMock.Setup(c => c.Set<Category>()).Returns(_mockCategorySet.Object);

        _categoryRepositoryMock = new CategoryRepository(_categoryContextMock.Object);
    }

    [Fact]
    public async Task DeveCriarUmaCategoria()
    {
        _categoryContextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var result = await _categoryRepositoryMock.Create(_category);

        // Assert
        _mockCategorySet.Verify(m => m.Add(It.IsAny<Category>()), Times.Once); 
        _categoryContextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once); 
        Assert.Equal(_category, result); 
    }
}
