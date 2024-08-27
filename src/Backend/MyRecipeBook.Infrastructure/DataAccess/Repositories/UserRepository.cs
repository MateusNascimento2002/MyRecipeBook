using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionBase;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
{
    private readonly MyRecipeBookDbContext _context;

    public UserRepository(MyRecipeBookDbContext context) => _context = context;

    public async Task Add(User user) => await _context.users.AddAsync(user);
    public async Task<bool> ExistActiveUserWithEmail(string email) => await _context.users.AnyAsync(u => u.Email.Equals(email) && u.Active);

    public async Task<User?> GetByEmailAndPassword(string email, string password)
    {
       return await _context.
                    users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Password.Equals(password) && u.Active);
    }

    public async Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier) => await _context.users.AnyAsync(u => u.UserIdentifier.Equals(userIdentifier) && u.Active);
    public async Task<User> GetByUserIdentifier(Guid userIdentifier) => await _context.users.AsNoTracking().FirstAsync(u => u.UserIdentifier.Equals(userIdentifier));
    public async Task<User> GetById(long id) => await _context.users.FirstOrDefaultAsync(u => u.Id == id) ?? throw new MyRecipeBookException("User not found!");
    public void Update(User user) => _context.users.Update(user);
}
