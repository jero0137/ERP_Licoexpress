﻿using ERP_LicoExpress_API.Interfaces;
using ERP_LicoExpress_API.Models;
using ERP_LicoExpress_API.Repositories;
using GestionTransporte_CS_API_PostgresSQL.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

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

        //Funcion de servicio para hacer validaciones para el login
        public async Task<Session> LoginAsync(User user)
        {
            var userExistente = await _userRepository.GetByCorreo(user.Correo);
            //Validamos que el user exista
            if(userExistente.Id     == 0)
                throw new AppValidationException($"Usuario no encontrado");
            //Validamos que la contraseña falta
            //Hay que cifrar la contraseña proximamente
            if(userExistente.Contrasena != user.Contrasena)
                throw new AppValidationException($"La constraseña del usuario no coincide");

            //LLenamos los datos de la session
            //Por ahora el token es el hashcode de la clase
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

            return session;
        }

        public async Task<User> Authenticate(User user)
        {
            // Buscar al usuario por su nombre de usuario en el repositorio
            var userExistente = await _userRepository.GetByCorreo(user.Correo);

            // Verificar si el usuario existe y si la contraseña es válida
            if (userExistente.Id == 0)
                throw new AppValidationException($"Usuario no encontrado");
            if(userExistente.Contrasena != user.Contrasena)
                throw new AppValidationException($"Las constraseñas no coinciden");
            // Las credenciales son incorrectas
            return userExistente;
        }

        public string GenerateToken(User user)
        {
            var secretKey = GenerateKey(32);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey); // Reemplaza por tu clave secreta
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Correo),
                    new Claim(ClaimTypes.Role, user.Rol) // Agregar rol como claim
                    // Puedes incluir más claims según las necesidades (identificador de usuario, etc.)
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Configura la expiración del token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public static string GenerateKey(int keySizeInBytes)
        {
            using (var generator = RandomNumberGenerator.Create())
            {
                var keyBytes = new byte[keySizeInBytes];
                generator.GetBytes(keyBytes);
                return Convert.ToBase64String(keyBytes);
            }
        }
    }
}
