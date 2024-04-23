using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintGD.Model
{
    public class SquareShape : IShape
    {
        private Rectangle shape;
        private List<Point> _points;
        public IReadOnlyCollection<Point> Points { get => _points.AsReadOnly(); }

        public SquareShape(int x, int y, int x1, int y1)
        {
            _points = new List<Point>() { new Point(x, y), new Point(x1 - x, y1 - y)};
            shape = new Rectangle(x, y, x1, y1);
        }

        public void DrawShape(Graphics g, Pen p)
        {
            g.DrawRectangle(p, shape);
        }
    }
}
