using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.Token;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Domain.Security.Tokens.Refresh;
using MyRecipeBook.Exceptions.ExceptionBase;
using MyRecipeBook.Infrastructure.Security.Cryptography;

namespace MyRecipeBook.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase(IUserReadOnlyRepository repository,
                            IPasswordEncripter passwordEncripter,
                            IAccessTokenGenerator accessTokenGenerator,
                            IRefreshTokenGenerator refreshTokenGenerator,
                            IUnitOfWork unitOfWork,
                            ITokenRepository tokenRepository) : IDoLoginUseCase
{
    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var user = await repository.GetByEmail(request.Email);

        if (user is null || passwordEncripter.IsValid(request.Password, user.Password).IsFalse())
        {
            throw new InvalidLoginException();
        }

        var refreshToken = await CreateAndSaveRefreshToken(user);

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Tokens = new ResponseTokensJson
            {
                AccessToken = accessTokenGenerator.Generate(user.UserIdentifier),
                RefreshToken = refreshToken
            }
        };
    }

    private async Task<string> CreateAndSaveRefreshToken(Domain.Entities.User user)
    {
        var refreshToken = new Domain.Entities.RefreshToken
        {
            Value = refreshTokenGenerator.Generate(),
            UserId = user.Id
        };

        await tokenRepository.SaveNewRefreshToken(refreshToken);

        await unitOfWork.Commit();

        return refreshToken.Value;
    }
}