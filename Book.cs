using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mas_mp1
{
    class Book : MediaItem
    {
        public List<Author> Authors;
        public int NumberOfPages { get; set; } 
        public Book(string title, int publicationYear, List<Author> authors, int numberOfPages, string? edition = null) : base(title, publicationYear, edition)
        {
            Authors = authors;
            NumberOfPages = numberOfPages;
        }
        public override string GetMediaType() => "Book";
        public string GetDetailedInfo() 
        {
                return $"{base.ToString()} - Authors: {Authors}";
        }
    }
}
