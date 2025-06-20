﻿using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Domain.Services.Storage;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionBase;

namespace MyRecipeBook.Application.UseCases.Recipe.Delete;

public class DeleteRecipeUseCase(ILoggedUser loggedUser, IRecipeReadOnlyRepository repositoryRead, IRecipeWriteOnlyRepository repositoryWrite, IUnitOfWork unitOfWork, IBlobStorageService blobStorageService) : IDeleteRecipeUseCase
{
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IRecipeReadOnlyRepository _repositoryRead = repositoryRead;
    private readonly IRecipeWriteOnlyRepository _repositoryWrite = repositoryWrite;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(long recipeId)
    {
        var loggedUser = await _loggedUser.User();

        var recipe = await _repositoryRead.GetById(loggedUser, recipeId) ?? throw new NotFoundException(ResourceMessagesException.RECIPE_NOT_FOUND);

        if (recipe.ImageIdentifier.NotEmpty())
            await blobStorageService.Delete(loggedUser, recipe.ImageIdentifier);
        

        await _repositoryWrite.Delete(recipeId);

        await _unitOfWork.Commit();
    }
}
