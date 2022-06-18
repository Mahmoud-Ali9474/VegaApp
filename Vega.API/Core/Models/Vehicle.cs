using System.ComponentModel.DataAnnotations;

namespace Vega.API.Core.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public bool IsRegistered { get; set; }
        public DateTime LastUpdate { get; set; }
        public IList<Feature> Features { get; set; }
        public IList<Photo> Photos { get; set; }

        [Required]
        [StringLength(255)]
        public string ContactName { get; set; }
        [Required]
        [StringLength(255)]
        public string ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
        public Vehicle()
        {
            Features = new List<Feature>();
            Photos = new List<Photo>();
        }
    }
}