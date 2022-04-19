using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vega.API.Core;
using Vega.API.Core.Models;
using Vega.API.Resources;

namespace Vega.API.Controllers;

[ApiController]
[Route("api/vehicles")]
public class VehiclesController : ControllerBase
{
    private readonly ILogger<VehiclesController> _logger;

    private readonly IMapper _mapper;
    private readonly IVehicleRepository repository;
    private readonly IUnitOfWork uow;

    public VehiclesController(ILogger<VehiclesController> logger,
        IMapper mapper,
        IVehicleRepository repository,
        IUnitOfWork uow)
    {
        this.repository = repository;
        this.uow = uow;
        _mapper = mapper;
        _logger = logger;
    }
    // [HttpGet]
    // public async Task<IEnumerable<Vehicle>> GetVehicles()
    // {
    //     return await _context.Vehicles.ToListAsync();
    // }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetVehicle(int id)
    {
        var vehicle = await repository.GetVehicle(id);

        if (vehicle == null)
            return NotFound();

        var vehicleResource = _mapper.Map<Vehicle, VehicleResource>(vehicle);
        return Ok(vehicleResource);
    }

    [HttpPost]
    public async Task<IActionResult> CreateVehicle(SaveVehicleResource vehicleResource)
    {
        var vehicle = _mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
        vehicle.LastUpdate = DateTime.Now;

        await repository.Create(vehicle);
        await uow.CompeleteAsync();

        vehicle = await repository.GetVehicle(vehicle.Id);
        var result = _mapper.Map<Vehicle, VehicleResource>(vehicle);
        return Ok(result);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVehicle(int id, SaveVehicleResource vehicleResource)
    {
        var vehicle = await repository.GetVehicle(id, includeRelated: false);

        if (vehicle == null)
            return NotFound();

        _mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
        vehicle.LastUpdate = DateTime.Now;

        await repository.Update(vehicle);
        await uow.CompeleteAsync();

        vehicle = await repository.GetVehicle(vehicle.Id);
        var result = _mapper.Map<Vehicle, VehicleResource>(vehicle);
        return Ok(result);

    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehicleById(int id)
    {
        var vehicle = await repository.GetVehicle(id, includeRelated: false);
        if (vehicle == null)
            return NotFound();

        await repository.Delete(vehicle);
        await uow.CompeleteAsync();

        return Ok(id);
    }


}
