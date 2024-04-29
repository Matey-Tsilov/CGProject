using System.Drawing.Drawing2D;

namespace PaintGD.Model
{
    public class SquareShape : IShape
    {
        private Rectangle shape;
        private List<Point> _points;
        public IReadOnlyCollection<Point> Points { get => _points.AsReadOnly(); }
        public bool IsSelected { get; set; } = false;

        public SquareShape(int x, int y, int width, int height)
        {
            _points = new List<Point>() { new Point(x, y), new Point(x + width, y + height)};
            shape = new Rectangle(x, y, width, height);
        }

        public void DrawShape(Graphics g, Pen p)
        {
            g.DrawRectangle(p, shape);
        }

        public void SelectShape(Graphics g)
        {
            g.DrawRectangle(new Pen(Color.CadetBlue, 5), shape);
            IsSelected = true;
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
