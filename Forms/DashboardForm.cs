using PharmacyManagementSystem.Forms;
using System;
using System.Windows.Forms;

namespace PharmacyManagementSystem.Forms
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            this.Text = "Dashboard";
            this.Size = new System.Drawing.Size(400, 300);

            Button emp = new Button() { Text = "Employees", Top = 30, Left = 160 };
            Button cust = new Button() { Text = "Customers", Top = 80, Left = 160 };
            Button drug = new Button() { Text = "Drugs", Top = 130, Left = 160 };
            Button inv = new Button() { Text = "Invoices", Top = 180, Left = 160 };

            emp.Click += (s, e) => new EmployeesForm().Show();
            cust.Click += (s, e) => new CustomersForm().Show();
            drug.Click += (s, e) => new DrugsForm().Show();
            inv.Click += (s, e) => new InvoicesForm().Show();

            Controls.Add(emp);
            Controls.Add(cust);
            Controls.Add(drug);
            Controls.Add(inv);
        }
    }
}
