using System.Text;
using Core.Extensions.MathematicsRelated;
using Core.Mathematics.Impl;
using Xunit;

namespace Core.Test.MathematicsRelated
{
    public class GeoTest
    {
        private const double DecimalLatitudeOfBerlin  = 52.518639;
        private const double DecimalLongitudeOfBerlin = 13.376090;

        [Fact]
        public void BasicTest()
        {
            var fac = new GeoFactory();
            var berlinCircle = fac.CreateCircle(DecimalLatitudeOfBerlin, DecimalLongitudeOfBerlin, 500);
            
            var leftLink = berlinCircle.Left().ToGoogleMapsLink();
            var centerLink = berlinCircle.ToGoogleMapsLink();
            var rightLink = berlinCircle.Right().ToGoogleMapsLink();

            var topLink = berlinCircle.Top().ToGoogleMapsLink();
            var bottomLink = berlinCircle.Bottom().ToGoogleMapsLink();

            var sb = new StringBuilder();
            sb.AppendLine(leftLink);
            sb.AppendLine(centerLink);
            sb.AppendLine(rightLink);
            sb.AppendLine(topLink);
            sb.AppendLine(bottomLink);
        }
    }
}

