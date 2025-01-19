using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetProducts();
    Task<ProductDTO> GetById(int? id);
    Task<ProductDTO> GetProductCategory(int? id);
    Task<ProductDTO> Add(ProductDTO productDTO);
    Task<ProductDTO> Update(ProductDTO productDTO);
    Task<ProductDTO> Remove(int? id);
}
