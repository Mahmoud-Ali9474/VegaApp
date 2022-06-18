using Microsoft.EntityFrameworkCore;
using Vega.API.Core;
using Vega.API.Core.Models;
using System.Linq.Expressions;
using Vega.API.Extensions;

namespace Vega.API.Data
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext context;
        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;

        }
        public async Task<QueryResult<Vehicle>> GetVehiclesAsync(VehicleQuery queryObj)
        {
            var result = new QueryResult<Vehicle>();
            var query = context.Vehicles
            .Include(v => v.Model)
                .ThenInclude(m => m.Make).AsQueryable();

            if (queryObj.MakeId.HasValue)
                query = query.Where(v => v.Model.Make.Id == queryObj.MakeId);
            if (queryObj.ModelId.HasValue)
                query = query.Where(v => v.Model.Id == queryObj.ModelId);

            result.TotalCount = await query.CountAsync();
            var dictionary = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName,
            };
            query = query.ApplyOrdering(queryObj, dictionary);
            query = query.ApplyPaging(queryObj);

            result.PagedList = await query.ToListAsync();
            return result;
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