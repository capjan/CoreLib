using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Mathematics;

namespace Core.Text.Formatter
{
    public class OpenStreetMapsLinkFormatter : ITextFormatter<IGeoLocation>
    {
        public int ZoomLevel { get; set; } = 17;

        public void Write(IGeoLocation value, TextWriter writer)
        {
            var lat = value.Latitude.ToString(CultureInfo.InvariantCulture);
            var lon = value.Longitude.ToString(CultureInfo.InvariantCulture);
            writer.Write($"http://www.openstreetmap.org/?mlat={lat}&mlon={lon}&zoom={ZoomLevel}");
        }
    }
    
}
