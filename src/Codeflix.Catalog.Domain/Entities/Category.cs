using Codeflix.Catalog.Domain.Exceptions;
using Codeflix.Catalog.Domain.SeedWork;

namespace Codeflix.Catalog.Domain.Entities;

public class Category : AggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Category(string name, string description, bool isActive = true)
    {
        Name = name;
        Description = description;
        IsActive = isActive;
        CreatedAt = DateTime.Now;

        Validate();
    }

    public void Activate()
    {
        IsActive = true;
        Validate();
    }
    
    public void Deactivate()
    {
        IsActive = false;
        Validate();
    }
    
    public void Update(string? name = null, string? description = null)
    {
        Name = name ?? Name;
        Description = description ?? Description;
        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new EntityValidationException($"{nameof(Name)} should not be empty or null");
        if (Name.Length < 3)
            throw new EntityValidationException($"{nameof(Name)} should be at least 3 characters long");
        if (Name.Length > 255)
            throw new EntityValidationException($"{nameof(Name)} should be less or equal to 255 characters long");
        if (Description == null)
            throw new EntityValidationException($"{nameof(Description)} should not be null");
        if (Description.Length > 10000)
            throw new EntityValidationException(
                $"{nameof(Description)} should be less or equal to 10000 characters long");
    }
}