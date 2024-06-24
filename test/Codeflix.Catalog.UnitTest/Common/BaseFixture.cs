using Bogus;

namespace Codeflix.Catalog.UnitTest.Common;

public abstract class BaseFixture
{
    public Faker Faker { get; set; }
    
    protected BaseFixture() => Faker = new Faker("pt_BR");
}