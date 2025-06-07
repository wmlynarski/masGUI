using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mas_mp1
{
    class Magazine : MediaItem
    {
        public int IssueNumber { get; set; }
        public Magazine(string title, int publicationYear, int issueNumber, string? edition = null) : base(title, publicationYear, edition)
        {
            IssueNumber = issueNumber;
        }
        public override string GetMediaType() => "Magazine";
    }
}
