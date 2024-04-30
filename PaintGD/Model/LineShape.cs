namespace PaintGD.Model
{
    public class LineShape : IShape
    {   
        public List<Point> Points { get; set; }
        public bool IsSelected { get; set; } = false;
        public Pen drawnPen { get; set; }
        public Point ShapeCenter { get; set ; }

        private Rectangle lineSelectRect; // Specific property for the Line to generate the select rectangle

        public LineShape(int x, int y, int x1, int y1)
        {
            Points = new List<Point>() { new Point(x, y), new Point(x1, y1)};
            ShapeCenter = new Point((int)Points.Average(propa => propa.X), (int)Points.Average(propa => propa.Y));
        }

        public void DrawShape(Graphics g, Pen p)
        {
            g.DrawLine(p, Points[0], Points[1]);

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
            lineSelectRect = new Rectangle(Points[0].X, Points[0].Y, Math.Abs(Points[1].X - Points[0].X), Math.Abs(Points[0].Y - Points[1].Y));
            return lineSelectRect.Contains(click);
        }
    }
}
