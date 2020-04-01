using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsNetCorePaint.Draw;

namespace WindowsFormsNetCorePaint
{
    public partial class Form1 : Form
    {
        //PictureBox pictureBox = new PictureBox();
        Point StartPoint;
        Point CurrentPoint;
        IShape WorkShape = new Line();
        public Form1()
        {
            InitializeComponent();
            var isMouseDown = false;
            var pictureBox = new PictureBox();
            pictureBox.BackColor = Color.White;
            //pictureBox.Dock = DockStyle.Fill;
            pictureBox.Location = new Point(0, 25);
            pictureBox.Size = new Size(500, 250); 
            pictureBox.Image = new Bitmap(500, 250);
            pictureBox.SizeChanged += (o, e) =>
            {
                MessageBox.Show($"{((PictureBox)o).Size} - {pictureBox.Size}");
                var newImage = new Bitmap(pictureBox.Image, pictureBox.Size);
                pictureBox.Image = newImage;
                pictureBox.Refresh();
            };
            //pictureBox.Resize += (o, e) => 
            //{
            //    var newImage = new Bitmap(pictureBox.Image, pictureBox.Image.Size);
            //    pictureBox.Image = newImage;
            //    pictureBox.Refresh();
            //};
            pictureBox.Paint += (o, e) => WorkShape.Draw(e.Graphics, StartPoint, CurrentPoint);
            pictureBox.MouseDown += (o, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    isMouseDown = true;
                    StartPoint = e.Location;
                }

            };
            pictureBox.MouseMove += (o, e) =>
            {
                if (isMouseDown)
                {
                    CurrentPoint = e.Location;
                    ((PictureBox)o).Refresh();
                }
            };
            pictureBox.MouseUp += (o, e) =>
            {
                if (isMouseDown)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        isMouseDown = false;
                        using var graphics = Graphics.FromImage(pictureBox.Image);
                        WorkShape.Draw(graphics, StartPoint, CurrentPoint);
                        pictureBox.Refresh();
                    }
                }
            };
            var button = new Button();
            button.Text = "Change size";
            button.Dock = DockStyle.Top;
            button.Click += (o, e) =>
            {
                pictureBox.Size = new Size(600, 400);
                //pictureBox.Image = newImage;
                //pictureBox.Refresh();
                //using (var graphics = Graphics.FromImage())
                //{
                //    graphics.DrawImage()
                //}
            };
            var panel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 30,
                Padding = new Padding(3)
            };
            var lineButton = CreateRadioButton("/");
            var elipseButton = CreateRadioButton("○");
            var rectangleButton = CreateRadioButton("■");
            lineButton.Checked = true;
            lineButton.Click += (o, e) => WorkShape = new Line();
            elipseButton.Click += (o, e) => WorkShape = new Ellipse();
            rectangleButton.Click += (o, e) => WorkShape = new ShapeRectangle();

            //this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            panel.Controls.AddRange(new Control[] { button, rectangleButton, elipseButton, lineButton });
            this.Controls.AddRange(new Control[] { panel, pictureBox });
        }

        private static RadioButton CreateRadioButton(string textButton)
        {
            return new RadioButton
            {
                Appearance = Appearance.Button,
                Text = textButton,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter
            };
        }

        private const int cGrip = 32;      // Grip size
        private const int cCaption = 16;   // Caption bar height;
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Black, rc);
            //rc = new Rectangle(0, 0, this.ClientSize.Width, cCaption);
            //e.Graphics.FillRectangle(Brushes.DarkBlue, rc);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {  // Trap WM_NCHITTEST
                Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 32);
                pos = this.PointToClient(pos);
                if (pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;  // HTCAPTION
                    return;
                }
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                    return;
                }
            }
            base.WndProc(ref m);
        }
    }
}
