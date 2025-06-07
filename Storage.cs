using mas_mp1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace masGUI
{
    public static class Storage
    {
        private const string UserFile = "users.json";

        public static void SaveUsers(List<BorrowerLibrarian> users)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };
            File.WriteAllText(UserFile, JsonConvert.SerializeObject(users, settings));
        }

        public static List<BorrowerLibrarian> LoadUsers()
        {
            if (!File.Exists(UserFile))
                return new List<BorrowerLibrarian>();

            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            return JsonConvert.DeserializeObject<List<BorrowerLibrarian>>(File.ReadAllText(UserFile), settings)
                   ?? new List<BorrowerLibrarian>();
        }
    }
}
