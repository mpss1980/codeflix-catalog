using Codeflix.Catalog.Domain.Exceptions;
using Xunit;
using DomainEntity = Codeflix.Catalog.Domain.Entities;

namespace Codeflix.Catalog.UnitTest.Domain.Entities.Category;

public class CategoryTest
{
    [Fact(DisplayName = nameof(Instantiate))]
    public void Instantiate()
    {
        var validData = new
        {
            Name = "Category Name",
            Description = "Category Description"
        };
        var datetimeBefore = DateTime.Now;

        var category = new DomainEntity.Category(validData.Name, validData.Description);
        var datetimeAfter = DateTime.Now.AddSeconds(100);

        Assert.NotNull(category);
        Assert.Equal(validData.Name, category.Name);
        Assert.Equal(validData.Description, category.Description);
        Assert.NotEqual(default(Guid), category.Id);
        Assert.NotEqual(default(DateTime), category.CreatedAt);
        Assert.True(category.CreatedAt > datetimeBefore);
        Assert.True(category.CreatedAt < datetimeAfter);
        Assert.True(category.IsActive);
    }

    [Theory(DisplayName = nameof(InstantiateWithIsActiveStatus))]
    [InlineData(true)]
    [InlineData(false)]
    public void InstantiateWithIsActiveStatus(bool isActive)
    {
        var validData = new
        {
            Name = "Category Name",
            Description = "Category Description"
        };
        var datetimeBefore = DateTime.Now;

        var category = new DomainEntity.Category(validData.Name, validData.Description, isActive);
        var datetimeAfter = DateTime.Now.AddSeconds(100);

        Assert.NotNull(category);
        Assert.Equal(validData.Name, category.Name);
        Assert.Equal(validData.Description, category.Description);
        Assert.NotEqual(default(Guid), category.Id);
        Assert.NotEqual(default(DateTime), category.CreatedAt);
        Assert.True(category.CreatedAt > datetimeBefore);
        Assert.True(category.CreatedAt < datetimeAfter);
        Assert.Equal(isActive, category.IsActive);
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsInvalid))]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("    ")]
    public void InstantiateErrorWhenNameIsInvalid(string? name)
    {
        Action action = () => new DomainEntity.Category(name!, "Category Description");
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name should not be empty or null", exception.Message);
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsNull))]
    public void InstantiateErrorWhenDescriptionIsNull()
    {
        Action action = () => new DomainEntity.Category("Category Name", null!);
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Description should not be null", exception.Message);
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameLengthIsSmallerThan3))]
    [InlineData("A")]
    [InlineData("AB")]
    public void InstantiateErrorWhenNameLengthIsSmallerThan3(string? invalidName)
    {
        Action action = () => new DomainEntity.Category(invalidName!, "Category Description");
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name should be at least 3 characters long", exception.Message);
    }
    
    [Fact(DisplayName = nameof(InstantiateErrorWhenNameLengthIsGreaterThan255))]
    public void InstantiateErrorWhenNameLengthIsGreaterThan255()
    {
        var invalidName = String.Join(null, Enumerable.Range(1, 256).Select(_ => "A").ToArray());
        Action action = () => new DomainEntity.Category(invalidName, "Category Description");
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name should be less or equal to 255 characters long", exception.Message);
    }
    
    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionLengthIsGreaterThan10K))]
    public void InstantiateErrorWhenDescriptionLengthIsGreaterThan10K()
    {
        var invalidDescription = String.Join(null, Enumerable.Range(1, 10001).Select(_ => "A").ToArray());
        Action action = () => new DomainEntity.Category("Category Name", invalidDescription);
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Description should be less or equal to 10000 characters long", exception.Message);
    }
}