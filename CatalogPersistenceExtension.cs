using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Xml;

namespace mas_mp1
{
    static class CatalogPersistenceExtension
    {
        private static readonly string persistenceFile = "catalog.json";
        public static void SaveToFile(this Catalog catalog)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = (Newtonsoft.Json.Formatting)System.Xml.Formatting.Indented
            };
            string json = JsonConvert.SerializeObject(catalog.MediaItems, settings);
            File.WriteAllText(persistenceFile, json);
        }
        public static void LoadFromFile(this Catalog catalog)
        {
            if (File.Exists(persistenceFile))
            {
                string json = File.ReadAllText(persistenceFile);
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };

                var items = JsonConvert.DeserializeObject<List<MediaItem>>(json, settings);

                if (items != null)
                {
                    catalog.MediaItems.Clear();
                    catalog.MediaItems.AddRange(items);
                    MediaItem.AllMediaItems.Clear();
                    MediaItem.AllMediaItems.AddRange(items);
                }
            }
        }
    }
}
