using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MyRecipeBook.Domain.Dtos;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Domain.Repositories.Recipe;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories;

public class RecipeRepository(MyRecipeBookDbContext context) : IRecipeWriteOnlyRepository, IRecipeReadOnlyRepository, IRecipeUpdateOnlyRepository
{
    private readonly MyRecipeBookDbContext _context = context;

    public async Task Add(Recipe recipe) => await _context.recipes.AddAsync(recipe);

    public async Task<IList<Recipe>> Filter(User user, FilterRecipesDto filters)
    {
        var query = _context
             .recipes
             .Include(r => r.Ingredients)
             .AsNoTracking()
             .Where(r => r.Active && r.UserId == user.Id);

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

        if (filters.RecipeTitle_Ingredient.NotEmpty())
        {
            query = query.Where(r => r.Title.Contains(filters.RecipeTitle_Ingredient) ||
                                r.Ingredients.Any(i => i.Item.Contains(filters.RecipeTitle_Ingredient)));
        }

        return await query.ToListAsync();
    }

    async Task<Recipe?> IRecipeReadOnlyRepository.GetById(User user, long recipeId)
    {
        return await GetFullRecipe().AsNoTracking().FirstOrDefaultAsync(recipe => recipe.Active && recipe.Id == recipeId && recipe.UserId == user.Id);
    }
    async Task<Recipe?> IRecipeUpdateOnlyRepository.GetById(User user, long recipeId)
    {
        return await GetFullRecipe().FirstOrDefaultAsync(recipe => recipe.Active && recipe.Id == recipeId && recipe.UserId == user.Id);
    }

    public void Update(Recipe recipe) => _context.recipes.Update(recipe);

    private IIncludableQueryable<Recipe, IList<DishType>> GetFullRecipe()
    {
        return _context
           .recipes
           .Include(recipe => recipe.Ingredients)
           .Include(recipe => recipe.Instructions)
           .Include(recipe => recipe.DishTypes);
    }
    public async Task Delete(long recipeId)
    {
        var recipe = await _context.recipes.FindAsync(recipeId);
        _context.recipes.Remove(recipe!);
    }

    public async Task<IList<Recipe>> GetForDashboard(User user)
    {
        return await _context
            .recipes
            .AsNoTracking()
            .Include(recipe => recipe.Ingredients)
            .Where(recipe => recipe.Active && recipe.UserId == user.Id)
            .OrderByDescending(r => r.CreatedAt)
            .Take(5)
            .ToListAsync();
    }
}
