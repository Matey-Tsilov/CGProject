namespace PaintGD.Model
{
    public class EllipseShape : IShape
    {
        public Rectangle Shape { get; set; }
        public List<Point> Points { get; set; }
        public bool IsSelected { get; set; } = false;
        public Pen drawnPen { get; set; }
        public Point ShapeCenter {get; set;}

        public EllipseShape(int x, int y, int width, int height)
        {
            Points = new List<Point>() { new Point(x, y), new Point(x + width, y + height) };
            Shape = new Rectangle(x, y, width, height);
            ShapeCenter = new Point((int)Points.Average(propa => propa.X), (int)Points.Average(propa => propa.Y));
        }

        public void DrawShape(Graphics g, Pen p)
        {
            g.DrawEllipse(p, Shape);

            // We memorize the color and width of the drawn shape
            drawnPen = p;
        }
        public void SelectShape(Graphics g)
        {
            // Toggle button for selecting an element
            this.IsSelected = !this.IsSelected;

            g.DrawRectangle(new Pen(Color.Blue, 5), Shape);

            // Draw shape center plus sign
            g.DrawLine(new Pen(Color.Red, 1), new Point(ShapeCenter.X, ShapeCenter.Y - 5), new Point(ShapeCenter.X, ShapeCenter.Y + 5));
            g.DrawLine(new Pen(Color.Red, 1), new Point(ShapeCenter.X - 5, ShapeCenter.Y), new Point(ShapeCenter.X + 5, ShapeCenter.Y));

        }
        public bool IsInBounds(Point click)
        {
            // return if the click is inside the rectangle or not!
            return Shape.Contains(click);
        }

        // newCenter will be the newPoint to which our shape is dragged to
        public void DragShapeTo(Point newCenter)
        {

        }
    }
}
