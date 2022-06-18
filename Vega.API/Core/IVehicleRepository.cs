using Vega.API.Core.Models;

namespace Vega.API.Core
{
    public interface IVehicleRepository
    {
        Task<QueryResult<Vehicle>> GetVehiclesAsync(VehicleQuery query);
        Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
        Task Create(Vehicle vehicle);
        Task Update(Vehicle vehicle);
        Task Delete(Vehicle vehicle);

    }
}