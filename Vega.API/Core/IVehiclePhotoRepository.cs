using Vega.API.Core.Models;

namespace Vega.API.Core
{
    public interface IVehiclePhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhotos(int vehicleId);
    }
}