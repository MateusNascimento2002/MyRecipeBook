namespace MyRecipeBook.Domain.Security.Tokens.Refresh
{
    public interface IRefreshTokenGenerator
    {
        string Generate();
    }
}