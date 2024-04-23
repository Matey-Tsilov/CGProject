using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintGD.Model
{
    public class LineShape : IShape
    {
        private List<Point> _points;
        public IReadOnlyCollection<Point> Points { get => _points.AsReadOnly(); }

        public LineShape(int x, int y, int x1, int y1)
        {
            _points = new List<Point>() { new Point(x, y), new Point(x1 - x, y1 - y)};
        }

        public void DrawShape(Graphics g, Pen p)
        {
            g.DrawLine(p, _points[0], _points[1]);
        }
    }
}
