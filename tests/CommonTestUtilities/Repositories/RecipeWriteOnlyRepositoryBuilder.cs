using Moq;
using MyRecipeBook.Domain.Repositories.Recipe;

namespace CommonTestUtilities.Repositories
{
    public static class RecipeWriteOnlyRepositoryBuilder
    {
        public static IRecipeWriteOnlyRepository Build()
        {
            var mock = new Mock<IRecipeWriteOnlyRepository>();
            return mock.Object;
        }
    }
}