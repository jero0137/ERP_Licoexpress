using ERP_LicoExpress_API.Interfaces;
using ERP_LicoExpress_API.Models;
using GestionTransporte_CS_API_PostgresSQL.Helpers;

namespace ERP_LicoExpress_API.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetById(int user_id)
        {
            //Validamos que exista un bus con ese Id
            var user = await _userRepository.GetByID(user_id);

            if (user.Id == 0)
                throw new AppValidationException($"Usuario no encontrado con el id {user_id}");

            return user;
        }

        public async Task<string> LoginAsync(User user)
        {
            var userExistente = await _userRepository.GetByCorreo(user.Correo);

            if(userExistente.Id == 0)
                throw new AppValidationException($"Usuario no encontrado");

            if(userExistente.Contrasena != user.Contrasena)
                throw new AppValidationException($"La constraseña del usuario no coincide");

            Session session = new Session();

            session.Token = $"{userExistente.GetHashCode()}";
            session.Fecha_creacion = DateTime.Now;
            session.Fecha_terminacion = DateTime.Now;
            try
            {
                bool resultadoAccion = await _userRepository.CreateSessionAsync(session);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la DB");

            }
            catch (DbOperationException error)
            {
                throw error;
            }

            return session.Token;
        }


    }
}
