using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities;

public sealed class Mark : Entity
{
    public string? Name { get; private set; }
    public ICollection<Product>? Products { get; set; }

    public Mark(string name)
    {
        if (string.IsNullOrEmpty(name)) 
            throw new ArgumentException("Invalid name. Name is required");

        if (name.Length < 3)
            throw new ArgumentException("Invalid name, too shorts, minimum 3 characters");

        Name = name;
    }

    public Mark(int id, string name)
    {
        if (id < 0)
            throw new ArgumentException("Invalid Id value");

        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Invalid name. Name is required");

        if (name.Length < 3)
            throw new ArgumentException("Invalid name, too shorts, minimum 3 characters");
        
        Id = id;
        Name = name;
    }

    public void Update(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Invalid name. Name is required");

        if (name.Length < 3)
            throw new ArgumentException("Invalid name, too shorts, minimum 3 characters");

        Name = name;
    }

}
