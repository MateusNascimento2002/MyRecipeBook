using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Exceptions.ExceptionBase;
using MyRecipeBook.Exceptions;
using Microsoft.AspNetCore.Http;
using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Domain.Services.Storage;
using MyRecipeBook.Application.Extensions;

namespace MyRecipeBook.Application.UseCases.Recipe.Image;

public class AddUpdateImageCoverUseCase(
    ILoggedUser loggedUser,
    IRecipeUpdateOnlyRepository repository,
    IUnitOfWork unitOfWork,
    IBlobStorageService blobStorageService) : IAddUpdateImageCoverUseCase
{
    private readonly ILoggedUser _loggedUser = loggedUser;

    public async Task Execute(long recipeId, IFormFile file)
    {
        var loggedUser = await _loggedUser.User();

        var recipe = await repository.GetById(loggedUser, recipeId);

        if (recipe is null)
            throw new NotFoundException(ResourceMessagesException.RECIPE_NOT_FOUND);

        var fileStream = file.OpenReadStream();

        (var isValidImage, var extension) = fileStream.ValidateAndGetImageExtension();

        if (isValidImage.IsFalse())
        {
            throw new ErrorOnValidationException([ResourceMessagesException.ONLY_IMAGES_ACCEPTED]);
        }

        if (string.IsNullOrEmpty(recipe.ImageIdentifier))
        {
            recipe.ImageIdentifier = $"{Guid.NewGuid()}{extension}";

            repository.Update(recipe);

            await unitOfWork.Commit();
        }

        fileStream.Position = 0;
        await blobStorageService.Upload(loggedUser, fileStream, recipe.ImageIdentifier);
    }
}