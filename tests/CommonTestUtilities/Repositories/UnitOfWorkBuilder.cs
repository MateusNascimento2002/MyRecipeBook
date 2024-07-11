using Moq;
using MyRecipeBook.Domain.Repositories;

namespace CommonTestUtilities.Repositories;

public static class UnitOfWorkBuilder
{
    public static IUnitOfWork Build()
    {
        var mock = new Mock<IUnitOfWork>();
        return mock.Object;
    }
}
