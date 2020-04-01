using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace WindowsFormsNetCore.Draw
{
    public class Line : IShape
    {

        public void Draw(Graphics graphics, Point startPoint, Point currentPoint)
        {
            graphics.DrawLine(Pens.Black, startPoint, currentPoint); 
        }
    }
}
