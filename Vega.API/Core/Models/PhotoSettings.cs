using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.API.Core.Models
{
    public class PhotoSettings
    {
        public int MaxBytes { get; set; }
        public string[] SupportedFileTypes { get; set; }

        public bool IsSupportedType(string fileName)
        {
            return SupportedFileTypes.Any(t => t == Path.GetExtension(fileName).ToLower());
        }
    }
}