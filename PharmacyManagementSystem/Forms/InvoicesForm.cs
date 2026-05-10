using System;
using System.Drawing;
using System.Windows.Forms;
using PharmacyManagementSystem.Services;
using PharmacyManagementSystem.Helpers;

namespace PharmacyManagementSystem.Forms
{
    public partial class InvoicesForm : Form
    {
        DataGridView grid  = new DataGridView();
        TextBox      cust  = new TextBox();
        TextBox      emp   = new TextBox();
        TextBox      drug  = new TextBox();
        TextBox      qty   = new TextBox();
        TextBox      invId = new TextBox();

        int currentInvoice = -1;
        int itemNo         = 0;

        private void LoadData()
        {
            grid.DataSource = InvoiceService.GetAll();
        }

        public InvoicesForm()
        {
            this.Text            = "Invoices";
            this.Size            = new Size(860, 620);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.BackColor       = UITheme.Background;

            // Header
            var header = UITheme.CreateHeader("🧾  Invoices", "Create and manage sales invoices");
            Controls.Add(header);

            // Status bar
            var statusBar = new Panel { Location = new Point(20, 90), Size = new Size(810, 30), BackColor = UITheme.SurfaceAlt };
            statusBar.Paint += (s, e) => {
                using (var pen = new Pen(UITheme.Border, 1))
                    e.Graphics.DrawRectangle(pen, 0, 0, statusBar.Width - 1, statusBar.Height - 1);
            };

            var statusLbl = new Label { Text = "ℹ  No active invoice. Click 'Create Invoice' to start.", Font = new Font("Segoe UI", 8.5f, FontStyle.Italic), ForeColor = UITheme.TextSecondary, AutoSize = true, Location = new Point(10, 7) };
            statusBar.Controls.Add(statusLbl);
            Controls.Add(statusBar);

            // Grid
            grid.Location = new Point(20, 128);
            grid.Size     = new Size(810, 200);
            UITheme.StyleGrid(grid);
            Controls.Add(grid);

            // Form card
            var card = UITheme.CreateCard(20, 344, 810, 220);
            Controls.Add(card);

            var formTitle = UITheme.CreateLabel("Invoice Details", 16, 12);
            formTitle.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            card.Controls.Add(formTitle);
            card.Controls.Add(UITheme.CreateSeparator(16, 34, 778));

            // Row 1
            int row1Y = 48;
            card.Controls.Add(UITheme.CreateLabel("CUSTOMER ID", 16, row1Y, true));
            UITheme.StyleTextBox(cust, 16, row1Y + 18, 220);
            PlaceholderHelper.Set(cust, "Customer ID");
            card.Controls.Add(cust);

            card.Controls.Add(UITheme.CreateLabel("EMPLOYEE SSN", 256, row1Y, true));
            UITheme.StyleTextBox(emp, 256, row1Y + 18, 240);
            PlaceholderHelper.Set(emp, "Employee SSN");
            card.Controls.Add(emp);

            card.Controls.Add(UITheme.CreateLabel("INVOICE ID (delete)", 516, row1Y, true));
            UITheme.StyleTextBox(invId, 516, row1Y + 18, 240);
            PlaceholderHelper.Set(invId, "Invoice ID");
            card.Controls.Add(invId);

            // Row 2
            int row2Y = 106;
            card.Controls.Add(UITheme.CreateLabel("DRUG CODE", 16, row2Y, true));
            UITheme.StyleTextBox(drug, 16, row2Y + 18, 220);
            PlaceholderHelper.Set(drug, "Drug Code");
            card.Controls.Add(drug);

            card.Controls.Add(UITheme.CreateLabel("QUANTITY", 256, row2Y, true));
            UITheme.StyleTextBox(qty, 256, row2Y + 18, 240);
            PlaceholderHelper.Set(qty, "Qty");
            card.Controls.Add(qty);

            // Buttons
            int row3Y = 164;
            var load    = UITheme.CreateButton("⟳  Load",           16,  row3Y, UITheme.BtnStyle.Secondary, 130);
            var create  = UITheme.CreateButton("＋  Create Invoice", 156, row3Y, UITheme.BtnStyle.Success,   160);
            var addItem = UITheme.CreateButton("➕  Add Item",       326, row3Y, UITheme.BtnStyle.Primary,   130);
            var del     = UITheme.CreateButton("✕  Delete",          466, row3Y, UITheme.BtnStyle.Danger,    120);

            // Wiring (UNCHANGED)
            load.Click += (s, e) => grid.DataSource = InvoiceService.GetAll();

            create.Click += (s, e) =>
            {
                currentInvoice = InvoiceService.CreateInvoice(int.Parse(cust.Text), emp.Text);
                itemNo = 0;
                invId.Text          = currentInvoice.ToString();
                statusLbl.Text      = "✔  Active Invoice: #" + currentInvoice + "  |  Customer: " + cust.Text + "  |  Employee: " + emp.Text;
                statusLbl.ForeColor = UITheme.Success;
                LoadData();
                MessageBox.Show("Invoice Created: " + currentInvoice);
            };

            addItem.Click += (s, e) =>
            {
                try
                {
                    if (currentInvoice == -1) { MessageBox.Show("Create Invoice first"); return; }
                    itemNo++;
                    InvoiceService.AddItem(currentInvoice, itemNo, int.Parse(drug.Text), int.Parse(qty.Text), int.Parse(cust.Text));
                    LoadData();
                    MessageBox.Show("Item Added");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            del.Click += (s, e) =>
            {
                if (string.IsNullOrEmpty(invId.Text)) { MessageBox.Show("Enter Invoice ID"); return; }
                InvoiceService.Delete(int.Parse(invId.Text));
                if (currentInvoice == int.Parse(invId.Text))
                {
                    currentInvoice      = -1;
                    statusLbl.Text      = "ℹ  No active invoice. Click 'Create Invoice' to start.";
                    statusLbl.ForeColor = UITheme.TextSecondary;
                }
                LoadData();
                MessageBox.Show("Invoice Deleted");
            };

            card.Controls.Add(load);
            card.Controls.Add(create);
            card.Controls.Add(addItem);
            card.Controls.Add(del);
        }
    }
}
