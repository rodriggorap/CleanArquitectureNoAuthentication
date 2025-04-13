using Bogus;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Application.Test._Builders;

public class CategoryBuilder
{
    private readonly Faker faker;

    private int _id;
    private string _nome;

    private CategoryBuilder()
    {
        faker = new();
        _id = faker.Random.Int(10, 20);
        _nome = faker.Random.Word();
    }

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
