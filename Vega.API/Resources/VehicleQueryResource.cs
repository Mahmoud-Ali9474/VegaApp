namespace Vega.API.Resources
{
    public class VehicleQueryResource : QueryResource
    {
        public int? MakeId { get; set; }
        public int? ModelId { get; set; }
    }
}