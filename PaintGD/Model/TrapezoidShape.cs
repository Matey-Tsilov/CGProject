using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintGD.Model
{
    public class TrapezoidShape : IShape
    {
        private List<Point> _points;
        public IReadOnlyCollection<Point> Points { get => _points.AsReadOnly(); }

        public TrapezoidShape(int x, int y, int x1, int y1,int x2, int y2, int x3, int y3)
        {
            _points = new List<Point>() { new Point(x, y), new Point(x1, y1), new Point(x2, y2), new Point(x3, y3)};
        }

        public void DrawShape(Graphics g, Pen p)
        {
            PointF[] pointsF = _points.Select(p => new PointF(p.X, p.Y)).ToArray();
            g.DrawPolygon(p, pointsF);
        }
    }
}
