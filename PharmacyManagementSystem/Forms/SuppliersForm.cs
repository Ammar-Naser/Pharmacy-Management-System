using PharmacyManagementSystem.Helpers;
using PharmacyManagementSystem.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PharmacyManagementSystem.Forms
{
    public partial class SuppliersForm : Form
    {
        DataGridView grid  = new DataGridView();
        TextBox      id    = new TextBox();
        TextBox      name  = new TextBox();
        TextBox      phone = new TextBox();

        public SuppliersForm()
        {
            this.Text            = "Suppliers";
            this.Size            = new Size(860, 570);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.BackColor       = UITheme.Background;

            // ── Header ────────────────────────────────────────────────────
            var header = UITheme.CreateHeader("🏭  Suppliers", "Supplier contacts and management");
            Controls.Add(header);

            // ── Grid ──────────────────────────────────────────────────────
            grid.Location = new Point(20, 95);
            grid.Size     = new Size(810, 220);
            UITheme.StyleGrid(grid);
            Controls.Add(grid);

            // ── Form card ─────────────────────────────────────────────────
            var card = UITheme.CreateCard(20, 330, 810, 185);
            Controls.Add(card);

            var formTitle = UITheme.CreateLabel("Supplier Details", 16, 12);
            formTitle.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            card.Controls.Add(formTitle);
            card.Controls.Add(UITheme.CreateSeparator(16, 34, 778));

            int row1Y = 48, row2Y = 104;

            card.Controls.Add(UITheme.CreateLabel("SUPPLIER ID", 16, row1Y, true));
            UITheme.StyleTextBox(id, 16, row1Y + 18, 220);
            PlaceholderHelper.Set(id, "ID (for update / delete)");
            card.Controls.Add(id);

            card.Controls.Add(UITheme.CreateLabel("SUPPLIER NAME", 256, row1Y, true));
            UITheme.StyleTextBox(name, 256, row1Y + 18, 260);
            PlaceholderHelper.Set(name, "Supplier Name");
            card.Controls.Add(name);

            card.Controls.Add(UITheme.CreateLabel("PHONE", 536, row1Y, true));
            UITheme.StyleTextBox(phone, 536, row1Y + 18, 240);
            PlaceholderHelper.Set(phone, "Phone Number");
            card.Controls.Add(phone);

            var load   = UITheme.CreateButton("⟳  Load",   16,  row2Y, UITheme.BtnStyle.Secondary, 120);
            var add    = UITheme.CreateButton("＋  Add",    146, row2Y, UITheme.BtnStyle.Success,   120);
            var update = UITheme.CreateButton("✎  Update", 276, row2Y, UITheme.BtnStyle.Primary,   120);
            var del    = UITheme.CreateButton("✕  Delete", 406, row2Y, UITheme.BtnStyle.Danger,    120);

            // ── Wiring (UNCHANGED) ────────────────────────────────────────
            load.Click += (s, e) =>
            {
                grid.DataSource = SupplierService.GetAll();
            };

            add.Click += (s, e) =>
            {
                SupplierService.Add(name.Text, phone.Text);
                load.PerformClick();
                MessageBox.Show("Supplier Added");
            };

            update.Click += (s, e) =>
            {
                SupplierService.Update(int.Parse(id.Text), name.Text, phone.Text);
                load.PerformClick();
                MessageBox.Show("Supplier Updated");
            };

            del.Click += (s, e) =>
            {
                SupplierService.Delete(int.Parse(id.Text));
                load.PerformClick();
                MessageBox.Show("Supplier Deleted");
            };

            card.Controls.Add(load);
            card.Controls.Add(add);
            card.Controls.Add(update);
            card.Controls.Add(del);
        }
    }
}
