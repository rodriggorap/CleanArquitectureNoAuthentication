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

public class ProductServiceTest
{
    private readonly Product _product;
    private readonly List<Product> _products;
    private readonly ProductDTO _productDTO;
    private readonly List<ProductDTO> _productsDTO;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly ProductService _productServicesMock;
    private readonly Mock<IMapper> _mockMapper;

    private readonly int _id;
    private readonly string _name;
    private readonly string _description;
    private readonly decimal _price;
    private readonly string _image;
    private readonly int _stock;

    public ProductServiceTest()
    {
        var fake = new Faker();

        _id = fake.Random.Int(10, 15);
        _name = fake.Random.String(10, 15);
        _description = fake.Random.String(10, 15);
        _price = fake.Random.Decimal(10, 15);
        _image = fake.Random.String(10, 15);
        _stock = fake.Random.Int(10, 15);

        _productDTO = new ProductDTO { Id = _id, Name = _name, Description = _description, Price = _price, Image = _image, Stock = _stock };

        _productsDTO = new List<ProductDTO>
        {
            new ProductDTO { Id = _id, Name = _name, Description = _description, Price = _price, Image = _image, Stock = _stock },
            new ProductDTO { Id = _id, Name = _name, Description = _description, Price = _price, Image = _image, Stock = _stock }
        };

        _product = new Product(_id, _name, _description, _price, _stock, _image);

        _products = new List<Product>
        {
            new Product(_id, _name, _description, _price, _stock, _image),
            new Product(_id, _name, _description, _price, _stock, _image)
        };

        _productRepositoryMock = new Mock<IProductRepository>();
        _mockMapper = new Mock<IMapper>();
        _productServicesMock = new ProductService(_productRepositoryMock.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task DeveRetornarUmProduto()
    {

        _productRepositoryMock.Setup(r => r.GetById(_id)).ReturnsAsync(_product);

        _mockMapper.Setup(m => m.Map<ProductDTO>(_product)).Returns(_productDTO);

        var result = await _productServicesMock.GetById(_id);

        Assert.Equal(_product.Id, result.Id);
        Assert.IsType<ProductDTO>(result);
    }

    [Fact]
    public async Task DeveRetornarVariosProdutos()
    {

        _productRepositoryMock.Setup(r => r.GetProduts()).ReturnsAsync(_products);

        _mockMapper.Setup(m => m.Map<IEnumerable<ProductDTO>>(_products)).Returns(_productsDTO);

        var result = await _productServicesMock.GetProducts();

        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProductDTO>>(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task DeveRetornarUmProdutoPelaCategoria()
    {

        _productRepositoryMock.Setup(r => r.GetProductCategory(_id)).ReturnsAsync(_product);

        _mockMapper.Setup(m => m.Map<ProductDTO>(_product)).Returns(_productDTO);

        var result = await _productServicesMock.GetProductCategory(_id);

        Assert.Equal(_product.Id, result.Id);
        Assert.IsType<ProductDTO>(result);
    }

    [Fact]
    public async Task DeveAdicionarUmProduto()
    {
        _mockMapper.Setup(m => m.Map<Product>(_productDTO)).Returns(_product);

        _productRepositoryMock.Setup(r => r.Create(_product)).ReturnsAsync(_product);

        _mockMapper.Setup(m => m.Map<ProductDTO>(_product)).Returns(_productDTO);

        var result = await _productServicesMock.Add(_productDTO);

        Assert.Equal(_product.Id, result.Id);
        Assert.IsType<ProductDTO>(result);
    }

    [Fact]
    public async Task DeveAtualizarUmProduto()
    {
        _mockMapper.Setup(m => m.Map<Product>(_productDTO)).Returns(_product);

        _productRepositoryMock.Setup(r => r.Update(_product)).ReturnsAsync(_product);

        _mockMapper.Setup(m => m.Map<ProductDTO>(_product)).Returns(_productDTO);

        var result = await _productServicesMock.Update(_productDTO);

        Assert.Equal(_product.Id, result.Id);
        Assert.IsType<ProductDTO>(result);
    }

    [Fact]
    public async Task DeveRemoverUmProduto()
    {

        _productRepositoryMock.Setup(r => r.GetById(_id)).ReturnsAsync(_product);

        _productRepositoryMock.Setup(r => r.Remove(_product)).ReturnsAsync(_product);

        _mockMapper.Setup(m => m.Map<ProductDTO>(_product)).Returns(_productDTO);

        var result = await _productServicesMock.Remove(_id);

        Assert.Equal(_product.Id, result.Id);
        Assert.IsType<ProductDTO>(result);
    }
}
