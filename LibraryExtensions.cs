using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mas_mp1
{
    public static class LibraryExtensions
    {
        public static string GetExtendedInfo(this MediaItem mediaItem) 
        {
            return $"{mediaItem.Title} (Age: {mediaItem.Age}";
        }
        public static bool IsOverdue(this Loan loan) 
        {
            return DateTime.Now > loan.DueDate;
        }

    }
}
