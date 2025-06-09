using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mas_mp1
{
    public enum Status
    {
        Borrowed = 0,              
        ReturnedInTime = 1,
        ReturnedLateFinePayed = 2,
        ReturnedLateFineNotPayed = 3
    }
}
