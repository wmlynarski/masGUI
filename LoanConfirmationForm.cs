using mas_mp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace masGUI
{
    public class LoanConfirmationForm : Form
    {
        public LoanConfirmationForm(BorrowerLibrarian user, MediaItem item)
        {
            Text = "Potwierdzenie wypożyczenia";
            Width = 400;
            Height = 260;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterParent;

            int loanDays = user.IsHonorable() ? 60 : 30;
            DateTime dueDate = DateTime.Now.AddDays(loanDays);

            string summary =
                $"Czy na pewno chcesz wypożyczyć:\n\n" +
                $"{item.Title} ({item.PublicationYear})\n" +
                $"Typ: {item.GetMediaType()}\n\n" +
                $"Czas na zwrot: {loanDays} dni (do {dueDate:dd.MM.yyyy})";

            var label = new Label
            {
                Text = summary,
                Left = 10,
                Top = 20,
                Width = 360,
                Height = 120
            };
            Controls.Add(label);

            var okBtn = new Button
            {
                Text = "Zatwierdź",
                Left = 80,
                Top = 150,
                Width = 100
            };
            okBtn.Click += (s, e) =>
            {
                var loan = new Loan(user, item);
                loan.SetStatusBorrowed();
                user.Loans.Add(loan);
                user.LoanCount++;
                Loan.AllLoans.Add(loan);
                LoanStorage.SaveLoans(Loan.AllLoans);
                MessageBox.Show("Wypożyczono!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine(loan.MediaItem.MediaItemID);
                Console.WriteLine("Blabla");
                Close();
            };
            Controls.Add(okBtn);

            var cancelBtn = new Button
            {
                Text = "Anuluj",
                Left = 200,
                Top = 150,
                Width = 100
            };
            cancelBtn.Click += (s, e) => Close();
            Controls.Add(cancelBtn);
        }
    }
}
