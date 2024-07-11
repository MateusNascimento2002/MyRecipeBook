using Microsoft.Extensions.Configuration;

namespace MyRecipeBook.Infrastructure.Extensions;

public static class ConfigureExtension
{
    public static string ConnectionString(this IConfiguration configuration)
    {
        return configuration.GetConnectionString("ConnectionSQLServer")!;
    }

    public static bool IsUnitTestEnviroment(this IConfiguration configuration)
    {
        return configuration.GetValue<bool>("InMemoryTest");
    }
}
