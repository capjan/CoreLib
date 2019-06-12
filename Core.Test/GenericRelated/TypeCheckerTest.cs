using Core.Generic;
using Xunit;

namespace Core.Test.GenericRelated
{
    public class TypeCheckerTest
    {
        [Fact]
        public void BasicTest()
        {
            bool IsStringOrInt<T>(T value)
            {
                var typeChecker = new TypeChecker<T>(typeof(string), typeof(int));
                return typeChecker.IsValid();
            }

            Assert.True(IsStringOrInt("this is a string"));
            Assert.False(IsStringOrInt(12.34));
            Assert.True(IsStringOrInt(1234));
        }
    }
}