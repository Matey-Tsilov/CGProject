using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintGD.Model
{
    public class EllipseShape : IShape
    {
        private Rectangle shape;
        private List<Point> _points;
        public IReadOnlyCollection<Point> Points { get => _points.AsReadOnly(); }

        public EllipseShape(int x, int y, int width, int height)
        {
            _points = new List<Point>() { new Point(x, y), new Point(x + width, y + height) };
            shape = new Rectangle(x, y, width, height);
        }

        public void DrawShape(Graphics g, Pen p)
        {
            g.DrawEllipse(p, shape);
        }
    }
}
