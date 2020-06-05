using System;
using System.Collections.Generic;
using System.Text;
using Core.Extensions.MathematicsRelated;
using Core.Mathematics.Impl;
using Xunit;

namespace Core.Test.TextRelated
{
    public class GeoLocationFormatterTest
    {
        [Fact]
        public void BasicTest()
        {
            const double latitudeOfBerlin  = 52.518639;
            const double longitudeOfBerlin = 13.376090;

            var locationFactory = new GeoFactory();
            var locationOfBerlin = locationFactory.CreateLocation(latitudeOfBerlin, longitudeOfBerlin);

            var googleMapsLink = locationOfBerlin.ToGoogleMapsLink();
            var bingLink = locationOfBerlin.ToBingMapsLink();
            var openStreetMapsLink = locationOfBerlin.ToOpenStreetMapsLink();

            Assert.NotEmpty(googleMapsLink);
            Assert.NotEmpty(bingLink);
            Assert.NotEmpty(openStreetMapsLink);
        }
    }
}
