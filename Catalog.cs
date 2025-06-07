using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mas_mp1
{
    public class Catalog
    {
        public string Name { get; set; }
        public List<MediaItem> MediaItems { get; set; }
        public List<MediaItem> Archived { get; set; } = new List<MediaItem>();
        public Library Library { get; set; }
        public Dictionary<int, MediaItem> ById { get; private set; }
        public Catalog(string name)
        {
            Name = name;
            MediaItems = new List<MediaItem>();
            ById = new Dictionary<int, MediaItem>();
        }
        public static void DisplayAllMediaItems()
        {
            foreach (var mediaItem in MediaItem.AllMediaItems)
            {
                Console.WriteLine(mediaItem);
            }
        }
        public void AddMediaItem(MediaItem mediaItem)
        {
            if (mediaItem == null) throw new ArgumentNullException(nameof(mediaItem));
            MediaItems.Add(mediaItem);
            ById[mediaItem.MediaItemID] = mediaItem;
            this.SaveToFile();
        }

        public void RemoveMediaItem(MediaItem mediaItem)
        {
            MediaItems.Remove(mediaItem);
            ById.Remove(mediaItem.MediaItemID);
            Archived.Add(mediaItem);
            this.SaveToFile();
        }
    }
}
