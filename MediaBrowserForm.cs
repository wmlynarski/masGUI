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
        private ListBox historyBox;
        private BorrowerLibrarian user;

        public MediaBrowserForm(BorrowerLibrarian borrower)
        {
            user = borrower;
            Text = $"Zasoby - {user.FirstName} {user.LastName}";
            Width = 600;
            Height = 600;

            var infoLabel = new Label
            {
                Text = "Zasoby dostępne w bibliotekach:",
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
            listBox.SelectedIndexChanged += ListBox_SelectedIndexChanged;
            Controls.Add(listBox);

            var borrowBtn = new Button
            {
                Text = "Wypożycz",
                Left = 10,
                Top = 350,
                Width = 120
            };
            borrowBtn.Click += BorrowBtn_Click;
            Controls.Add(borrowBtn);

            var closeBtn = new Button
            {
                Text = "Zamknij",
                Left = 470,
                Top = 350,
                Width = 100
            };
            closeBtn.Click += (s, e) => Close();
            Controls.Add(closeBtn);

            var historyLabel = new Label
            {
                Text = "Historia wypożyczeń wybranego zasobu:",
                Left = 10,
                Top = 390,
                Width = 560
            };
            Controls.Add(historyLabel);

            historyBox = new ListBox
            {
                Left = 10,
                Top = 420,
                Width = 560,
                Height = 120
            };
            Controls.Add(historyBox);

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
                            loan.MediaItem.MediaItemID == item.MediaItemID &&
                            loan.Status == Status.Borrowed);

                        string statusLabel = activeLoan != null ? "[WYPOŻYCZONA]" : "[DOSTĘPNA]";
                        listBox.Items.Add($"{item.MediaItemID}: {item.Title} ({item.PublicationYear}) - {item.GetMediaType()} — {library.Name} {statusLabel}");
                    }
                }
            }
        }

        private void BorrowBtn_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
            {
                MessageBox.Show("Wybierz zasób do wypożyczenia.");
                return;
            }

            string selectedText = listBox.SelectedItem.ToString();
            int itemId = int.Parse(selectedText.Split(':')[0]);

            var item = MediaItem.AllMediaItems.FirstOrDefault(m => m.MediaItemID == itemId);

            if (item == null)
            {
                MessageBox.Show("Nie znaleziono zasobu.");
                return;
            }

            var activeLoan = Loan.AllLoans.FirstOrDefault(loan =>
                loan.MediaItem.MediaItemID == item.MediaItemID && loan.Status == Status.Borrowed);

            if (activeLoan != null)
            {
                MessageBox.Show("Ta pozycja jest już wypożyczona.");
                return;
            }

            var confirmationForm = new LoanConfirmationForm(user, item);
            confirmationForm.ShowDialog();

            Loan.AllLoans = LoanStorage.LoadLoans();
            ReloadItems();
        }

        private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            historyBox.Items.Clear();

            if (listBox.SelectedIndex == -1)
                return;

            string selectedText = listBox.SelectedItem.ToString();
            int itemId = int.Parse(selectedText.Split(':')[0]);

            var item = MediaItem.AllMediaItems.FirstOrDefault(m => m.MediaItemID == itemId);
            if (item == null)
                return;

            var itemLoans = Loan.AllLoans
                .Where(loan => loan.MediaItem.MediaItemID == item.MediaItemID)
                .OrderByDescending(loan => loan.DueDate)
                .ToList();

            if (!itemLoans.Any())
            {
                historyBox.Items.Add("Brak historii wypożyczeń.");
                return;
            }

            foreach (var loan in itemLoans)
            {
                string kto = loan.BorrowerLibrarian?.ToString() ?? "[nieznany]";
                string status = loan.Status switch
                {
                    Status.Borrowed => "Wypożyczona",
                    Status.ReturnedInTime => "Zwrócona w terminie",
                    Status.ReturnedLateFinePayed => "Zwrócona po terminie (kara zapłacona)",
                    Status.ReturnedLateFineNotPayed => "Zwrócona po terminie (kara nie zapłacona)",
                    _ => "Nieznany"
                };

                string dataWypozyczenia = loan.DueDate.AddDays(-30).ToString("dd.MM.yyyy");

                string linia = loan.Status == Status.Borrowed
                    ? $" {dataWypozyczenia} Do: {loan.DueDate:dd.MM.yyyy} {kto} {status}"
                    : $" {dataWypozyczenia} → {loan.DueDate:dd.MM.yyyy}  {kto} {status}";

                historyBox.Items.Add(linia);
            }
        }
    }

}