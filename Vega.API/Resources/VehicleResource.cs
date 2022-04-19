namespace Vega.API.Resources
{
    public class VehicleResource
    {

        public int Id { get; set; }
        public KeyValuePairResource Model { get; set; }
        public KeyValuePairResource Make { get; set; }
        public bool IsRegistered { get; set; }
        public DateTime LastUpdate { get; set; }
        public IList<KeyValuePairResource> Features { get; set; }
        public ContactResource Contact { get; set; }

        public VehicleResource()
        {
            Features = new List<KeyValuePairResource>();
        }
    }
}