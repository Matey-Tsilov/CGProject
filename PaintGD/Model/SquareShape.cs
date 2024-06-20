using Newtonsoft.Json;

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
            Type = "SquareShape";
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
            Type = "SquareShape";
        }

        // This will be our JSONconstructor for the custom deserialization
        [JsonConstructor]
        public SquareShape(string Shape, string[] Points, string ShapeCenter, string DrawnPenColor, string DrawnPenSize, bool IsSelected)
        {
            int[] ShapeNumbers = Shape.Split(", ").Select(int.Parse).ToArray();
            this.Shape = new Rectangle(ShapeNumbers[0], ShapeNumbers[1], ShapeNumbers[2], ShapeNumbers[3]);

            int[][] PointsNums = Points.Select(arr => arr.Split(", ").Select(int.Parse).ToArray()).ToArray();
            this.Points = PointsNums.Select(arr => new Point(arr[0], arr[1])).ToList();

            int[] ShapeCenterNumbers = ShapeCenter.Split(", ").Select(int.Parse).ToArray();
            this.ShapeCenter = new Point(ShapeCenterNumbers[0], ShapeCenterNumbers[1]);

            this.DrawnPenColor = ColorTranslator.FromHtml(DrawnPenColor);
            this.DrawnPenSize = float.Parse(DrawnPenSize);

            this.IsSelected = IsSelected;

            this.Type = "SquareShape";
        }

        public override void DrawShape(Graphics g, Pen p)
        {
            g.DrawRectangle(p, Shape);

            // We memorize the color and width of the drawn shape
            DrawnPenColor = p.Color;
            DrawnPenSize = p.Width;
        }

        public override void SelectShape(Graphics g)
        {
            IsSelected = true;

            // Convert the hexadecimal color string to a Color object
            string hexColor = "#3399FF";
            Color color = ColorTranslator.FromHtml(hexColor);

            g.DrawRectangle(new Pen(color, 2), Shape);

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
