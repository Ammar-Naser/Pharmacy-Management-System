using PharmacyManagementSystem.Forms;
using PharmacyManagementSystem.Helpers;
using PharmacyManagementSystem.Services;
using System;
using System.Windows.Forms;

namespace PharmacyManagementSystem.Forms
{
    public partial class LoginForm : Form
    {
        TextBox ssn = new TextBox();
        TextBox name = new TextBox();

        public LoginForm()
        {
            this.Text = "Login";
            this.Size = new System.Drawing.Size(400, 300);

            PlaceholderHelper.Set(ssn, "SSN");
            PlaceholderHelper.Set(name, "Name");
            
            ssn.Top = 50;
            name.Top = 90;

            ssn.Left = 130;
            name.Left = 130;

            ssn.Width = 160; name.Width = 160;

            Label login_Message = new Label();
            login_Message.Text = "SSN and Name Must be stored in the customers table in the DB to login,\n        so write \"1\" and \"Ammar Naser\" to login sucssesfully :)";
            login_Message.Top = 140;
            login_Message.Left = 10;
            login_Message.Width = 800;
            login_Message.AutoSize = true;

            Button login = new Button() { Text = "Login", Top = 200, Left= 170 };

            login.Click += (s, e) =>
            {
                if (AuthService.Login(ssn.Text, name.Text))
                {
                    new DashboardForm().Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Login");
                }
            };

            Controls.Add(ssn);
            Controls.Add(name);
            Controls.Add(login_Message);
            Controls.Add(login);
        }
    }
}
