using Jonas.Darts.Domain.Models;

namespace Jonas.Darts.Data.Repositories.Interfaces;

public interface IAuthRepository
{
    Task<bool> UserNameExistsAsync(string username);
    Task<User?> GetUserByUsernameAsync(string username);
    Task AddUserAsync(User user);
    Task SaveChangesAsync();
}
