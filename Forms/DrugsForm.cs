using PharmacyManagementSystem.Helpers;
using PharmacyManagementSystem.Services;
using System;
using System.Windows.Forms;

namespace PharmacyManagementSystem.Forms
{
    public partial class DrugsForm : Form
    {
        DataGridView grid = new DataGridView();

        TextBox code = new TextBox();
        TextBox name = new TextBox();
        TextBox price = new TextBox();
        TextBox stock = new TextBox();
        TextBox expiry = new TextBox();
        TextBox categoryId = new TextBox();

        public DrugsForm()
        {
            this.Text = "Drugs";
            this.Size = new System.Drawing.Size(900, 600);

            grid.Dock = DockStyle.Top;
            grid.Height = 200;

            PlaceholderHelper.Set(code, "Code");
            PlaceholderHelper.Set(name, "Name");
            PlaceholderHelper.Set(price, "Price");
            PlaceholderHelper.Set(stock, "Stock");
            PlaceholderHelper.Set(expiry, "Expiry Date");
            PlaceholderHelper.Set(categoryId, "Category ID");

            int startTop = 210;
            int startLeft = 150;
            int spacingY = 40;
            int col2Left = 450;

            code.Top = startTop;
            code.Left = startLeft;

            name.Top = startTop + spacingY;
            name.Left = startLeft;

            price.Top = startTop + spacingY * 2;
            price.Left = startLeft;


            stock.Top = startTop;
            stock.Left = col2Left;

            expiry.Top = startTop + spacingY;
            expiry.Left = col2Left;

            categoryId.Top = startTop + spacingY * 2;
            categoryId.Left = col2Left;

            code.Width = name.Width = price.Width =stock.Width =
            expiry.Width = categoryId.Width = 200;

            Button load = new Button() { Text = "Load", Top = 400, Left= 370};
            Button add = new Button() { Text = "Add", Top = 430, Left = 370 };
            Button update = new Button() { Text = "Update", Top = 460, Left = 370 };
            Button del = new Button() { Text = "Delete", Top = 490, Left = 370 };

            load.Click += (s, e) =>
            {
                grid.DataSource = DrugService.GetAll();
            };

            add.Click += (s, e) =>
            {
                DrugService.Add( int.Parse(code.Text), name.Text, double.Parse(price.Text), 
                    int.Parse(stock.Text), expiry.Text, int.Parse(categoryId.Text) );

                load.PerformClick();
            };

            update.Click += (s, e) =>
            {
                int drugCode = int.Parse(code.Text);

                DrugService.Update(drugCode, name.Text, double.Parse(price.Text), 
                    int.Parse(stock.Text), expiry.Text, int.Parse(categoryId.Text) );

                load.PerformClick();
            };

            del.Click += (s, e) =>
            {
                int drugCode = int.Parse(code.Text);

                DrugService.Delete(drugCode);

                load.PerformClick();
            };
            Controls.Add(grid);

            Controls.Add(code);
            Controls.Add(name);
            Controls.Add(price);
            Controls.Add(stock);
            Controls.Add(expiry);
            Controls.Add(categoryId);

            Controls.Add(load);
            Controls.Add(add);
            Controls.Add(update);
            Controls.Add(del);
        }
    }
}