namespace PaintGD.Model
{
    public class LineShape : IShape
    {   
        private List<Point> _points;
        public IReadOnlyCollection<Point> Points { get => _points.AsReadOnly(); }
        public bool IsSelected { get; set; } = false;
        public Pen drawnPen { get; set; }

        private Rectangle lineSelectRect; // Specific property for the Line to generate the select rectangle

        public LineShape(int x, int y, int x1, int y1)
        {
            _points = new List<Point>() { new Point(x, y), new Point(x1, y1)};
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
        }
        public bool IsInBounds(Point click)
        {
            lineSelectRect = new Rectangle(_points[0].X, _points[0].Y, Math.Abs(_points[1].X - _points[0].X), Math.Abs(_points[0].Y - _points[1].Y));
            return lineSelectRect.Contains(click);
        }
    }
}
