using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mas_mp1
{
    class DVD : MediaItem
    {
        public TimeSpan Duration { get; set; }
        public DVD(string title, int publicationYear, TimeSpan duration, string? edition = null) : base(title, publicationYear, edition)
        {
            Duration = duration;
        }
        public override string GetMediaType() => "DVD";
        public override string ToString()
        {
            return base.ToString() + $", duration: {Duration}";
        }
    }
}
