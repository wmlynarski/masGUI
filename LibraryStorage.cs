using mas_mp1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace masGUI
{
    public static class LibraryStorage
    {
        private const string LibraryFile = "libraries.json";

        public static void SaveLibraries(List<Library> libraries)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };
            File.WriteAllText(LibraryFile, JsonConvert.SerializeObject(libraries, settings));
        }

        public static List<Library> LoadLibraries()
        {
            if (!File.Exists(LibraryFile))
                return new List<Library>();

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            return JsonConvert.DeserializeObject<List<Library>>(File.ReadAllText(LibraryFile), settings)
                ?? new List<Library>();
        }
    }
}
