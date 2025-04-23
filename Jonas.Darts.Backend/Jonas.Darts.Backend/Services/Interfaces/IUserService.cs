using Jonas.Darts.Domain.Models;

namespace Jonas.Darts.Backend.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
    }
}
