namespace MyRecipeBook.Infrastructure.Security.Cryptography;

public interface IPasswordEncripter
{
    public string Encrypt(string password);
}
