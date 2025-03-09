using AutoMapper;
using MyRecipeBook.Application.Extensions;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Domain.Services.Storage;

namespace MyRecipeBook.Application.UseCases.Dashboard;
public class GetDashboardUseCase(IRecipeReadOnlyRepository repository, IMapper mapper, ILoggedUser loggedUser, IBlobStorageService blobStorageService) : IGetDashboardUseCase
{
    private readonly ILoggedUser _loggedUser = loggedUser;

    public async Task<ResponseRecipesJson> Execute()
    {
        var loggedUser = await _loggedUser.User();

        var recipes = await repository.GetForDashboard(loggedUser);

        return new ResponseRecipesJson
        {
            Recipes = await recipes.MapToShortRecipeJson(loggedUser, blobStorageService, mapper)
        };
    }
}