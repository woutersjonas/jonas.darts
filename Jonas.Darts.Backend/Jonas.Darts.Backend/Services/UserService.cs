using Jonas.Darts.Backend.Services.Interfaces;
using Jonas.Darts.Data.Repositories.Interfaces;
using Jonas.Darts.Domain.Models;

namespace Jonas.Darts.Backend.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }
}
