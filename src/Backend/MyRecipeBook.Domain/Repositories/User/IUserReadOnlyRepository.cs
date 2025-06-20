﻿


namespace MyRecipeBook.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    public Task<bool> ExistActiveUserWithEmail(string email);
    Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier);
    Task<Entities.User?> GetByEmail(string email);
    Task<Entities.User> GetByUserIdentifier(Guid userIdentifier);
}
