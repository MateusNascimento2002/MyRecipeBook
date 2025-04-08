using MyRecipeBook.Domain.Security.Tokens.Refresh;
using MyRecipeBook.Infrastructure.Security.Tokens.Refresh;

namespace MyRecipeBook.API.Token;

public class RefreshTokenGeneratorBuilder
{
    public static IRefreshTokenGenerator Build() => new RefreshTokenGenerator();
}

