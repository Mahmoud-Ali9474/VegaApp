namespace Vega.API.Resources
{
    public class SaveVehicleResource
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public bool IsRegistered { get; set; }
        public ContactResource Contact { get; set; }
        public IList<int> Features { get; set; }
        public SaveVehicleResource()
        {
            Features = new List<int>();
        }

    }
}