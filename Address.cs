using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mas_mp1
{
    public class Address //atrybut złożony
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public Address(string street, string city, string zipCode, string country)
        {
            Street = street;
            City = city;
            ZipCode = zipCode;
            Country = country;
        }
        public override string ToString() //przesłonięcie
        {
            return $"{Street}, {City}, {ZipCode}, {Country}";
        }
    }
}
