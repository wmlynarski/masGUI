using mas_mp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace masGUI
{
    public class MediaBrowserForm : Form
    {
        private ListBox listBox;
        private BorrowerLibrarian user;

        public MediaBrowserForm(BorrowerLibrarian borrower)
        {
            user = borrower;
            Text = $"Zasoby - {user.FirstName} {user.LastName}";
            Width = 600;
            Height = 450;

            var infoLabel = new Label
            {
                Text = $"Zasoby dostępne w bibliotekach:",
                Top = 10,
                Left = 10,
                Width = 560
            };
            Controls.Add(infoLabel);

            listBox = new ListBox
            {
                Left = 10,
                Top = 40,
                Width = 560,
                Height = 300
            };
            Controls.Add(listBox);

            listBox.Items.Clear();
            foreach (var library in Library.AllLibraries)
            {
                foreach (var catalog in library.Catalogs)
                {
                    foreach (var item in catalog.MediaItems)
                    {
                        var activeLoan = Loan.AllLoans.FirstOrDefault(loan =>
                            loan.MediaItem == item && loan.GetStatus == Status.Borrowed);

                        string statusLabel = activeLoan != null ? "[WYPOŻYCZONA]" : "[DOSTĘPNA]";
                        listBox.Items.Add($"{item.MediaItemID}: {item.Title} ({item.PublicationYear}) - {item.GetMediaType()} — {library.Name} {statusLabel}");
                    }
                }
            }

            var borrowBtn = new Button
            {
                Text = "Wypożycz",
                Left = 10,
                Top = 360,
                Width = 120
            };
            borrowBtn.Click += BorrowBtn_Click;
            Controls.Add(borrowBtn);

            var closeBtn = new Button
            {
                Text = "Zamknij",
                Left = 460,
                Top = 360,
                Width = 100
            };
            closeBtn.Click += (s, e) => Close();
            Controls.Add(closeBtn);
        }

        private void BorrowBtn_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
            {
                MessageBox.Show("Wybierz zasób do wypożyczenia.");
                return;
            }

            string selectedText = listBox.SelectedItem.ToString()!;
            string title = selectedText.Split(':')[1].Split('(')[0].Trim();

            var item = MediaItem.AllMediaItems.FirstOrDefault(m =>
                m.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (item == null)
            {
                MessageBox.Show("Nie znaleziono zasobu.");
                return;
            }

            var activeLoan = Loan.AllLoans.FirstOrDefault(loan =>
                loan.MediaItem == item && loan.GetStatus == Status.Borrowed);

            if (activeLoan != null)
            {
                MessageBox.Show("Ta pozycja jest już wypożyczona.");
                return;
            }

            var confirmationForm = new LoanConfirmationForm(user, item);
            confirmationForm.ShowDialog();
            ReloadItems();
        }
        private void ReloadItems()
        {
            listBox.Items.Clear();

            foreach (var library in Library.AllLibraries)
            {
                foreach (var catalog in library.Catalogs)
                {
                    foreach (var item in catalog.MediaItems)
                    {
                        var activeLoan = Loan.AllLoans.FirstOrDefault(loan =>
                            loan.MediaItem.MediaItemID == item.MediaItemID && loan.GetStatus == Status.Borrowed);

                        string statusLabel = activeLoan != null ? "[WYPOŻYCZONA]" : "[DOSTĘPNA]";
                        listBox.Items.Add($"{item.MediaItemID}: {item.Title} ({item.PublicationYear}) - {item.GetMediaType()} — {library.Name} {statusLabel}");
                    }
                }
            }
        }
    }
}
