using Codeflix.Catalog.UnitTest.Common;

namespace Codeflix.Catalog.UnitTest.Domain.Entities.Category;

using Xunit;
using DomainEntity = Codeflix.Catalog.Domain.Entities;

public class CategoryTestFixture : BaseFixture
{
    public CategoryTestFixture() : base()
    {
    }

    public string GetValidName() => Faker.Random.String2(3, 255);

    public string GetValidDescription() => Faker.Random.String2(1, 10000);

    public DomainEntity.Category GetValidCategory() => new(
        GetValidName(),
        GetValidDescription());
}

[CollectionDefinition(nameof(CategoryTestFixture))]
public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture>
{
}