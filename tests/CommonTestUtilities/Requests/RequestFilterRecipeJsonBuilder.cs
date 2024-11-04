using Bogus;
using MyRecipeBook.Communication.Enums;
using MyRecipeBook.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestFilterRecipeJsonBuilder
{
    public static RequestFilterRecipeJson Build(int passwordLenght = 10)
    {
        return new Faker<RequestFilterRecipeJson>()
            .RuleFor(user => user.CookingTimes, (f) => f.Make(1, () => f.PickRandom<CookingTime>()))
            .RuleFor(user => user.Difficulties, (f) => f.Make(1, () => f.PickRandom<Difficulty>()))
            .RuleFor(user => user.DishTypes, (f) => f.Make(1, () => f.PickRandom<DishType>()))
            .RuleFor(user => user.RecipeTitle_Ingredient, f => f.Lorem.Word());
    }
}