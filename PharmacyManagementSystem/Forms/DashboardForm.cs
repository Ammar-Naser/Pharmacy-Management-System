using PharmacyManagementSystem.Helpers;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PharmacyManagementSystem.Forms
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            this.Text            = "PharmaCare – Dashboard";
            this.Size            = new Size(520, 560);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.BackColor       = UITheme.Background;

            var header = new Panel { Dock = DockStyle.Top, Height = 90, BackColor = UITheme.Primary };
            header.Controls.Add(new Label { Text = "⊕  PharmaCare", Font = new Font("Segoe UI", 18f, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(24, 18) });
            header.Controls.Add(new Label { Text = "Select a module to get started", Font = new Font("Segoe UI", 9f), ForeColor = Color.FromArgb(160, 200, 235), AutoSize = true, Location = new Point(26, 52) });

            string[] icons  = { "👤", "🧑", "💊", "🧾", "🏭" };
            string[] names  = { "Employees", "Customers", "Drugs", "Invoices", "Suppliers" };
            string[] descs  = { "Manage staff & salaries", "Customer records & contacts", "Inventory & drug catalog", "Sales & invoice history", "Supplier management" };
            int[]    tops   = { 108, 178, 248, 318, 388 };

            Action[] actions = new Action[]
            {
                () => new EmployeesForm().Show(),
                () => new CustomersForm().Show(),
                () => new DrugsForm().Show(),
                () => new InvoicesForm().Show(),
                () => new SuppliersForm().Show(),
            };

            for (int i = 0; i < names.Length; i++)
            {
                int idx = i;

                var card = new Panel { Location = new Point(30, tops[i]), Size = new Size(448, 60), BackColor = UITheme.Surface, Cursor = Cursors.Hand };
                card.Paint += (s, e) => {
                    using (var pen = new Pen(UITheme.Border, 1))
                        e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
                };

                card.Controls.Add(new Panel { Location = new Point(0, 0), Size = new Size(4, 60), BackColor = UITheme.Accent });
                card.Controls.Add(new Label { Text = icons[idx], Font = new Font("Segoe UI", 20f), Location = new Point(16, 8), Size = new Size(40, 40), TextAlign = ContentAlignment.MiddleCenter });
                card.Controls.Add(new Label { Text = names[idx], Font = new Font("Segoe UI", 11f, FontStyle.Bold), ForeColor = UITheme.TextPrimary, AutoSize = true, Location = new Point(66, 10) });
                card.Controls.Add(new Label { Text = descs[idx], Font = new Font("Segoe UI", 8.5f), ForeColor = UITheme.TextSecondary, AutoSize = true, Location = new Point(68, 33) });
                card.Controls.Add(new Label { Text = "›", Font = new Font("Segoe UI", 18f), ForeColor = UITheme.TextSecondary, AutoSize = true, Location = new Point(415, 14) });

                card.MouseEnter += (s, e) => card.BackColor = UITheme.SurfaceAlt;
                card.MouseLeave += (s, e) => card.BackColor = UITheme.Surface;
                card.MouseClick += (s, e) => actions[idx]();
                foreach (Control c in card.Controls) c.Click += (s, e) => actions[idx]();

                Controls.Add(card);
            }

            Controls.Add(header);
        }
    }
}
