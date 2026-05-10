using System;
using System.Windows.Forms;
using PharmacyManagementSystem.Services;
using PharmacyManagementSystem.Helpers;

namespace PharmacyManagementSystem.Forms
{
    public partial class EmployeesForm : Form
    {
        DataGridView grid = new DataGridView();
        TextBox ssn = new TextBox();
        TextBox name = new TextBox();
        TextBox salary = new TextBox();

        public EmployeesForm()
        {
            this.Text = "Employees";
            this.Size = new System.Drawing.Size(900, 600);

            grid.Dock = DockStyle.Top;
            grid.Height = 200;

            PlaceholderHelper.Set(ssn, "SSN");
            PlaceholderHelper.Set(name, "Name");
            PlaceholderHelper.Set(salary, "Salary");

            ssn.Top = 210;
            name.Top = 240;
            salary.Top = 270;

            ssn.Left = 370;
            name.Left = 370;
            salary.Left = 370;


            Button load = new Button() { Text = "Load", Top = 330, Left = 380 };
            Button add = new Button() { Text = "Add", Top = 360, Left = 380 };
            Button update = new Button() { Text = "Update", Top = 390, Left = 380 };
            Button del = new Button() { Text = "Delete", Top = 420, Left = 380 };

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

            Controls.Add(grid);
            Controls.Add(ssn);
            Controls.Add(name);
            Controls.Add(salary);
            Controls.Add(load);
            Controls.Add(add);
            Controls.Add(update);
            Controls.Add(del);
        }
    }
}