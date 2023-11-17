using ERP_LicoExpress_API.Models;

namespace ERP_LicoExpress_API.Interfaces
{
    public interface ILocationRepository
    {
        public Task<IEnumerable<Location>> GetAllAsync();
        public Task<Location> GetByIdAsync(int id);
        public Task<Location> GetByNameAsync(string name);
        public Task<bool> DeleteAsync(Location unaLocation);
        public Task<bool> CreateAsync(Location unaLocation);
        public Task<bool> UpdateAsync(int location_id, Location unaLocation);
    }
}
