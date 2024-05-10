namespace PaintGD.Model
{
    public class SquareShape : Shape
    {
        public Rectangle Shape { get; set; }
        public SquareShape(int x, int y, int width, int height)
        {
            Points = new List<Point>() { new Point(x, y), new Point(x + width, y + height) };
            Shape = new Rectangle(x, y, width, height);
            ShapeCenter = new Point((int)Points.Average(propa => propa.X), (int)Points.Average(propa => propa.Y));
        }
        public SquareShape(Point center, int halfWidth, int halfHeight)
        {
            // We need those Points in all shapes to calculate the width, while dragging
            Points = new List<Point>() {
                new Point(center.X - halfWidth, center.Y - halfHeight),
                new Point(center.X + halfWidth, center.Y + halfHeight)
            };
            Shape = new Rectangle(center.X - halfWidth, center.Y - halfHeight, halfWidth * 2, halfHeight * 2);
            ShapeCenter = center;
        }

        public override void DrawShape(Graphics g, Pen p)
        {
            g.DrawRectangle(p, Shape);

            // We memorize the color and width of the drawn shape
            DrawnPen = p;
        }

        public override void SelectShape(Graphics g)
        {
            IsSelected = true;

            g.DrawRectangle(new Pen(Color.Blue, 2), Shape);

            // Draw shape center plus sign
            g.DrawLine(new Pen(Color.Red, 1), new Point(ShapeCenter.X, ShapeCenter.Y - 5), new Point(ShapeCenter.X, ShapeCenter.Y + 5));
            g.DrawLine(new Pen(Color.Red, 1), new Point(ShapeCenter.X - 5, ShapeCenter.Y), new Point(ShapeCenter.X + 5, ShapeCenter.Y));
        }

        public override bool IsInBounds(Point click)
        {
            // return if the click is inside the rectangle or not!
            return Shape.Contains(click);
        }
    }
}
