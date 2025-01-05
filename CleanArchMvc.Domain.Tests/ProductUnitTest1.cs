using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Entities;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests;

public class ProductUnitTest1
{
    [Fact]
    public void CreateProduct_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 10.95m, 5, "Product Imagem");
        action.Should()
            .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () => new Product(-1, "Product Name", "Product Description", 10.95m, 5, "Product Imagem");
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid Id value.");
    }

    [Fact]
    public void CreateProduct_ShortName_DomainExceptionShortName()
    {
        Action action = () => new Product(1, "Pr", "Product Description", 10.95m, 5, "Product Imagem");
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name, too short, minimum 3 characters");
    }

    [Fact]
    public void CreateProduct_LongImageName_DomainExceptionLongImage()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 10.95m, 5, "Product Imagem Product Imagem Product Imagem");
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid image name, too long, maximum 20 characters");
    }

    [Fact]
    public void CreateProduct_WithNullImageName_DomainException()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 10.95m, 5, null);
        action.Should()
            .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_WithNullImageName_ReferenceException()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 10.95m, 5, null);
        action.Should()
            .NotThrow<NullReferenceException>();
    }

    [Theory]
    [InlineData(-5)]
    public void CreateProduct_InvalidStockValue_DomainException(int value)
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 10.95m, value, "product image");
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid stock value");
    }
}
