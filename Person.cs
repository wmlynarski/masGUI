using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace mas_mp1
{
    public abstract class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address? Address { get; set; } 
        public DateOnly BirthDate { get; set; }
        public Person(string firstName, string lastName, DateOnly birthDate, Address? address)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Address = address;
        }
        public override string ToString() 
        {
            return $"{FirstName} {LastName}";
        }
    }
}
