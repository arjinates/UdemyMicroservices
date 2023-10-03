using FreeCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    //[Owned]
    public class Address : ValueObject //EF CORE OWNED TYPE
    { //Address is a type with no identity property. It is used as a property of the Order type to specify the shipping address for a particular order.
        public string Province { get; private set; } 
        public string Disctrict { get; private set; }
        public string Street { get; private set; }
        public string ZipCode { get; private set; }
        public string Line { get; private set; }

        public Address(string province, string disctrict, string street, string zipCode, string line)
        {
            Province = province;
            Disctrict = disctrict;
            Street = street;
            ZipCode = zipCode;
            Line = line;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Province;
            yield return Disctrict;
            yield return Street;
            yield return ZipCode;
            yield return Line;
        }
    }
}
