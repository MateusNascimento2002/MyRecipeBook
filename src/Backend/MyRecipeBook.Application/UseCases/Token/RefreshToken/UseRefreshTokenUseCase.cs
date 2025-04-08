using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.Token;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Domain.Security.Tokens.Refresh;
using MyRecipeBook.Domain.ValueObjects;
using MyRecipeBook.Exceptions.ExceptionBase;

namespace MyRecipeBook.Application.UseCases.Token.RefreshToken;

public class UseRefreshTokenUseCase(
    IUnitOfWork unitOfWork,
    ITokenRepository tokenRepository,
    IRefreshTokenGenerator refreshTokenGenerator,
    IAccessTokenGenerator accessTokenGenerator) : IUseRefreshTokenUseCase
{
    public async Task<ResponseTokensJson> Execute(RequestNewTokenJson request)
    {
        var refreshToken = await tokenRepository.Get(request.RefreshToken);

        if (refreshToken is null)
            throw new RefreshTokenNotFoundException();

        var refreshTokenValidUntil = refreshToken.CreatedAt.AddDays(MyRecipeBookRuleConstants.REFRESH_TOKEN_EXPIRATION_DAYS);
        if (DateTime.Compare(refreshTokenValidUntil, DateTime.UtcNow) < 0)
            throw new RefreshTokenExpiredException();

        var newRefreshToken = new Domain.Entities.RefreshToken
        {
            Value = refreshTokenGenerator.Generate(),
            UserId = refreshToken.UserId
        };

        await tokenRepository.SaveNewRefreshToken(newRefreshToken);

        await unitOfWork.Commit();

        return new ResponseTokensJson
        {
            AccessToken = accessTokenGenerator.Generate(refreshToken.User.UserIdentifier),
            RefreshToken = newRefreshToken.Value
        };
    }
}