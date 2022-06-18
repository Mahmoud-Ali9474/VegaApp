namespace Vega.API.Core.Models
{
    public class VehicleQuery : Query
    {
        public int? MakeId { get; set; }
        public int? ModelId { get; set; }
    }
}