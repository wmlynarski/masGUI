using mas_mp1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace masGUI
{
    public static class LoanStorage
    {
        private const string LoanFile = "loans.json";

        public static void SaveLoans(List<Loan> loans)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };
            File.WriteAllText(LoanFile, JsonConvert.SerializeObject(loans, settings));
        }

        public static List<Loan> LoadLoans()
        {
            if (!File.Exists(LoanFile))
                return new List<Loan>();

            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            return JsonConvert.DeserializeObject<List<Loan>>(File.ReadAllText(LoanFile), settings)
                ?? new List<Loan>();
        }
    }
}
