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
        private TextBox firstNameBox, lastNameBox, idBox, phoneBox;

        public RegisterForm()
        {
            Text = "Rejestracja";
            Width = 320;
            Height = 280;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            int labelWidth = 100;
            int fieldWidth = 180;
            int top = 20;

            // Imię
            Controls.Add(new Label { Text = "Imię:", Left = 10, Top = top, Width = labelWidth });
            firstNameBox = new TextBox { Left = 120, Top = top, Width = fieldWidth };
            Controls.Add(firstNameBox);

            top += 30;
            Controls.Add(new Label { Text = "Nazwisko:", Left = 10, Top = top, Width = labelWidth });
            lastNameBox = new TextBox { Left = 120, Top = top, Width = fieldWidth };
            Controls.Add(lastNameBox);

            top += 30;
            Controls.Add(new Label { Text = "ID:", Left = 10, Top = top, Width = labelWidth });
            idBox = new TextBox { Left = 120, Top = top, Width = fieldWidth };
            Controls.Add(idBox);

            top += 35;
            var line = new Panel
            {
                Left = 10,
                Top = top,
                Width = 290,
                Height = 1,
                BorderStyle = BorderStyle.Fixed3D
            };
            Controls.Add(line);

            top += 15;
            Controls.Add(new Label { Text = "Telefon (można kilka, przecinkiem):", Left = 10, Top = top, Width = 290 });
            top += 20;
            phoneBox = new TextBox { Left = 10, Top = top, Width = 290 };
            Controls.Add(phoneBox);

            var saveBtn = new Button { Text = "Zapisz", Left = 110, Top = top + 40, Width = 100 };
            saveBtn.Click += SaveBtn_Click;
            Controls.Add(saveBtn);
        }

        private void SaveBtn_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(phoneBox.Text))
            {
                MessageBox.Show("Podaj przynajmniej jeden numer telefonu.");
                return;
            }

            var phones = phoneBox.Text.Split(',').Select(p => p.Trim()).Where(p => !string.IsNullOrEmpty(p)).ToList();
            var user = new BorrowerLibrarian(firstNameBox.Text, lastNameBox.Text, null, new DateOnly(1990, 1, 1));
            user.AddBorrowerRole(idBox.Text.Trim(), phones);

            BorrowerLibrarian.AllBorrowerLibrarians.Add(user);
            MessageBox.Show("Zarejestrowano użytkownika.");
            Close();
        }
    }
}
