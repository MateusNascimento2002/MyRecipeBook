using MyRecipeBook.Application.Cryptography;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Exceptions.ExceptionBase;

namespace MyRecipeBook.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase(IUserReadOnlyRepository repository, 
                            PasswordEncripter passwordEncripter,
                            IAccessTokenGenerator accessTokenGenerator) : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository = repository;
    private readonly PasswordEncripter _passwordEncripter = passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator = accessTokenGenerator;

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var encriptedPassword = _passwordEncripter.Encrypt(request.Password);
        var user = await _repository.GetByEmailAndPassword(request.Email, encriptedPassword) ?? throw new InvalidLoginException();

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