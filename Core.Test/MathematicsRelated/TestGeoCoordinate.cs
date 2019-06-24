using System;
using Core.Enums;
using Core.Extensions.MathematicsRelated;
using Core.Mathematics.Impl;
using Xunit;

namespace Core.Test.MathematicsRelated
{
    public class TestGeoCoordinate
    {
        private const double DecimalLatitudeOfBerlin = 52.518639;
        private const double DecimalLongitudeOfBerlin = 13.376090;


        [Fact]
        public void TestGeoLocationFactory()
        {
            var factory = new GeoLocationFactory();
            var geoBerlin = factory.Create(DecimalLatitudeOfBerlin, DecimalLongitudeOfBerlin);

            var latBerlin = geoBerlin.LatitudeDMS;
            var lonBerlin = geoBerlin.LongitudeDMS;
            
            Assert.Equal(52, latBerlin.Degrees);
            Assert.Equal(31, latBerlin.Minutes);
            Assert.Equal(7.1, Math.Round(latBerlin.Seconds, 3, MidpointRounding.AwayFromZero));
            
            Assert.Equal(13, lonBerlin.Degrees);
            Assert.Equal(22, lonBerlin.Minutes);
            Assert.Equal(33.924, Math.Round(lonBerlin.Seconds, 3, MidpointRounding.AwayFromZero));
            
            Assert.Equal("N 52° 31' 07.1\"", geoBerlin.LatitudeDMS.WriteToString());
            Assert.Equal("E 13° 22' 33.9\"", geoBerlin.LongitudeDMS.WriteToString());
            geoBerlin.LongitudeDMS.WriteToString();
        }
        
    }
}