

namespace MyRecipeBook.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    public Task<bool> ExistActiveUserWithEmail(string email);
    Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier);
    public Task<Entities.User?> GetByEmailAndPassword(string email, string password);
    Task<Entities.User> GetByUserIdentifier(Guid userIdentifier);
}
