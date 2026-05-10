using PharmacyManagementSystem.Helpers;
using PharmacyManagementSystem.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PharmacyManagementSystem.Forms
{
    public partial class DrugsForm : Form
    {
        DataGridView grid       = new DataGridView();
        TextBox      code       = new TextBox();
        TextBox      name       = new TextBox();
        TextBox      price      = new TextBox();
        TextBox      stock      = new TextBox();
        TextBox      expiry     = new TextBox();
        TextBox      categoryId = new TextBox();

        public DrugsForm()
        {
            this.Text            = "Drugs";
            this.Size            = new Size(860, 620);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.BackColor       = UITheme.Background;

            // ── Header ────────────────────────────────────────────────────
            var header = UITheme.CreateHeader("💊  Drugs", "Drug inventory, pricing, and expiry management");
            Controls.Add(header);

            // ── Grid ──────────────────────────────────────────────────────
            grid.Location = new Point(20, 95);
            grid.Size     = new Size(810, 220);
            UITheme.StyleGrid(grid);
            Controls.Add(grid);

            // ── Form card ─────────────────────────────────────────────────
            var card = UITheme.CreateCard(20, 330, 810, 230);
            Controls.Add(card);

            var formTitle = UITheme.CreateLabel("Drug Details", 16, 12);
            formTitle.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            card.Controls.Add(formTitle);
            card.Controls.Add(UITheme.CreateSeparator(16, 34, 778));

            // Row 1: Code, Name, Price
            int row1Y = 48;
            card.Controls.Add(UITheme.CreateLabel("DRUG CODE", 16, row1Y, true));
            UITheme.StyleTextBox(code, 16, row1Y + 18, 200);
            PlaceholderHelper.Set(code, "e.g. 1001");
            card.Controls.Add(code);

            card.Controls.Add(UITheme.CreateLabel("DRUG NAME", 236, row1Y, true));
            UITheme.StyleTextBox(name, 236, row1Y + 18, 240);
            PlaceholderHelper.Set(name, "Drug Name");
            card.Controls.Add(name);

            card.Controls.Add(UITheme.CreateLabel("PRICE (EGP)", 496, row1Y, true));
            UITheme.StyleTextBox(price, 496, row1Y + 18, 200);
            PlaceholderHelper.Set(price, "e.g. 49.99");
            card.Controls.Add(price);

            // Row 2: Stock, Expiry, Category
            int row2Y = 106;
            card.Controls.Add(UITheme.CreateLabel("STOCK QTY", 16, row2Y, true));
            UITheme.StyleTextBox(stock, 16, row2Y + 18, 200);
            PlaceholderHelper.Set(stock, "Units in stock");
            card.Controls.Add(stock);

            card.Controls.Add(UITheme.CreateLabel("EXPIRY DATE", 236, row2Y, true));
            UITheme.StyleTextBox(expiry, 236, row2Y + 18, 240);
            PlaceholderHelper.Set(expiry, "yyyy-MM-dd");
            card.Controls.Add(expiry);

            card.Controls.Add(UITheme.CreateLabel("CATEGORY ID", 496, row2Y, true));
            UITheme.StyleTextBox(categoryId, 496, row2Y + 18, 200);
            PlaceholderHelper.Set(categoryId, "Category ID");
            card.Controls.Add(categoryId);

            // Buttons row
            int row3Y = 166;
            var load   = UITheme.CreateButton("⟳  Load",   16,  row3Y, UITheme.BtnStyle.Secondary, 120);
            var add    = UITheme.CreateButton("＋  Add",    146, row3Y, UITheme.BtnStyle.Success,   120);
            var update = UITheme.CreateButton("✎  Update", 276, row3Y, UITheme.BtnStyle.Primary,   120);
            var del    = UITheme.CreateButton("✕  Delete", 406, row3Y, UITheme.BtnStyle.Danger,    120);

            // ── Wiring (UNCHANGED) ────────────────────────────────────────
            load.Click += (s, e) =>
            {
                grid.DataSource = DrugService.GetAll();
            };

            add.Click += (s, e) =>
            {
                DrugService.Add(int.Parse(code.Text), name.Text, double.Parse(price.Text),
                    int.Parse(stock.Text), expiry.Text, int.Parse(categoryId.Text));
                load.PerformClick();
            };

            update.Click += (s, e) =>
            {
                int drugCode = int.Parse(code.Text);
                DrugService.Update(drugCode, name.Text, double.Parse(price.Text),
                    int.Parse(stock.Text), expiry.Text, int.Parse(categoryId.Text));
                load.PerformClick();
            };

            del.Click += (s, e) =>
            {
                int drugCode = int.Parse(code.Text);
                DrugService.Delete(drugCode);
                load.PerformClick();
            };

            card.Controls.Add(load);
            card.Controls.Add(add);
            card.Controls.Add(update);
            card.Controls.Add(del);
        }
    }
}
