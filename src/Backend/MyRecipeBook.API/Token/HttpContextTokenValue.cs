using MyRecipeBook.Domain.Security.Tokens;

namespace MyRecipeBook.API.Token;

public class HttpContextTokenValue(IHttpContextAccessor httpContextAccessor) : ITokenProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public string Value()
    {
        var authorization = _httpContextAccessor.HttpContext!.Request.Headers.Authorization.ToString();
        return authorization["Bearer ".Length..].Trim();
    }
}
