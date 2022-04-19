using Microsoft.EntityFrameworkCore;
using Vega.API.Core;
using Vega.API.Core.Models;

namespace Vega.API.Data
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext context;
        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;

        }
        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Vehicles
                .Include(v => v.Features)
                .SingleOrDefaultAsync(v => v.Id == id);

            return await context.Vehicles
                    .Include(v => v.Features)
                    .Include(v => v.Model)
                        .ThenInclude(m => m.Make)
                    .SingleOrDefaultAsync(v => v.Id == id);

        }

        public async Task Create(Vehicle vehicle)
        {
            var selectedFeatures = vehicle.Features.Select(fr => fr.Id);
            vehicle.Features = await context.Features
                .Where(f => selectedFeatures.Contains(f.Id))
                .ToListAsync();
            context.Vehicles.Add(vehicle);
        }
        public async Task Update(Vehicle vehicle)
        {
            var selectedFeatures = vehicle.Features.Select(fr => fr.Id);
            vehicle.Features = await context.Features
                .Where(f => selectedFeatures.Contains(f.Id))
                .ToListAsync();
        }

        public async Task Delete(Vehicle vehicle)
        {
            if (vehicle == null) return;

            context.Vehicles.Remove(vehicle);
        }
    }
}