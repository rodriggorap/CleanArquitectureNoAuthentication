using CleanArchMvc.Domain.Entities;
using ExpectedObjects;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests;

public class MarkUnitTes1
{
    [Fact]
    public void Mark_WithValidParameters_ResultObjectValidState()
    {
        var createCategory = new
        {
            Id = 1,
            Name = "Name",
        };

        var mark = new Mark(createCategory.Id, createCategory.Name);

        createCategory.ToExpectedObject().ShouldMatch(mark);
    }

    [Fact]
    public void Mark_NegativeIdValue_DomainExceptionInvalidId()
    {
        var message = Assert.Throws<ArgumentException>(() =>
            new Mark(-1, "Category Name")).Message;

        Assert.Equal("Invalid Id value", message);
    }

    [Fact]
    public void Mark_ShortNameValue_DomainExceptionShortName()
    {
        var message = Assert.Throws<ArgumentException>(() =>
            new Mark(1, "Ca")).Message;

        Assert.Equal("Invalid name, too shorts, minimum 3 characters", message);
    }

    [Fact]
    public void Mark_MissingNameValue_DomainExceptionRequiredName()
    {
        var message = Assert.Throws<ArgumentException>(() =>
            new Mark(1, "")).Message;

        Assert.Equal("Invalid name. Name is required", message);
    }

    [Fact]
    public void Mark_WithNullNameValue_DomainExceptionInvalidName()
    {
        var message = Assert.Throws<ArgumentException>(() =>
            new Mark(1, null)).Message;

        Assert.Equal("Invalid name. Name is required", message);
    }
}