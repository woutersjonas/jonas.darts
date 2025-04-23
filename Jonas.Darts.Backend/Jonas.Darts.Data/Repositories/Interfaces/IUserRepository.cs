using Jonas.Darts.Domain.Models;

namespace Jonas.Darts.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
