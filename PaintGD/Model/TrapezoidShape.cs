using System.Drawing.Drawing2D;

namespace PaintGD.Model
{
    public class TrapezoidShape : IShape
    {
        public List<Point> Points { get; set; }
        public bool IsSelected { get; set; } = false;
        public Pen drawnPen { get; set; }
        public Point ShapeCenter { get; set; }

        public TrapezoidShape(int x, int y, int x1, int y1,int x2, int y2, int x3, int y3)
        {
            Points = new List<Point>() { new Point(x, y), new Point(x1, y1), new Point(x2, y2), new Point(x3, y3)};
            ShapeCenter = new Point((x + x1) / 2, (y - y1) / 2);
        }

        public void DrawShape(Graphics g, Pen p)
        {
            PointF[] pointsF = Points.Select(p => new PointF(p.X, p.Y)).ToArray();
            g.DrawPolygon(p, pointsF);

            // We memorize the color and width of the drawn shape
            drawnPen = p;
        }

        public void SelectShape(Graphics g)
        {
            // Toggle button for selecting an element
            this.IsSelected = !this.IsSelected;

            var selectRect = new Rectangle(
                Points[0].X, Points[3].Y, 
                Math.Abs(Points[1].X - Points[0].X), 
                Math.Abs(Points[1].Y - Points[2].Y));

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
                path.AddPolygon(Points.Select(p => new PointF(p.X, p.Y)).ToArray());

                // Check if the point is inside the bounds of the polygon
                return path.IsVisible(click);
            }
        }
    }
}
