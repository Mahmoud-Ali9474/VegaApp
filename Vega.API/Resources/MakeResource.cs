namespace Vega.API.Resources
{
    public class MakeResource : KeyValuePairResource
    {
        public List<KeyValuePairResource> Models { get; set; }

        public MakeResource()
        {
            Models = new List<KeyValuePairResource>();
        }

    }
}