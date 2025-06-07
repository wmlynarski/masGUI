using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mas_mp1
{
    public class Author : Person
    {
        public DateOnly? DeathDate { get; set; }
        public static List<Author> AllAuthors = new List<Author>(); 
        public Author(string firstName, string lastName, DateOnly birthDate, DateOnly? deathDate, Address? address) : base(firstName, lastName, birthDate, address)
        {
            BirthDate = birthDate;
            DeathDate = deathDate;
            AllAuthors.Add(this);
        }
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
