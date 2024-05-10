using System.Drawing.Drawing2D;

namespace PaintGD.Model
{
    public class TriangleShape : Shape
    {
        public TriangleShape(int x, int y, int x1, int y1, int x2, int y2)
        {
            Points = new List<Point>() { new Point(x, y), new Point(x1, y1), new Point(x2, y2) };
            ShapeCenter = new Point((x + x1) / 2, (y + y2) / 2);
        }

        public TriangleShape(Point center, int halfWidth, int halfHeight)
        {
            // We need those Points in all shapes to calculate the width, while dragging
            Points = new List<Point>() {
                new Point(center.X - halfWidth, center.Y + halfHeight),
                new Point(center.X + halfWidth, center.Y + halfHeight),
                new Point(center.X, center.Y - halfHeight),
            };
            ShapeCenter = center;
        }

        public override void DrawShape(Graphics g, Pen p)
        {
            PointF[] pointsF = Points.Select(p => new PointF(p.X, p.Y)).ToArray();
            g.DrawPolygon(p, pointsF);

            // We memorize the color and width of the drawn shape
            DrawnPen = p;
        }
        public override void SelectShape(Graphics g)
        {
            this.IsSelected = true;

            var selectRect = new Rectangle(
                Points[0].X, Points[2].Y,
                Math.Abs(Points[1].X - Points[0].X),
                Math.Abs(Points[2].Y - Points[1].Y));

            g.DrawRectangle(new Pen(Color.Blue, 2), selectRect);

            // Draw shape center plus sign
            g.DrawLine(new Pen(Color.Red, 1), new Point(ShapeCenter.X, ShapeCenter.Y - 5), new Point(ShapeCenter.X, ShapeCenter.Y + 5));
            g.DrawLine(new Pen(Color.Red, 1), new Point(ShapeCenter.X - 5, ShapeCenter.Y), new Point(ShapeCenter.X + 5, ShapeCenter.Y));
        }

        public override bool IsInBounds(Point click)
        {
            // Create a GraphicsPath object
            using (GraphicsPath path = new GraphicsPath())
            {
                // Add the polygon points to the path
                path.AddPolygon(Points.Select(p => new PointF(p.X, p.Y)).ToArray());

                // Check if the point is inside the bounds of the polygon
                return path.IsVisible(click);
            }
        }
    }
}
