using Sqids;

namespace CommonTestUtilities.IdEncryption;

public class IdEncripterBuilder
{
    public static SqidsEncoder<long> Build()
    {
        return new SqidsEncoder<long>(new()
        {
            MinLength = 3,
            Alphabet = "M704FZcIJuCDGryVKR2s3qQYwlTENgASLhvOXizoHe5k1f8paWU9PbmBdtj6nx"
        });
    }
}