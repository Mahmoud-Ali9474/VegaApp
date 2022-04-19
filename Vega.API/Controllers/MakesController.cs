using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.API.Data;
using Vega.API.Core.Models;
using Vega.API.Resources;

namespace Vega.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MakesController : ControllerBase
{
    private readonly ILogger<MakesController> _logger;
    private readonly VegaDbContext _context;
    private readonly IMapper _mapper;

    public MakesController(ILogger<MakesController> logger, VegaDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
        _logger = logger;
    }

    [HttpGet("/api/makes")]
    public async Task<IEnumerable<MakeResource>> GetMakes()
    {
        var makes = await _context.Makes.Include(e => e.Models).ToListAsync();

        return _mapper.Map<List<Make>, List<MakeResource>>(makes);
    }

    [HttpGet("/api/features")]
    public async Task<IEnumerable<KeyValuePairResource>> GetFeatures()
    {
        var features = await _context.Features.ToListAsync();

        return _mapper.Map<List<Feature>, List<KeyValuePairResource>>(features);
    }
}
