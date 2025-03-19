using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Tokens;

namespace MyRecipeBook.Application.UseCases.Login.External;

public class ExternalLoginUseCase(
    IUserReadOnlyRepository repository,
    IUserWriteOnlyRepository repositoryWrite,
    IUnitOfWork unitOfWork,
    IAccessTokenGenerator accessTokenGenerator) : IExternalLoginUseCase
{
    public async Task<string> Execute(string name, string email)
    {
        var user = await repository.GetByEmail(email);

        if (user is null)
        {
            user = new Domain.Entities.User
            {
                Name = name,
                Email = email,
                Password = "-"
            };

            await repositoryWrite.Add(user);
            await unitOfWork.Commit();
        }

        return accessTokenGenerator.Generate(user.UserIdentifier);
    }
}
