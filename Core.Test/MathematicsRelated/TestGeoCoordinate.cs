using Core.Enums;
using Core.Extensions.MathematicsRelated;
using Core.Mathematics.Impl;
using Xunit;

namespace Core.Test.MathematicsRelated
{
    public class TestGeoCoordinate
    {
        private const decimal DecimalLatitudeOfBerlin = 52.518639M;
        private const decimal DecimalLongitudeOfBerlin = 13.376090M;


        [Fact]
        public void TestGeoLocationFactory()
        {
            var factory = new DefaultGeoLocationFactory();
            var geoBerlin = factory.Create(DecimalLatitudeOfBerlin, DecimalLongitudeOfBerlin);

            var latBerlin = geoBerlin.LatitudeDMS;
            var lonBerlin = geoBerlin.LongitudeDMS;
            
            Assert.Equal(52, latBerlin.Degrees);
            Assert.Equal(31, latBerlin.Minutes);
            Assert.Equal(7, latBerlin.Seconds);
            Assert.Equal(100, latBerlin.Milliseconds);
            
            Assert.Equal(13, lonBerlin.Degrees);
            Assert.Equal(22, lonBerlin.Minutes);
            Assert.Equal(33, lonBerlin.Seconds);
            Assert.Equal(924, lonBerlin.Milliseconds);
            
            Assert.Equal("N 52° 31' 07.1\"", geoBerlin.LatitudeDMS.WriteToString());
            Assert.Equal("E 13° 22' 33.9\"", geoBerlin.LongitudeDMS.WriteToString());
            geoBerlin.LongitudeDMS.WriteToString();
        }
        
    }
}