using AutoMapper;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Domain.Services.Storage;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionBase;

namespace MyRecipeBook.Application.UseCases.Recipe.GetById;

public class GetRecipeByIdUseCase(IMapper mapper, ILoggedUser loggedUser, IRecipeReadOnlyRepository repository, IBlobStorageService blobStorageService) : IGetRecipeByIdUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IRecipeReadOnlyRepository _repository = repository;

    public async Task<ResponseRecipeJson> Execute(long recipeId)
    {
        var user = await _loggedUser.User();

        var recipe = await _repository.GetById(user, recipeId);


        if (recipe is null)
        {
            throw new NotFoundException(ResourceMessagesException.RECIPE_NOT_FOUND);
        }

        var response = _mapper.Map<ResponseRecipeJson>(recipe);

        if (recipe.ImageIdentifier.NotEmpty())
        {
            var url = await blobStorageService.GetFileUrl(user, recipe.ImageIdentifier);

            response.ImageUrl = url;
        }

        return response;
    }
}
