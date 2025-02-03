using Bogus;
using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Test._Builders;

public class CategoryDTOBuilder
{
    private int _id = 0;
    private string _nome = "Nome";

    public static CategoryDTOBuilder Novo()
    {
        return new CategoryDTOBuilder();
    }
    public CategoryDTOBuilder ComId(int id)
    {
        _id = id;
        return this;
    }
    public CategoryDTOBuilder ComNome(string nome)
    {
        _nome = nome;
        return this;
    }
    public CategoryDTO Build()
    {
        return new CategoryDTO { Id = _id, Name = _nome };
    }

}
