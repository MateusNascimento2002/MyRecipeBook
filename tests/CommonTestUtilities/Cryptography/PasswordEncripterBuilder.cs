using MyRecipeBook.Application.Cryptography;

namespace CommonTestUtilities.Cryptography;

public static class PasswordEncripterBuilder
{
    public static PasswordEncripter Build()
    {
        return new PasswordEncripter("123");
    }
}
