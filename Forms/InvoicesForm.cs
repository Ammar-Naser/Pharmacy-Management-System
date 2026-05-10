using System;
using System.Windows.Forms;
using PharmacyManagementSystem.Services;
using PharmacyManagementSystem.Helpers;

namespace PharmacyManagementSystem.Forms
{
    public partial class InvoicesForm : Form
    {
        DataGridView grid = new DataGridView();

        TextBox cust = new TextBox();
        TextBox emp = new TextBox();
        TextBox drug = new TextBox();
        TextBox qty = new TextBox();
        TextBox invId = new TextBox(); 

        int currentInvoice = -1;
        int itemNo = 0;
        private void LoadData()
        {
            grid.DataSource = InvoiceService.GetAll();
        }
        public InvoicesForm()
        {
            this.Text = "Invoices";
            this.Size = new System.Drawing.Size(900, 600);

            grid.Dock = DockStyle.Top;
            grid.Height = 250;

            PlaceholderHelper.Set(cust, "Customer ID");
            PlaceholderHelper.Set(emp, "Employee SSN");
            PlaceholderHelper.Set(drug, "Drug Code");
            PlaceholderHelper.Set(qty, "Quantity");
            PlaceholderHelper.Set(invId, "Invoice ID ( for delete )");

            int top = 270;
            int left = 20;
            int spacing = 200;

            cust.SetBounds(left, top, 150, 25);
            emp.SetBounds(left + spacing, top, 150, 25);
            invId.SetBounds(left + spacing * 2, top, 150, 25);

            drug.SetBounds(left, top + 40, 150, 25);
            qty.SetBounds(left + spacing, top + 40, 150, 25);

            Button load = new Button() { Text = "Load", Top = 380, Left = 200 };
            Button create = new Button() { Text = "Create", Top = 380, Left = 300 };
            Button addItem = new Button() { Text = "Add Item", Top = 380, Left = 400 };
            Button delete = new Button() { Text = "Delete", Top = 380, Left = 500 };

            load.Click += (s, e) =>
            {
                grid.DataSource = InvoiceService.GetAll();
            };

            create.Click += (s, e) =>
            {
                currentInvoice = InvoiceService.CreateInvoice(
                    int.Parse(cust.Text),
                    emp.Text
                );
                itemNo = 0;

                invId.Text = currentInvoice.ToString();
                LoadData();
                MessageBox.Show("Invoice Created: " + currentInvoice);
            };

            addItem.Click += (s, e) =>
            {
                try
                {
                    if (currentInvoice == -1)
                    {
                        MessageBox.Show("Create Invoice first");
                        return;
                    }

                    itemNo++;

                    InvoiceService.AddItem(currentInvoice, itemNo, int.Parse(drug.Text),
                                            int.Parse(qty.Text), int.Parse(cust.Text) );

                    LoadData();
                    MessageBox.Show("Item Added");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            delete.Click += (s, e) =>
            {
                if (string.IsNullOrEmpty(invId.Text))
                {
                    MessageBox.Show("Enter Invoice ID");
                    return;
                }

                InvoiceService.Delete(int.Parse(invId.Text));
                LoadData();
                MessageBox.Show("Invoice Deleted");
                
            };

            Controls.Add(grid);
            Controls.Add(cust);
            Controls.Add(emp);
            Controls.Add(invId);
            Controls.Add(drug);
            Controls.Add(qty);

            Controls.Add(load);
            Controls.Add(create);
            Controls.Add(addItem);
            Controls.Add(delete);
        }
    }
}