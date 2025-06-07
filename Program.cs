using mas_mp1;

namespace masGUI
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            BorrowerLibrarian.AllBorrowerLibrarians = Storage.LoadUsers();
            Library.AllLibraries = LibraryStorage.LoadLibraries();
            Loan.AllLoans = LoanStorage.LoadLoans();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());

            Storage.SaveUsers(BorrowerLibrarian.AllBorrowerLibrarians);
            LibraryStorage.SaveLibraries(Library.AllLibraries);
            LoanStorage.SaveLoans(Loan.AllLoans);
        }
    }
}