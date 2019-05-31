using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Core.Extensions.LinqRelated;
using Xunit;

namespace Core.Test.LinqRelated
{
    public class PropertyNameExtractionTest
    {
        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        public class PropertyDumper<T>
        {
            public string Dump<TProperty>(Expression<Func<T, TProperty>> expression)
            {
                var memberInfo = expression.GetMember();

                if (memberInfo.MemberType == MemberTypes.Property)
                {
                    var pi = (PropertyInfo) memberInfo;
                }
                var name = memberInfo.Name;
                var type = typeof(TProperty);

                return $"{type.Name} {name}";
            }
        }

        [Fact]
        public void TestMapper()
        {
            var person = new Person
            {
                Name = "John Doe",
                Age  = 21
            };

            var dumper = new PropertyDumper<Person>();
            var dump1 = dumper.Dump(p=>p.Name);
            var dump2 = dumper.Dump(p=>p.Age);

            Assert.Equal("String Name", dump1);
            Assert.Equal("Int32 Age", dump2);

        }
    }
}
