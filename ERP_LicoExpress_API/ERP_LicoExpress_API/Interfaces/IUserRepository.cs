using ERP_LicoExpress_API.Models;

namespace ERP_LicoExpress_API.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> CreateAsync(User user);
        public Task<User> GetByCorreo(string correo);
        public Task<User> GetByID(int user_id);
        public Task<bool> CreateSessionAsync(Session sesion);
    }
}
