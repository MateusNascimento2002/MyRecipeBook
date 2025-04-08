using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.Token;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories;

public class TokenRepository(MyRecipeBookDbContext context) : ITokenRepository
{
    public async Task<RefreshToken?> Get(string refreshToken)
    {
        return await context
            .RefreshTokens
            .AsNoTracking()
            .Include(token => token.User)
            .FirstOrDefaultAsync(token => token.Value.Equals(refreshToken));
    }

    public async Task SaveNewRefreshToken(RefreshToken refreshToken)
    {
        var tokens = context.RefreshTokens.Where(token => token.UserId == refreshToken.UserId);

        context.RefreshTokens.RemoveRange(tokens);

        await context.RefreshTokens.AddAsync(refreshToken);
    }
}
