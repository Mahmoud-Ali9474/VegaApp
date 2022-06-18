using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.API.Core.Models
{
    public class Photo
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}