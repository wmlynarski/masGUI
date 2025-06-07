using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mas_mp1
{
    public class Library
    {
        public string Name { get; set; }
        public Address Address { get; set; } 
        public List<Catalog> Catalogs { get; private set; } = new List<Catalog>();
        public List<BorrowerLibrarian> Librarians { get; private set; } = new List<BorrowerLibrarian>();
        public static List<Library> AllLibraries = new List<Library>();
        public List<Membership> Memberships { get; private set; } = new List<Membership>();
        public Library(string name, Address address)
        {
            Name = name;
            Address = address ?? throw new ArgumentNullException(nameof(address), "Address cannot be null.");
        }
        public void showCatalogs()
        {
            Console.WriteLine($"Catalogs of {Name}:");
            Console.WriteLine(Catalogs);
        }
        public void AddLibrarian(BorrowerLibrarian librarian)
        {
            Librarians.Add(librarian);
        }
        public void AddCatalog(Catalog catalog)
        {
            Catalogs.Add(catalog);
        }
        public void RemoveCatalog(Catalog catalog)
        {
            Catalogs.Remove(catalog);
        }
    }
}
