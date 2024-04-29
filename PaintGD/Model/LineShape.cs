using System.Drawing.Drawing2D;

namespace PaintGD.Model
{
    public class LineShape : IShape
    {
        private List<Point> _points;
        public IReadOnlyCollection<Point> Points { get => _points.AsReadOnly(); }
        public bool IsSelected { get; set; } = false;

        public LineShape(int x, int y, int x1, int y1)
        {
            _points = new List<Point>() { new Point(x, y), new Point(x1, y1)};
        }

        public void DrawShape(Graphics g, Pen p)
        {
            g.DrawLine(p, _points[0], _points[1]);
        }
        public void SelectShape(Graphics g)
        {
            var selectRect = new Rectangle(_points[0].X, _points[0].Y, Math.Abs(_points[1].X - _points[0].X), Math.Abs(_points[2].Y - _points[1].Y));
            g.DrawRectangle(new Pen(Color.CadetBlue, 5), selectRect);
        }
        public bool IsInBounds(Point click)
        {
            // Create a GraphicsPath object
            using (GraphicsPath path = new GraphicsPath())
            {
                // Add the polygon points to the path
                path.AddPolygon(_points.Select(p => new PointF(p.X, p.Y)).ToArray());

                // Check if the point is inside the bounds of the polygon
                return path.IsVisible(click);
            }
        }
    }
}
