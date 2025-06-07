using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mas_mp1
{
    public abstract class MediaItem
    {
        public int MediaItemID { get; private set; }
        public string Title { get; set; }
        public int PublicationYear { get; set; }
        public Catalog Catalog { get; set; }
        public string? Edition { get; set; } 
        public static List<MediaItem> AllMediaItems = new List<MediaItem>(); 
        private static int _nextMediaItemID = 1;
        public MediaItem(string title, int publicationYear, string? edition = null)
        {
            MediaItemID = _nextMediaItemID++;
            Title = title;
            PublicationYear = publicationYear;
            Edition = edition;
            AllMediaItems.Add(this);
        }
        public int Age
        {
            get
            {
                return DateTime.Now.Year - PublicationYear;
            }
        }
        public static List<MediaItem> SearchByTitle(string title) 
        {
            return AllMediaItems.Where(m => m.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public abstract string GetMediaType();
        public override string ToString() 
        {
            return $"{GetMediaType()} [{MediaItemID}]: published in {PublicationYear}, Age: {Age} years";
        }

    }
}
