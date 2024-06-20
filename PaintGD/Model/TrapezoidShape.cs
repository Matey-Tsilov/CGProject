using Newtonsoft.Json;
using System.Drawing.Drawing2D;

namespace PaintGD.Model
{
    public class TrapezoidShape : Shape
    {
        public TrapezoidShape(int x, int y, int x1, int y1, int x2, int y2, int x3, int y3)
        {
            Points = new List<Point>() { new Point(x, y), new Point(x1, y1), new Point(x2, y2), new Point(x3, y3) };
            ShapeCenter = new Point((x + x1) / 2, (y + y2) / 2);
            Type = "TrapezoidShape";
        }
        public TrapezoidShape(Point center, int halfWidth, int halfHeight, int indent)
        {
            // We need those Points in all shapes to calculate the width, while dragging
            Points = new List<Point>() {
                new Point(center.X - halfWidth, center.Y + halfHeight),
                new Point(center.X + halfWidth, center.Y + halfHeight),
                new Point(center.X + halfWidth - indent, center.Y - halfHeight),
                new Point(center.X - halfWidth + indent, center.Y - halfHeight)
            };
            ShapeCenter = center;
            Type = "TrapezoidShape";
        }

        // This will be our JSONconstructor for the custom deserialization
        [JsonConstructor]
        public TrapezoidShape(string[] Points, string ShapeCenter, string DrawnPenColor, string DrawnPenSize, bool IsSelected)
        {

            int[][] PointsNums = Points.Select(arr => arr.Split(", ").Select(int.Parse).ToArray()).ToArray();
            this.Points = PointsNums.Select(arr => new Point(arr[0], arr[1])).ToList();

            int[] ShapeCenterNumbers = ShapeCenter.Split(", ").Select(int.Parse).ToArray();
            this.ShapeCenter = new Point(ShapeCenterNumbers[0], ShapeCenterNumbers[1]);

            this.DrawnPenColor = ColorTranslator.FromHtml(DrawnPenColor);
            this.DrawnPenSize = float.Parse(DrawnPenSize);

            this.IsSelected = IsSelected;

            this.Type = "TrapezoidShape";
        }
        public override void DrawShape(Graphics g, Pen p)
        {
            PointF[] pointsF = Points.Select(p => new PointF(p.X, p.Y)).ToArray();
            g.DrawPolygon(p, pointsF);

            // We memorize the color and width of the drawn shape
            DrawnPenColor = p.Color;
            DrawnPenSize = p.Width;
        }

        public override void SelectShape(Graphics g)
        {
            this.IsSelected = true;

            var selectRect = new Rectangle(
                Points[0].X, Points[3].Y,
                Math.Abs(Points[1].X - Points[0].X),
                Math.Abs(Points[1].Y - Points[2].Y));

            // Convert the hexadecimal color string to a Color object
            string hexColor = "#3399FF";
            Color color = ColorTranslator.FromHtml(hexColor);

            g.DrawRectangle(new Pen(color, 2), selectRect);

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
