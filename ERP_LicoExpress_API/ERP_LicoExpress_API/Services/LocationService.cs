using ERP_LicoExpress_API.Interfaces;
using ERP_LicoExpress_API.Models;
using ERP_LicoExpress_API.Helpers;

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


        }

        public async Task<Location> UpdateAsync(int location_id, Location unaLocation)
        {
            

            var locationExistente = await _locationRepository
                .GetByIdAsync(location_id);

            if (locationExistente.Id == 0)
                throw new AppValidationException($"No existe un autobus registrado con el id {unaLocation.Id}");

            if (location_id != locationExistente.Id)
                throw new AppValidationException($"Inconsistencia en el Id de la sede a actualizar. Verifica argumentos");


            if (string.IsNullOrEmpty(locationExistente.Contacto_admin))
                throw new AppValidationException("No se puede actualizar un contacto nulo");

            if (string.IsNullOrEmpty(locationExistente.Direccion))
                throw new AppValidationException("No se puede actualizar una dirección nula");

            if (string.IsNullOrEmpty(locationExistente.Nombre))
                throw new AppValidationException("No se puede actualizar un nombre nulo");

            if (string.IsNullOrEmpty(locationExistente.Ciudad))
                throw new AppValidationException("No se puede actualizar una ciudad nula");

            if (string.IsNullOrEmpty(locationExistente.Telefono_admin))
                throw new AppValidationException("No se puede actualizar un telefono nulo");


            //Validamos que haya al menos un cambio en las propiedades
            if (unaLocation.Equals(locationExistente))
                throw new AppValidationException("No hay cambios en los atributos de la sede. No se realiza actualización.");

            try
            {
                bool resultadoAccion = await _locationRepository
                    .UpdateAsync(location_id,unaLocation);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la DB");

                locationExistente = await _locationRepository
                    .GetByIdAsync(unaLocation.Id!);
            }
            catch (DbOperationException error)
            {
                throw error;
            }

            return locationExistente;

        }

    }
}
