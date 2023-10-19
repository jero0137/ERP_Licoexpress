using ERP_LicoExpress_API.Interfaces;

namespace ERP_LicoExpress_API.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


    }
}
