using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.API.Data;
using Vega.API.Models;

namespace Vega.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MakeController : ControllerBase
{
    private readonly ILogger<MakeController> _logger;
    private readonly VegaDbContext _context;

    public MakeController(ILogger<MakeController> logger, VegaDbContext context)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("/api/makes")]
    public IEnumerable<Make> GetMakes()
    {

        return _context.Makes.Include(e => e.Models);
    }

    [HttpGet("/api/features")]
    public IEnumerable<Feature> GetFeatures()
    {
        // return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        // {
        //     Date = DateTime.Now.AddDays(index),
        //     TemperatureC = Random.Shared.Next(-20, 55),
        //     Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        // })
        // .ToArray();
        return _context.Features;
    }
}
