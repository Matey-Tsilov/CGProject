namespace PaintGD.Model
{
    public class LineShape : Shape
    {
        private Rectangle lineSelectRect; // Specific property for the Line to generate the select rectangle

        public LineShape(int x, int y, int x1, int y1)
        {
            Points = new List<Point>() { new Point(x, y), new Point(x1, y1) };
            ShapeCenter = new Point((int)Points.Average(propa => propa.X), (int)Points.Average(propa => propa.Y));
        }

        public override void DrawShape(Graphics g, Pen p)
        {
            g.DrawLine(p, Points[0], Points[1]);

            // We memorize the color and width of the drawn shape
            DrawnPen = p;
        }
        public override void SelectShape(Graphics g)
        {
            this.IsSelected = true;

            g.DrawRectangle(new Pen(Color.Blue, 2), lineSelectRect);

            // Draw shape center plus sign
            g.DrawLine(new Pen(Color.Red, 1), new Point(ShapeCenter.X, ShapeCenter.Y - 5), new Point(ShapeCenter.X, ShapeCenter.Y + 5));
            g.DrawLine(new Pen(Color.Red, 1), new Point(ShapeCenter.X - 5, ShapeCenter.Y), new Point(ShapeCenter.X + 5, ShapeCenter.Y));
        }
        public override bool IsInBounds(Point click)
        {
            lineSelectRect = new Rectangle(Points[0].X, Points[0].Y, Math.Abs(Points[1].X - Points[0].X), Math.Abs(Points[0].Y - Points[1].Y));
            return lineSelectRect.Contains(click);
        }
    }
}
