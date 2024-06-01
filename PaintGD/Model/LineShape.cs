using Newtonsoft.Json;

namespace PaintGD.Model
{
    public class LineShape : Shape
    {
        public LineShape(int x, int y, int x1, int y1)
        {
            Points = new List<Point>() { new Point(x, y), new Point(x1, y1) };
            ShapeCenter = new Point((int)Points.Average(propa => propa.X), (int)Points.Average(propa => propa.Y));
            Type = "LineShape";
        }
        public LineShape(Point center, int halfWidth, int halfHeight, bool isUp)
        {
            // We have conditional statements to not rotate line on dragging
            if (isUp)
            {
                Points = new List<Point>() {
                new Point(center.X - halfWidth, center.Y + halfHeight),
                new Point(center.X + halfWidth, center.Y - halfHeight)
                };
            }
            // We need those Points in all shapes to calculate the width, while dragging
            else
            {
                Points = new List<Point>() {
                new Point(center.X - halfWidth, center.Y - halfHeight),
                new Point(center.X + halfWidth, center.Y + halfHeight)
                };
            }

            ShapeCenter = center;
            Type = "LineShape";
        }

        // This will be our JSONconstructor for the custom deserialization
        [JsonConstructor]
        public LineShape(string[] Points, string ShapeCenter, string DrawnPenColor, bool IsSelected)
        {

            int[][] PointsNums = Points.Select(arr => arr.Split(", ").Select(int.Parse).ToArray()).ToArray();
            this.Points = PointsNums.Select(arr => new Point(arr[0], arr[1])).ToList();

            int[] ShapeCenterNumbers = ShapeCenter.Split(", ").Select(int.Parse).ToArray();
            this.ShapeCenter = new Point(ShapeCenterNumbers[0], ShapeCenterNumbers[1]);

            this.DrawnPenColor = ColorTranslator.FromHtml(DrawnPenColor);

            this.IsSelected = IsSelected;

            this.Type = "LineShape";
        }

        public override void DrawShape(Graphics g, Pen p)
        {
            g.DrawLine(p, Points[0], Points[1]);

            // We memorize the color and width of the drawn shape
            DrawnPenColor = p.Color;
            DrawnPenSize = p.Width;
        }
        public override void SelectShape(Graphics g)
        {
            this.IsSelected = true;

            // We need to put the upper most Point as the begin for the select rectangle
            // * We have this logic here since the only problem with the LineShape will be during the select
            //   rectangle drawing, since we don't know if it was drawn backwards or not
            int upperY = Math.Min(Points[0].Y, Points[1].Y);
            int upperX = Math.Min(Points[0].X, Points[1].X);

            // We put that here for it to be calculated on each resize, which triggers a redraw and modification in the Points collection values
            var lineSelectRect = new Rectangle(upperX, upperY, Math.Abs(Points[1].X - Points[0].X), Math.Abs(Points[0].Y - Points[1].Y));

            // Convert the hexadecimal color string to a Color object
            string hexColor = "#3399FF";
            Color color = ColorTranslator.FromHtml(hexColor);

            g.DrawRectangle(new Pen(color, 2), lineSelectRect);

            // Draw shape center plus sign
            g.DrawLine(new Pen(Color.Red, 1), new Point(ShapeCenter.X, ShapeCenter.Y - 5), new Point(ShapeCenter.X, ShapeCenter.Y + 5));
            g.DrawLine(new Pen(Color.Red, 1), new Point(ShapeCenter.X - 5, ShapeCenter.Y), new Point(ShapeCenter.X + 5, ShapeCenter.Y));
        }
        public override bool IsInBounds(Point click)
        {
            // We need to put the upper most Point as the begin for the select rectangle
            // * We have this logic here since the only problem with the LineShape will be during the select
            //   rectangle drawing, since we don't know if it was drawn backwards or not
            int upperY = Math.Min(Points[0].Y, Points[1].Y);
            int upperX = Math.Min(Points[0].X, Points[1].X);

            // Here we dont only select when we click directly on the line but on each place in the select rectangle of it
            var LineSelectZone = new Rectangle(upperX, upperY, Math.Abs(Points[1].X - Points[0].X), Math.Abs(Points[0].Y - Points[1].Y));
            return LineSelectZone.Contains(click);
        }
    }
}
