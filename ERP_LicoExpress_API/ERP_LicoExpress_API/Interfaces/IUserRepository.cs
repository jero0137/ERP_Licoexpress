using ERP_LicoExpress_API.Models;

namespace ERP_LicoExpress_API.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> CreateAsync(User user);
        public Task<User> GetByCorreoAsync(string correo);
        public Task<User> GetByIdAsync(int user_id);
    }
}
