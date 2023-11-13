using ERP_LicoExpress_API.Interfaces;
using ERP_LicoExpress_API.Models;
using GestionTransporte_CS_API_PostgresSQL.Helpers;

namespace ERP_LicoExpress_API.Services
{
    public class LocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            return await _locationRepository
                .GetAllAsync();
        }


        public async Task<Location> GetByIdAsync(int location_id)
        {
            var unaLocation = await _locationRepository
                .GetByIdAsync(location_id);

            if (unaLocation.Id == 0)
                throw new AppValidationException($"Sede no encontrada con el id {location_id}");

            return unaLocation;
        }


        public async Task DeleteAsync(int location_id)
        {
            // validamos que la sede a eliminar si exista con ese Id
            var locationExistente = await _locationRepository.GetByIdAsync(location_id);

            if (locationExistente.Id == 0)
                throw new AppValidationException($"No existe una sede con el Id {location_id} que se pueda eliminar");

            try
            {
                bool resultadoAccion = await _locationRepository
                    .DeleteAsync(locationExistente);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la DB");
            }
            catch (DbOperationException error)
            {
                throw error;
            }
        }


        public async Task<Location> CreateAsync(Location unaLocation)
        {
            //var locationExistente = await _locationRepository.GetByNameAsync(unaLocation.Nombre);

            //if (locationExistente.Nombre.Equals(unaLocation.Nombre))
              //  throw new AppValidationException($"Ya existe una sede con el nombre {unaLocation.Nombre} ");


            if (unaLocation.Nombre.Length == 0)
                throw new AppValidationException("No se puede insertar una sede sin un nombre");

            if (unaLocation.Direccion.Length == 0)
                throw new AppValidationException("No se puede insertar una sede sin una dirección");

            if (unaLocation.Nombre_admin.Length == 0)
                throw new AppValidationException("No se puede insertar una sede sin un administrador");

            if (unaLocation.Contacto_admin.Length == 0)
                throw new AppValidationException("No se puede insertar una sede sin un contacto");

            if (unaLocation.Telefono_admin.Length == 0)
                throw new AppValidationException("No se puede insertar una sede sin un teléfono");

            if (unaLocation.Ciudad.Length == 0)
                throw new AppValidationException("No se puede insertar una sede sin un contacto");

            try
            {
                bool resultadoAccion = await _locationRepository.CreateAsync(unaLocation);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la DB");

                var locationExistente = await _locationRepository.GetByIdAsync(unaLocation.Id);

                return locationExistente;

            }
            catch (DbOperationException error)
            {
                throw error;
            }


            /*var locationExistente = await _locationRepository
                    .GetByIdAsync(unaLocation.Id);

            return (locationExistente);*/

        }

    }
}
