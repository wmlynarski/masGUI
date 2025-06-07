namespace mas_mp1
{
	public class Membership
	{
		public DateTime Since { get; set; }
		public BorrowerLibrarian BorrowerLibrarian { get; private set; }
		public Library Library { get; private set; }
        public Membership(BorrowerLibrarian borrowerLibrarian, Library library, DateTime since)
		{
			BorrowerLibrarian = borrowerLibrarian ?? throw new ArgumentNullException(nameof(borrowerLibrarian));
			Library = library ?? throw new ArgumentNullException(nameof(library));
			Since = since;
			borrowerLibrarian.Memberships.Add(this);
			library.Memberships.Add(this);
		}
	}
}