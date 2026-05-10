using PharmacyManagementSystem.Helpers;
using PharmacyManagementSystem.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PharmacyManagementSystem.Forms
{
    public partial class LoginForm : Form
    {
        TextBox ssn  = new TextBox();
        TextBox name = new TextBox();

        public LoginForm()
        {
            this.Text            = "Pharmacy Management System – Login";
            this.Size            = new Size(460, 520);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.BackColor       = UITheme.Background;

            // Accent strip
            var accent = new Panel { Dock = DockStyle.Left, Width = 6, BackColor = UITheme.Accent };

            // Header
            var header = new Panel { Dock = DockStyle.Top, Height = 130, BackColor = UITheme.Primary };
            header.Controls.Add(new Label { Text = "⊕", Font = new Font("Segoe UI", 28f, FontStyle.Bold), ForeColor = UITheme.Accent, AutoSize = true, Location = new Point(30, 22) });
            header.Controls.Add(new Label { Text = "PharmaCare", Font = new Font("Segoe UI", 20f, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(30, 60) });
            header.Controls.Add(new Label { Text = "Management System", Font = new Font("Segoe UI", 9f), ForeColor = Color.FromArgb(160, 200, 235), AutoSize = true, Location = new Point(32, 90) });

            // Card
            var card = new Panel { Location = new Point(30, 150), Size = new Size(390, 300), BackColor = UITheme.Surface };
            card.Paint += (s, e) => {
                using (var pen = new Pen(UITheme.Border, 1))
                    e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
            };

            card.Controls.Add(new Label { Text = "Sign In", Font = new Font("Segoe UI", 14f, FontStyle.Bold), ForeColor = UITheme.TextPrimary, AutoSize = true, Location = new Point(24, 22) });
            card.Controls.Add(UITheme.CreateSeparator(24, 52, 342));

            card.Controls.Add(UITheme.CreateLabel("EMPLOYEE SSN", 24, 68, true));
            UITheme.StyleTextBox(ssn, 24, 88, 342);
            PlaceholderHelper.Set(ssn, "SSN");
            card.Controls.Add(ssn);

            card.Controls.Add(UITheme.CreateLabel("FULL NAME", 24, 132, true));
            UITheme.StyleTextBox(name, 24, 152, 342);
            PlaceholderHelper.Set(name, "Name");
            card.Controls.Add(name);

            card.Controls.Add(new Label { Text = "ℹ  Use SSN \"1\" and name \"Ammar Naser\" to login", Font = new Font("Segoe UI", 8f, FontStyle.Italic), ForeColor = UITheme.TextSecondary, AutoSize = true, Location = new Point(24, 196) });

            var login = UITheme.CreateButton("SIGN IN", 24, 230, UITheme.BtnStyle.Primary, 342);
            login.Height = 38;
            login.Font   = new Font("Segoe UI", 10f, FontStyle.Bold);

            // Wiring (UNCHANGED)
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

            card.Controls.Add(login);

            Controls.Add(accent);
            Controls.Add(header);
            Controls.Add(card);
            Controls.Add(new Label { Text = "v1.0  •  Pharmacy Management System", Font = new Font("Segoe UI", 8f), ForeColor = UITheme.TextSecondary, AutoSize = true, Location = new Point(115, 465) });
        }
    }
}
