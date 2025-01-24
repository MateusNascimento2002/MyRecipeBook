using AutoMapper;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionBase;

namespace MyRecipeBook.Application.UseCases.Recipe.GetById;

public class GetRecipeByIdUseCase(IMapper mapper, ILoggedUser loggedUser, IRecipeReadOnlyRepository repository) : IGetRecipeByIdUseCase
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

        return _mapper.Map<ResponseRecipeJson>(recipe);
    }
}
