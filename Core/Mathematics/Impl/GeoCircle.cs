using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mathematics.Impl
{
    class GeoCircle: GeoLocation, IGeoCircle
    {
        public double Radius { get; }
        public double MinLatitude { get; }
        public double MaxLatitude { get; }
        public double MinLongitude { get; }
        public double MaxLongitude { get; }

        internal GeoCircle(double latitude, double longitude, IGeoCoordinate latitudeDMS, IGeoCoordinate longitudeDMS, double radius, double minLatitude, double maxLatitude, double minLongitude, double maxLongitude) : base(latitude, longitude, latitudeDMS, longitudeDMS)
        {
            Radius = radius;
            MinLatitude = minLatitude;
            MaxLatitude = maxLatitude;
            MinLongitude = minLongitude;
            MaxLongitude = maxLongitude;
        }
     
    }
}
