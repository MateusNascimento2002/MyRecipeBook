using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    private readonly MyRecipeBookDbContext _context;

    public UserRepository(MyRecipeBookDbContext context) => _context = context;

    public async Task Add(User user) => await _context.users.AddAsync(user);
    public async Task<bool> ExistActiveUserWithEmail(string email) => await _context.users.AnyAsync(u => u.Email.Equals(email) && u.Active);

}
