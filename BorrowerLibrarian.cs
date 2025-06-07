using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mas_mp1
{
    public class BorrowerLibrarian : Person
    {
        public Role Roles = Role.Borrower;
        public static List<BorrowerLibrarian> AllBorrowerLibrarians = new List<BorrowerLibrarian>();
        public string BorrowerID { get; set; } = string.Empty;
        public string LibrarianID { get; set; } = string.Empty;
        public int LoanCount { get; set; }
        public Library? Library { get; set; } 
        public List<Membership> Memberships { get; set; } = new List<Membership>();
        public List<Loan> Loans { get; set; } = new List<Loan>();
        public List<string>? PhoneNumbers { get; set; }
        public int CommentCount { get; set; }
        public int RecommendationCount { get; set; }
        public int ReadBooksCount { get; set; }
        public int WatchedDVDsCount { get; set; }
        public BorrowerLibrarian(string firstName, string lastName, Address? address, DateOnly birthDate) : base(firstName, lastName, birthDate, address)
        {   
        }
        public void Borrow(MediaItem item)
        {
            if (Loans.Count(l => l.GetStatus == Status.Borrowed) >= 5)
                throw new InvalidOperationException("Limit aktywnych wypożyczeń (5) został przekroczony.");
            if (!IsBorrower())
                throw new InvalidOperationException("Only borrowers can borrow items.");
            var library = item.Catalog.Library;
            if (!Memberships.Any(m => m.Library == library))
                throw new InvalidOperationException($"Brak aktywnego członkostwa w bibliotece „{library.Name}”.");
            var loan = new Loan(this, item);
            loan.SetStatusBorrowed();
            Loans.Add(loan);
            LoanCount++;
        }
        public void Return(MediaItem item)
        {
            var loan = Loans.Find(l => l.MediaItem == item && l.GetStatus == Status.Borrowed);
            if (loan != null)
            {
                if (DateTime.Now <= loan.DueDate)
                    loan.SetStatusReturnedInTime();
                else
                    loan.SetStatusReturnedLateFineNotPayed();

                Loans.Remove(loan);

                if (item is Book)
                    ReadBooksCount++;
                else if (item is DVD)
                    WatchedDVDsCount++;
            }
        }
        public void WriteComment(MediaItem item, string comment)
        {
            CommentCount++;
        }
        public void GiveRecommendation(MediaItem item, string recommendation)
        {
            if(IsExpert())
            {
                RecommendationCount++;
            }
            else
            {
                throw new InvalidOperationException("Only experts can give recommendations.");
            }
        }
        public bool IsBorrower() => Roles.HasFlag(Role.Borrower);
        public bool IsLibrarian() => Roles.HasFlag(Role.Librarian);
        public bool IsExpert() => LoanCount >= 30 && CommentCount >= 30;
        public void AddLibrarianRole(string librarianId, Library library)
        {
            Roles |= Role.Librarian;
            LibrarianID = librarianId;
            library.AddLibrarian(this);
        }
        public void AddBorrowerRole(string borrowerId, List<String> phoneNumbers)
        {
            Roles |= Role.Borrower;
            BorrowerID = borrowerId;
            PhoneNumbers = phoneNumbers;
        }
        public void AddMembership(BorrowerLibrarian person, Library library, DateTime since)
        {
            if (!IsLibrarian())
                throw new InvalidOperationException("Only librarians can assign memberships.");
            Memberships.Add(new Membership(person, library, since));
        }
        public bool IsHonorable()
        {
            return IsBorrower() && IsLibrarian();
        }
        public bool IsReader()
        {
            return ReadBooksCount > WatchedDVDsCount;
        }
    }
}
