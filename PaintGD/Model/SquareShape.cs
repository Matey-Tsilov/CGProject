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
            // Toggle button for selecting an element
            this.IsSelected = !this.IsSelected;

            g.DrawRectangle(new Pen(Color.Blue, 5), shape);
        }

        public bool IsInBounds(Point click)
        {
            // return if the click is inside the rectangle or not!
            return shape.Contains(click);
        }
    }
}
