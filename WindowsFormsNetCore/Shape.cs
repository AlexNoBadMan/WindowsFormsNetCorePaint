using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace WindowsFormsNetCore
{
    public interface IShape
    {
        void Draw(Graphics graphics, Point startPoint, Point currentPoint);
    }
}
