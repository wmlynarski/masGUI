using mas_mp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace masGUI
{
    public partial class LoginForm : Form
    {
        TextBox loginBox, passBox;

        public LoginForm()
        {
            Text = "Logowanie";
            Width = 300; Height = 200;

            Controls.Add(new Label { Text = "Imię:", Left = 10, Top = 20, Width = 80 });
            loginBox = new TextBox { Left = 100, Top = 20, Width = 150 };
            Controls.Add(loginBox);

            Controls.Add(new Label { Text = "Nazwisko:", Left = 10, Top = 60, Width = 80 });
            passBox = new TextBox { Left = 100, Top = 60, Width = 150 };
            Controls.Add(passBox);

            var loginBtn = new Button { Text = "Zaloguj", Left = 50, Top = 100, Width = 80 };
            loginBtn.Click += LoginBtn_Click;
            Controls.Add(loginBtn);

            var registerBtn = new Button { Text = "Zarejestruj się", Left = 150, Top = 100, Width = 100 };
            registerBtn.Click += (s, e) => { new RegisterForm().ShowDialog(); };
            Controls.Add(registerBtn);
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            var user = BorrowerLibrarian.AllBorrowerLibrarians.FirstOrDefault(u =>
                u.FirstName.Equals(loginBox.Text.Trim(), StringComparison.OrdinalIgnoreCase) &&
                u.LastName.Equals(passBox.Text.Trim(), StringComparison.OrdinalIgnoreCase));

            if (user == null || !user.IsBorrower())
            {
                MessageBox.Show("Nieprawidłowe dane lub brak roli Borrower.");
                return;
            }

            Hide();
            new MediaBrowserForm(user).ShowDialog();
            Close();
        }
    }
}
