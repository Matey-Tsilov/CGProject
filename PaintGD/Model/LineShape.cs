namespace PaintGD.Model
{
    public class LineShape : IShape
    {   
        private List<Point> _points;
        public IReadOnlyCollection<Point> Points { get => _points.AsReadOnly(); }
        public bool IsSelected { get; set; } = false;
        public Pen drawnPen { get; set; }
        public Point ShapeCenter { get; set ; }

        private Rectangle lineSelectRect; // Specific property for the Line to generate the select rectangle

        public LineShape(int x, int y, int x1, int y1)
        {
            _points = new List<Point>() { new Point(x, y), new Point(x1, y1)};
            ShapeCenter = new Point((int)_points.Average(propa => propa.X), (int)_points.Average(propa => propa.Y));
        }

        public void DrawShape(Graphics g, Pen p)
        {
            g.DrawLine(p, _points[0], _points[1]);

            // We memorize the color and width of the drawn shape
            drawnPen = p;
        }
        public void SelectShape(Graphics g)
        {
            // Toggle button for selecting an element
            this.IsSelected = !this.IsSelected;

            g.DrawRectangle(new Pen(Color.Blue, 2), lineSelectRect);

            // Draw shape center plus sign
            g.DrawLine(new Pen(Color.Red, 1), new Point(ShapeCenter.X, ShapeCenter.Y - 5), new Point(ShapeCenter.X, ShapeCenter.Y + 5));
            g.DrawLine(new Pen(Color.Red, 1), new Point(ShapeCenter.X - 5, ShapeCenter.Y), new Point(ShapeCenter.X + 5, ShapeCenter.Y));
        }
        public bool IsInBounds(Point click)
        {
            lineSelectRect = new Rectangle(_points[0].X, _points[0].Y, Math.Abs(_points[1].X - _points[0].X), Math.Abs(_points[0].Y - _points[1].Y));
            return lineSelectRect.Contains(click);
        }
    }
}
