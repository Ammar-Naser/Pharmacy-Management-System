using System.Drawing;
using System.Windows.Forms;

namespace PharmacyManagementSystem
{
    public static class UITheme
    {
        // Color Palette
        public static readonly Color Primary       = Color.FromArgb(15, 76, 129);    // Deep navy blue
        public static readonly Color PrimaryLight  = Color.FromArgb(22, 110, 185);   // Lighter blue
        public static readonly Color Accent        = Color.FromArgb(0, 188, 170);    // Teal accent
        public static readonly Color Background    = Color.FromArgb(245, 247, 252);  // Off-white bg
        public static readonly Color Surface       = Color.White;
        public static readonly Color SurfaceAlt    = Color.FromArgb(235, 240, 250);  // Subtle blue-tint
        public static readonly Color TextPrimary   = Color.FromArgb(18, 32, 58);     // Near-black
        public static readonly Color TextSecondary = Color.FromArgb(100, 116, 148);  // Muted blue-gray
        public static readonly Color Border        = Color.FromArgb(210, 220, 240);  // Soft border
        public static readonly Color Success       = Color.FromArgb(16, 172, 132);
        public static readonly Color Danger        = Color.FromArgb(220, 60, 75);
        public static readonly Color Warning       = Color.FromArgb(245, 158, 11);
        public static readonly Color BtnText       = Color.White;

        // Fonts
        public static readonly Font TitleFont    = new Font("Segoe UI", 20f, FontStyle.Bold);
        public static readonly Font SubtitleFont = new Font("Segoe UI", 11f, FontStyle.Regular);
        public static readonly Font LabelFont    = new Font("Segoe UI", 9f,  FontStyle.Bold);
        public static readonly Font InputFont    = new Font("Segoe UI", 10f, FontStyle.Regular);
        public static readonly Font BtnFont      = new Font("Segoe UI", 9f,  FontStyle.Bold);
        public static readonly Font GridFont     = new Font("Segoe UI", 9f,  FontStyle.Regular);
        public static readonly Font GridHeader   = new Font("Segoe UI", 9f,  FontStyle.Bold);

        /// <summary>Apply global styling to a Form.</summary>
        public static void ApplyForm(Form form, string title = null)
        {
            form.BackColor = Background;
            form.Font = InputFont;
            if (title != null) form.Text = title;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.MaximizeBox = false;
        }

        /// <summary>Create a styled header panel.</summary>
        public static Panel CreateHeader(string title, string subtitle = null)
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = subtitle != null ? 80 : 60,
                BackColor = Primary,
                Padding = new Padding(20, 0, 0, 0)
            };

            var lbl = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, subtitle != null ? 14 : 16)
            };
            panel.Controls.Add(lbl);

            if (subtitle != null)
            {
                var sub = new Label
                {
                    Text = subtitle,
                    Font = new Font("Segoe UI", 9f, FontStyle.Regular),
                    ForeColor = Color.FromArgb(180, 210, 240),
                    AutoSize = true,
                    Location = new Point(22, 44)
                };
                panel.Controls.Add(sub);
            }

            return panel;
        }

        /// <summary>Create a card-style panel.</summary>
        public static Panel CreateCard(int x, int y, int w, int h)
        {
            var card = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(w, h),
                BackColor = Surface,
                Padding = new Padding(16)
            };
            card.Paint += (s, e) =>
            {
                var g = e.Graphics;
                using (var pen = new System.Drawing.Pen(Border, 1))
                    g.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
            };
            return card;
        }

        /// <summary>Create a styled label.</summary>
        public static Label CreateLabel(string text, int x, int y, bool isFieldLabel = false)
        {
            return new Label
            {
                Text = text,
                Font = isFieldLabel ? LabelFont : SubtitleFont,
                ForeColor = isFieldLabel ? TextSecondary : TextPrimary,
                AutoSize = true,
                Location = new Point(x, y)
            };
        }

        /// <summary>Style a TextBox.</summary>
        public static void StyleTextBox(TextBox tb, int x, int y, int width = 200)
        {
            tb.Location = new Point(x, y);
            tb.Size = new Size(width, 32);
            tb.Font = InputFont;
            tb.ForeColor = TextPrimary;
            tb.BackColor = SurfaceAlt;
            tb.BorderStyle = BorderStyle.FixedSingle;
        }

        public enum BtnStyle { Primary, Success, Danger, Secondary }

        /// <summary>Create a styled button.</summary>
        public static Button CreateButton(string text, int x, int y, BtnStyle style = BtnStyle.Primary, int width = 100)
        {
            Color bg = style == BtnStyle.Primary   ? PrimaryLight
                     : style == BtnStyle.Success   ? Success
                     : style == BtnStyle.Danger    ? Danger
                                                   : Color.FromArgb(180, 195, 220);

            Color hover = style == BtnStyle.Primary   ? Primary
                        : style == BtnStyle.Success   ? Color.FromArgb(10, 140, 100)
                        : style == BtnStyle.Danger    ? Color.FromArgb(180, 40, 55)
                                                      : Color.FromArgb(150, 170, 200);

            var btn = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, 34),
                Font = BtnFont,
                ForeColor = style == BtnStyle.Secondary ? TextPrimary : BtnText,
                BackColor = bg,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = hover;
            btn.FlatAppearance.MouseDownBackColor = hover;
            return btn;
        }

        /// <summary>Style a DataGridView.</summary>
        public static void StyleGrid(DataGridView grid)
        {
            grid.BackgroundColor = Surface;
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.GridColor = Border;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.AllowUserToAddRows = false;
            grid.ReadOnly = true;
            grid.Font = GridFont;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 230, 255);
            grid.DefaultCellStyle.SelectionForeColor = TextPrimary;
            grid.DefaultCellStyle.BackColor = Surface;
            grid.DefaultCellStyle.ForeColor = TextPrimary;
            grid.DefaultCellStyle.Padding = new Padding(4, 4, 4, 4);
            grid.AlternatingRowsDefaultCellStyle.BackColor = SurfaceAlt;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Primary;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = GridHeader;
            grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(4, 6, 4, 6);
            grid.ColumnHeadersHeight = 36;
            grid.RowTemplate.Height = 32;
            grid.EnableHeadersVisualStyles = false;
        }

        /// <summary>Create a separator line.</summary>
        public static Panel CreateSeparator(int x, int y, int width)
        {
            return new Panel
            {
                Location = new Point(x, y),
                Size = new Size(width, 1),
                BackColor = Border
            };
        }
    }
}
