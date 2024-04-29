namespace PaintGD.Model
{
    public class LineShape : IShape
    {
        public List<Point> linePoints { get; set; } // This is a LIne specific option as there is no way to know if a click is on the line other then to compare all point of the line
        
        private List<Point> _points;
        public IReadOnlyCollection<Point> Points { get => _points.AsReadOnly(); }
        public bool IsSelected { get; set; } = false;

        public LineShape(int x, int y, int x1, int y1)
        {
            _points = new List<Point>() { new Point(x, y), new Point(x1, y1)};
            linePoints = new List<Point>();
        }

        public void DrawShape(Graphics g, Pen p)
        {
            g.DrawLine(p, _points[0], _points[1]);
        }
        public void SelectShape(Graphics g)
        {
            // Toggle button for selecting an element
            this.IsSelected = !this.IsSelected;

            var selectRect = new Rectangle(_points[0].X, _points[0].Y, Math.Abs(_points[1].X - _points[0].X), Math.Abs(_points[2].Y - _points[1].Y));
            g.DrawRectangle(new Pen(Color.Blue, 5), selectRect);
        }
        public bool IsInBounds(Point click)
        {
            return linePoints.Any(p => p.X == click.X && p.Y == click.Y);
        }
    }
}
