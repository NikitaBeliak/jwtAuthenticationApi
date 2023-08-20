using Domain.Models;

namespace Domain.Entitys.Interfaces
{
    public interface IUserRepository
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);

    }
}
