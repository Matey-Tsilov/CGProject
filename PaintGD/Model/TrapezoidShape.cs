using System.Drawing.Drawing2D;

namespace PaintGD.Model
{
    public class TrapezoidShape : IShape
    {
        private List<Point> _points;
        public IReadOnlyCollection<Point> Points { get => _points.AsReadOnly(); }
        public bool IsSelected { get; set; } = false;
        public Pen drawnPen { get; set; }
        public Point ShapeCenter { get; set; }

        public TrapezoidShape(int x, int y, int x1, int y1,int x2, int y2, int x3, int y3)
        {
            _points = new List<Point>() { new Point(x, y), new Point(x1, y1), new Point(x2, y2), new Point(x3, y3)};
            ShapeCenter = new Point((x + x1) / 2, (y - y1) / 2);
        }

        public void DrawShape(Graphics g, Pen p)
        {
            PointF[] pointsF = _points.Select(p => new PointF(p.X, p.Y)).ToArray();
            g.DrawPolygon(p, pointsF);

            // We memorize the color and width of the drawn shape
            drawnPen = p;
        }

        public void SelectShape(Graphics g)
        {
            // Toggle button for selecting an element
            this.IsSelected = !this.IsSelected;

            var selectRect = new Rectangle(
                _points[0].X, _points[3].Y, 
                Math.Abs(_points[1].X - _points[0].X), 
                Math.Abs(_points[1].Y - _points[2].Y));

            g.DrawRectangle(new Pen(Color.Blue, 5), selectRect);

            // Draw shape center plus sign
            g.DrawLine(new Pen(Color.Red, 1), new Point(ShapeCenter.X, ShapeCenter.Y - 5), new Point(ShapeCenter.X, ShapeCenter.Y + 5));
            g.DrawLine(new Pen(Color.Red, 1), new Point(ShapeCenter.X - 5, ShapeCenter.Y), new Point(ShapeCenter.X + 5, ShapeCenter.Y));
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
