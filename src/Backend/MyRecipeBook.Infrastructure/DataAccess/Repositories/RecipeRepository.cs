using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Dtos;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Domain.Repositories.Recipe;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories;

public class RecipeRepository(MyRecipeBookDbContext context) : IRecipeWriteOnlyRepository, IRecipeReadOnlyRepository
{
    private readonly MyRecipeBookDbContext _context = context;

    public async Task Add(Recipe recipe) => await _context.recipes.AddAsync(recipe);

    public async Task<IList<Recipe>> Filter(User user, FilterRecipesDto filters)
    {
       var query = _context
            .recipes
            .Include(r => r.Ingredients)
            .AsNoTracking()
            .Where(r => r.Active && r.UserId == user.Id );

        if (filters.Difficulties.Any())
        {
            query = query.Where(q => q.Difficulty.HasValue && filters.Difficulties.Contains(q.Difficulty.Value));
        }

        if (filters.CookingTimes.Any())
        {
            query = query.Where(q => q.CookingTime.HasValue && filters.CookingTimes.Contains(q.CookingTime.Value));
        }

        if (filters.DishTypes.Any())
        {
            query = query.Where(q => q.DishTypes.Any(d => filters.DishTypes.Contains(d.Type)));
        }

        if(filters.RecipeTitle_Ingredient.NotEmpty())
        {
            query = query.Where(r => r.Title.Contains(filters.RecipeTitle_Ingredient) || 
                                r.Ingredients.Any(i => i.Item.Contains(filters.RecipeTitle_Ingredient)));
        }

        return await query.ToListAsync();
    }
}
