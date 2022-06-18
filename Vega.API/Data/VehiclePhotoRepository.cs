using Microsoft.EntityFrameworkCore;
using Vega.API.Core;
using Vega.API.Core.Models;
using Vega.API.Resources;

namespace Vega.API.Data
{
    class VehiclePhotoRepository : IVehiclePhotoRepository
    {
        private readonly VegaDbContext context;

        public VehiclePhotoRepository(VegaDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId)
        {
            return await context.Photos
            .Where(p => p.VehicleId == vehicleId)
            .ToListAsync();
        }
    }
}