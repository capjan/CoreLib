using System;
using System.Linq.Expressions;
using Core.Extensions.LinqRelated;
using Xunit;

namespace Core.Test.LinqRelated
{
    public class PropertyNameExtractionTest
    {
        private class Person
        {
            // ReSharper disable UnusedAutoPropertyAccessor.Local
            public string Name { get; set; }
            public int Age { get; set; }
            // ReSharper restore UnusedAutoPropertyAccessor.Local
        }

        private class PropertyDumper<T>
        {
            public string Dump<TProperty>(Expression<Func<T, TProperty>> expression)
            {
                var memberInfo = expression.GetMember();
                var name = memberInfo.Name;
                var type = typeof(TProperty);

                return $"{type.Name} {name}";
            }
        }

        [Fact]
        public void TestMapper()
        {

            var dumper = new PropertyDumper<Person>();
            var dump1 = dumper.Dump(p=>p.Name);
            var dump2 = dumper.Dump(p=>p.Age);

            Assert.Equal("String Name", dump1);
            Assert.Equal("Int32 Age", dump2);

        }
    }
}
