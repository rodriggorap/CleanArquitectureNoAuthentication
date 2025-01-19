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

public class ProductService : IProductService
{
    private IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository ?? 
            throw new ArgumentNullException(nameof(productRepository));

        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDTO>> GetProducts()
    {
        var productEntity = await _productRepository.GetProduts();
        return _mapper.Map<IEnumerable<ProductDTO>>(productEntity);
    }

    public async Task<ProductDTO> GetById(int? id)
    {
        var productEntity = await _productRepository.GetById(id);
        return _mapper.Map<ProductDTO>(productEntity);
    }

    public async Task<ProductDTO> GetProductCategory(int? id)
    {
        var productCategoryEntity = await _productRepository.GetProductCategory(id);
        return _mapper.Map<ProductDTO>(productCategoryEntity);
    }

    public async Task<ProductDTO> Add(ProductDTO productDTO)
    {
        var productEntity = _mapper.Map<Product>(productDTO);
        productEntity = await _productRepository.Create(productEntity);
        return _mapper.Map<ProductDTO>(productEntity);
    }

    public async Task<ProductDTO> Update(ProductDTO productDTO)
    {
        var productEntity = _mapper.Map<Product>(productDTO);
        productEntity = await _productRepository.Update(productEntity);
        return _mapper.Map<ProductDTO>(productEntity);
    }

    public async Task<ProductDTO> Remove(int? id)
    {
        var productEntity = await _productRepository.GetById(id);
        productEntity = await _productRepository.Remove(productEntity);
        return _mapper.Map<ProductDTO>(productEntity);
    }
}
