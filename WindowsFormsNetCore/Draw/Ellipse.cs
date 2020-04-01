using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace WindowsFormsNetCorePaint.Draw
{
    public class Ellipse : IShape
    {
        public void Draw(Graphics graphics, Point startPoint, Point currentPoint)
        {
            graphics.DrawEllipse(Pens.Black, startPoint.X, startPoint.Y, currentPoint.X - startPoint.X, currentPoint.Y - startPoint.Y);
        }
    }
}
