using System;
using System.Drawing;
using System.Windows.Forms;
using PharmacyManagementSystem.Services;
using PharmacyManagementSystem.Helpers;

namespace PharmacyManagementSystem.Forms
{
    public partial class EmployeesForm : Form
    {
        DataGridView grid   = new DataGridView();
        TextBox      ssn    = new TextBox();
        TextBox      name   = new TextBox();
        TextBox      salary = new TextBox();

        public EmployeesForm()
        {
            this.Text            = "Employees";
            this.Size            = new Size(860, 580);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.BackColor       = UITheme.Background;

            // ── Header ────────────────────────────────────────────────────
            var header = UITheme.CreateHeader("👤  Employees", "Manage staff records and salaries");
            Controls.Add(header);

            // ── Grid ──────────────────────────────────────────────────────
            grid.Location = new Point(20, 95);
            grid.Size     = new Size(810, 220);
            UITheme.StyleGrid(grid);
            Controls.Add(grid);

            // ── Form card ─────────────────────────────────────────────────
            var card = UITheme.CreateCard(20, 330, 810, 190);
            Controls.Add(card);

            var formTitle = UITheme.CreateLabel("Employee Details", 16, 12);
            formTitle.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            card.Controls.Add(formTitle);
            card.Controls.Add(UITheme.CreateSeparator(16, 34, 778));

            // Field labels & inputs
            int row1Y = 48, row2Y = 104;

            card.Controls.Add(UITheme.CreateLabel("SSN", 16, row1Y, true));
            UITheme.StyleTextBox(ssn, 16, row1Y + 18, 220);
            PlaceholderHelper.Set(ssn, "Employee SSN");
            card.Controls.Add(ssn);

            card.Controls.Add(UITheme.CreateLabel("FULL NAME", 256, row1Y, true));
            UITheme.StyleTextBox(name, 256, row1Y + 18, 260);
            PlaceholderHelper.Set(name, "Full Name");
            card.Controls.Add(name);

            card.Controls.Add(UITheme.CreateLabel("SALARY (EGP)", 536, row1Y, true));
            UITheme.StyleTextBox(salary, 536, row1Y + 18, 240);
            PlaceholderHelper.Set(salary, "e.g. 12000");
            card.Controls.Add(salary);

            // Buttons
            var load   = UITheme.CreateButton("⟳  Load",   16,  row2Y, UITheme.BtnStyle.Secondary, 120);
            var add    = UITheme.CreateButton("＋  Add",    146, row2Y, UITheme.BtnStyle.Success,   120);
            var update = UITheme.CreateButton("✎  Update", 276, row2Y, UITheme.BtnStyle.Primary,   120);
            var del    = UITheme.CreateButton("✕  Delete", 406, row2Y, UITheme.BtnStyle.Danger,    120);

            // ── Wiring (UNCHANGED) ────────────────────────────────────────
            load.Click += (s, e) => grid.DataSource = EmployeeService.GetAll();

            add.Click += (s, e) =>
            {
                EmployeeService.Add(ssn.Text, name.Text, double.Parse(salary.Text));
                load.PerformClick();
            };

            update.Click += (s, e) =>
            {
                EmployeeService.Update(ssn.Text, name.Text, double.Parse(salary.Text));
                load.PerformClick();
            };

            del.Click += (s, e) =>
            {
                EmployeeService.Delete(ssn.Text);
                load.PerformClick();
            };

            card.Controls.Add(load);
            card.Controls.Add(add);
            card.Controls.Add(update);
            card.Controls.Add(del);
        }
    }
}
