﻿using CommonTestUtilities.BlobStorage;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using MyRecipeBook.Application.UseCases.Recipe.Filter;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionBase;

namespace UsesCases.Test.Recipe.Filter;

public class FilterRecipeUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, _) = UserBuilder.Build();
        var request = RequestFilterRecipeJsonBuilder.Build();
        var recipes = RecipeBuilder.Collection(user);
        var useCase = CreateUseCase(user, recipes);
        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Recipes.Should().NotBeEmpty();
        result.Recipes.Should().HaveCount(recipes.Count);
    }

    [Fact]
    public async Task Error_CookingTime_Invalid()
    {
        (var user, _) = UserBuilder.Build();

        var recipes = RecipeBuilder.Collection(user);

        var request = RequestFilterRecipeJsonBuilder.Build();
        request.CookingTimes.Add((MyRecipeBook.Communication.Enums.CookingTime)100);


        var useCase = CreateUseCase(user, recipes);

        Func<Task> act = async () => { await useCase.Execute(request); };

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.GetErrorMessages().Count == 1 &&
                e.GetErrorMessages().Contains(ResourceMessagesException.COOKING_TIME_NOT_SUPPORTED));
    }

    private static FilterRecipeUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user, IList<MyRecipeBook.Domain.Entities.Recipe> recipes)
    {
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var repository = new RecipeReadOnlyRepositoryBuilder().Filter(user, recipes).Build();
        var blobStorage = new BlobStorageServiceBuilder().GetFileUrl(user, recipes).Build();

        return new FilterRecipeUseCase(mapper, loggedUser, repository, blobStorage);
    }
}
