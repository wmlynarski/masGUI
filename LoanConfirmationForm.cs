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
            Width = 400; Height = 200;

            var summary = $"Czy na pewno chcesz wypożyczyć:\n\n{item.Title} ({item.PublicationYear})\nTyp: {item.GetMediaType()}";
            var label = new Label { Text = summary, Left = 10, Top = 20, Width = 360, Height = 60 };
            Controls.Add(label);

            var okBtn = new Button { Text = "Zatwierdź", Left = 80, Top = 100, Width = 100 };
            okBtn.Click += (s, e) =>
            {
                var loan = new Loan(user, item);
                loan.SetStatusBorrowed();
                user.Loans.Add(loan);
                user.LoanCount++;
                Loan.AllLoans.Add(loan);

                LoanStorage.SaveLoans(Loan.AllLoans);
                MessageBox.Show("Wypożyczono!");
                Close();
            };
            Controls.Add(okBtn);

            var cancelBtn = new Button { Text = "Anuluj", Left = 200, Top = 100, Width = 100 };
            cancelBtn.Click += (s, e) => Close();
            Controls.Add(cancelBtn);
        }
    }
}
