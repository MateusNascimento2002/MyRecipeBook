
namespace MyRecipeBook.Domain.Repositories.Recipe;

public interface IRecipeUpdateOnlyRepository
{
    void Update(Entities.Recipe recipe);
    Task<Entities.Recipe?> GetById(Entities.User user, long recipeId);
}
