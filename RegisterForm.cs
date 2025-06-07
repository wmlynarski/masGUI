using mas_mp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace masGUI
{
    public class RegisterForm : Form
    {
        TextBox firstNameBox, lastNameBox, idBox, phoneBox;

        public RegisterForm()
        {
            Text = "Rejestracja";
            Width = 300; Height = 250;

            Controls.Add(new Label { Text = "Imię:", Left = 10, Top = 20 });
            firstNameBox = new TextBox { Left = 100, Top = 20, Width = 150 };
            Controls.Add(firstNameBox);

            Controls.Add(new Label { Text = "Nazwisko:", Left = 10, Top = 50 });
            lastNameBox = new TextBox { Left = 100, Top = 50, Width = 150 };
            Controls.Add(lastNameBox);

            Controls.Add(new Label { Text = "ID:", Left = 10, Top = 80 });
            idBox = new TextBox { Left = 100, Top = 80, Width = 150 };
            Controls.Add(idBox);

            Controls.Add(new Label { Text = "Telefon (można kilka, przecinkiem):", Left = 10, Top = 110, Width = 250 });
            phoneBox = new TextBox { Left = 10, Top = 130, Width = 260 };
            Controls.Add(phoneBox);

            var saveBtn = new Button { Text = "Zapisz", Left = 100, Top = 170 };
            saveBtn.Click += (s, e) =>
            {
                var phonesRaw = phoneBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(phonesRaw))
                {
                    MessageBox.Show("Podaj przynajmniej jeden numer telefonu.");
                    return;
                }

                var phoneList = new List<string>(phonesRaw.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));

                var user = new BorrowerLibrarian(firstNameBox.Text, lastNameBox.Text, null, new DateOnly(1990, 1, 1));
                user.AddBorrowerRole(idBox.Text.Trim(), phoneList);

                BorrowerLibrarian.AllBorrowerLibrarians.Add(user);
                MessageBox.Show("Zarejestrowano użytkownika.");
                Close();
            };
            Controls.Add(saveBtn);
        }
    }
}
