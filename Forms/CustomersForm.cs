using PharmacyManagementSystem.Helpers;
using PharmacyManagementSystem.Services;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PharmacyManagementSystem.Forms
{
    public partial class CustomersForm : Form
    {
        DataGridView grid = new DataGridView();
        TextBox name = new TextBox();
        TextBox phone = new TextBox();
        TextBox id = new TextBox();

        public CustomersForm()
        {
            this.Text = "Customers";
            this.Size = new System.Drawing.Size(900, 600);

            grid.Dock = DockStyle.Top;
            grid.Height = 200;

            PlaceholderHelper.Set(name, "Name");
            PlaceholderHelper.Set(phone, "Phone");
            PlaceholderHelper.Set(id, "ID");

            id.Top = 210;
            name.Top = 240;
            phone.Top = 270;

            id.Left = 370;
            name.Left = 370;
            phone.Left = 370;


            Button load = new Button() { Text = "Load", Top = 330, Left = 380 };
            Button add = new Button() { Text = "Add", Top = 360, Left = 380 };
            Button update = new Button() { Text = "Update", Top = 390, Left = 380 };
            Button del = new Button() { Text = "Delete", Top = 420, Left = 380 };

            load.Click += (s, e) => grid.DataSource = CustomerService.GetAll();

            add.Click += (s, e) =>
            {
                CustomerService.Add(name.Text, phone.Text);
                load.PerformClick();
            };

            update.Click += (s, e) =>
            {
                int customerId = int.Parse(id.Text);
                CustomerService.Update(customerId, name.Text, phone.Text);
                load.PerformClick();
            };

            del.Click += (s, e) =>
            {
                int customerId = int.Parse(id.Text);
                CustomerService.Delete(customerId);
                load.PerformClick();
            };

            Controls.Add(grid);
            Controls.Add(name);
            Controls.Add(phone);
            Controls.Add(id);
            Controls.Add(load);
            Controls.Add(add);
            Controls.Add(update);
            Controls.Add(del);
        }
    }
}
