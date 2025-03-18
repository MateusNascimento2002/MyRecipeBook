using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionBase;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository, IUserDeleteOnlyRepository
{
    private readonly MyRecipeBookDbContext _context;

    public UserRepository(MyRecipeBookDbContext context) => _context = context;

    public async Task Add(User user) => await _context.Users.AddAsync(user);
    public async Task<bool> ExistActiveUserWithEmail(string email) => await _context.Users.AnyAsync(u => u.Email.Equals(email) && u.Active);

    public async Task<User?> GetByEmailAndPassword(string email, string password)
    {
        return await _context.
                     Users
                     .AsNoTracking()
                     .FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Password.Equals(password) && u.Active);
    }

    public async Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier) => await _context.Users.AnyAsync(u => u.UserIdentifier.Equals(userIdentifier) && u.Active);
    public async Task<User> GetByUserIdentifier(Guid userIdentifier) => await _context.Users.AsNoTracking().FirstAsync(u => u.UserIdentifier.Equals(userIdentifier));
    public async Task<User> GetById(long id) => await _context.Users.FirstOrDefaultAsync(u => u.Id == id) ?? throw new NotFoundException("User not found!");
    public void Update(User user) => _context.Users.Update(user);

    public async Task DeleteAccount(Guid userIdentifier)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.UserIdentifier == userIdentifier);
        if (user is null)
            return;

        var recipes = _context.Recipes.Where(recipe => recipe.UserId == user.Id);

        _context.Recipes.RemoveRange(recipes);

        _context.Users.Remove(user);
    }
}
