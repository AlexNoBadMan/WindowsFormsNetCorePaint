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
        IShape WorkShape;
        public Form1()
        {
            InitializeComponent();
            var isMouseDown = false;
            var pictureBox = new PictureBox();
            pictureBox.BackColor = Color.White;
            //pictureBox.Dock = DockStyle.Fill;
            pictureBox.Size = new Size(500, 250); 
            pictureBox.Image = new Bitmap(500, 250);
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
                    (o as PictureBox).Refresh();
                }
            };
            pictureBox.MouseUp += (o, e) =>
            {
                if (isMouseDown)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        isMouseDown = false;

                        //if (pictureBox.Image == null) pictureBox.Image = new Bitmap(pictureBox.ClientSize.Width, pictureBox.ClientSize.Height);
                        //pictureBox.Image ??= new Bitmap(pictureBox.ClientSize.Width, pictureBox.ClientSize.Height);
                        using (var graphics = Graphics.FromImage(pictureBox.Image))
                        {
                            WorkShape.Draw(graphics, StartPoint, CurrentPoint);
                            pictureBox.Refresh();
                        }
                    }
                }
            };
            var panel = new Panel();
            panel.Dock = DockStyle.Left;
            panel.Width = 30;
            panel.Padding = new Padding(3);
            var lineButton = CreateRadioButton("/");
            var elipseButton = CreateRadioButton("○");
            var rectangleButton = CreateRadioButton("■");
            lineButton.Checked = true;
            lineButton.Click += (o, e) => WorkShape = new Line();
            elipseButton.Click += (o, e) => WorkShape = new Ellipse();
            rectangleButton.Click += (o, e) => WorkShape = new Draw.Rectangle();
            //var button = new Button();
            //button.Text = "Change size";
            //button.Dock = DockStyle.Top;
            //button.Click += (o, e) => 
            //{
            //    var newSize = pictureBox.Image.Size;
            //    var newImage = new Bitmap(pictureBox.Image, new Size(300,300));
            //    pictureBox.Image = newImage;
            //    pictureBox.Refresh();
            //    //using (var graphics = Graphics.FromImage())
            //    //{
            //    //    graphics.DrawImage()
            //    //}
            //};
            panel.Controls.AddRange(new Control[] { /*button, */rectangleButton, elipseButton, lineButton });
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
    }
}
