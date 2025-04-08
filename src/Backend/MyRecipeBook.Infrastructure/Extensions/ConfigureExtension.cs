using Microsoft.Extensions.Configuration;
using MyRecipeBook.Domain.Enums;

namespace MyRecipeBook.Infrastructure.Extensions;

public static class ConfigureExtension
{
    public static string ConnectionString(this IConfiguration configurarion)
    {
        var databaseType = configurarion.DatabaseType();

        if (databaseType == Domain.Enums.DatabaseType.MySql)
            return configurarion.GetConnectionString("ConnectionMySQLServer")!;
        else
            return configurarion.GetConnectionString("ConnectionSQLServer")!;
    }
    public static DatabaseType DatabaseType(this IConfiguration configurarion)
    {
        var databaseType = configurarion.GetConnectionString("DatabaseType");

        return (DatabaseType)Enum.Parse(typeof(DatabaseType), databaseType!);
    }

    public static bool IsUnitTestEnviroment(this IConfiguration configuration)
    {
        return configuration.GetValue<bool>("InMemoryTest");
    }
}
