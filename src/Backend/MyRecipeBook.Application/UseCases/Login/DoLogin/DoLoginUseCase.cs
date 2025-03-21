﻿using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Exceptions.ExceptionBase;
using MyRecipeBook.Infrastructure.Security.Cryptography;

namespace MyRecipeBook.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase(IUserReadOnlyRepository repository,
                            IPasswordEncripter passwordEncripter,
                            IAccessTokenGenerator accessTokenGenerator) : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository = repository;
    private readonly IPasswordEncripter _passwordEncripter = passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator = accessTokenGenerator;

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var user = await _repository.GetByEmail(request.Email);

        if (user is null || _passwordEncripter.IsValid(request.Password, user.Password).IsFalse())
        {
            throw new InvalidLoginException();
        }

        return new ResponseRegisteredUserJson()
        {
            Name = user.Name,
            Tokens = new ResponseTokensJson()
            {
                AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }
}