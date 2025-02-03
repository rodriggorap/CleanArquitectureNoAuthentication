using Bogus;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Application.Test._Builders;

public class CategoryBuilder
{
    private int _id = 0;
    private string _nome = "Nome";

    public static CategoryBuilder Novo()
    {
        return new CategoryBuilder();
    }
    public CategoryBuilder ComId(int id)
    {
        _id = id;
        return this;
    }
    public CategoryBuilder ComNome(string nome)
    {
        _nome = nome;
        return this;
    }
    public Category Build()
    {
        return new Category(_id, _nome);
    }

}
