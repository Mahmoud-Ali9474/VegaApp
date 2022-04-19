namespace Vega.API.Core.Models
{
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Vehicle> Vehicles { get; set; }

        public Feature()
        {
            Vehicles = new List<Vehicle>();
        }
    }
}