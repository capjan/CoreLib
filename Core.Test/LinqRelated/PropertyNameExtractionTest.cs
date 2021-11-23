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
            public Person(string name, int age)
            {
                Name = name;
                Age = age;
            }

            public Person()
            {
                Name = "";
                Age = 0;
            }

            public string Name { get; }
            public int Age { get; }
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
