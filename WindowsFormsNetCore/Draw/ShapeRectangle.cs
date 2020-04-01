using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace WindowsFormsNetCorePaint.Draw
{
    public class ShapeRectangle : IShape
    {
        void IShape.Draw(Graphics graphics, Point startPoint, Point currentPoint)
        {
            var rectangle = new System.Drawing.Rectangle();
            if ((startPoint.X > currentPoint.X) && (startPoint.Y > currentPoint.Y))
            {
                rectangle = new System.Drawing.Rectangle(currentPoint.X, currentPoint.Y, startPoint.X - currentPoint.X, startPoint.Y - currentPoint.Y);
            }
            else if (startPoint.X > currentPoint.X)
            {
                rectangle = new System.Drawing.Rectangle(currentPoint.X, startPoint.Y, startPoint.X - currentPoint.X, currentPoint.Y - startPoint.Y);
            }
            else if (startPoint.Y > currentPoint.Y)
            {
                rectangle = new System.Drawing.Rectangle(startPoint.X, currentPoint.Y, currentPoint.X - startPoint.X, startPoint.Y - currentPoint.Y);
            }
            else
            {
                rectangle = new System.Drawing.Rectangle(startPoint.X, startPoint.Y, currentPoint.X - startPoint.X, currentPoint.Y - startPoint.Y);
            }
            graphics.FillRectangle(new SolidBrush(Color.Black), rectangle);
        }
    }
}
