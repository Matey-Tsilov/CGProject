using Newtonsoft.Json;

namespace PaintGD.Model
{
    public class CustomShape : Shape
    {
        // The complex Shape model:
        public Rectangle BaseShape { get; set; }
        public List<LineShape> lineShapes { get; set; }
        public CustomShape(int x, int y, int width, int height, List<LineShape> lines)
        {
            Points = new List<Point>() { new Point(x, y), new Point(x + width, y + height) };
            ShapeCenter = new Point(x + width / 2, y + height / 2);
            BaseShape = new Rectangle(x, y, width, height);
            lineShapes = lines;
            Type = "CustomShape";
        }

        public CustomShape(Point center, int halfWidth, int halfHeight, List<LineShape> lines)
        {
            Points = new List<Point>() {
                new Point(center.X - halfWidth, center.Y - halfHeight),
                new Point(center.X + halfWidth, center.Y + halfHeight)
            };
            ShapeCenter = center;
            lineShapes = lines;
            BaseShape = new Rectangle(center.X - halfWidth, center.Y - halfHeight, halfWidth * 2, halfHeight * 2);
            Type = "CustomShape";
        }

        // This will be our JSONconstructor for the custom deserialization
        [JsonConstructor]
        public CustomShape(string ShapeCenter, string DrawnPenColor, string DrawnPenSize, bool IsSelected)
        {
            int[] ShapeCenterNumbers = ShapeCenter.Split(", ").Select(int.Parse).ToArray();
            this.ShapeCenter = new Point(ShapeCenterNumbers[0], ShapeCenterNumbers[1]);

            this.DrawnPenColor = ColorTranslator.FromHtml(DrawnPenColor);
            this.DrawnPenSize = float.Parse(DrawnPenSize);

            this.IsSelected = IsSelected;

            this.Type = "CustomShape";
        }

        public override void DrawShape(Graphics g, Pen p)
        {
            g.DrawEllipse(p, BaseShape);
            lineShapes.ForEach(line => g.DrawLine(p, line.Points[0], line.Points[1]));

            // We memorize the color and width of the drawn shape
            DrawnPenColor = p.Color;
            DrawnPenSize = p.Width;
        }

        public override bool IsInBounds(Point click)
        {
            // return if the click is inside the rectangle or not!
            return BaseShape.Contains(click);
        }

        public override void SelectShape(Graphics g)
        {
            this.IsSelected = true;

            // Convert the hexadecimal color string to a Color object
            string hexColor = "#3399FF";
            Color color = ColorTranslator.FromHtml(hexColor);

            g.DrawRectangle(new Pen(color, 2), BaseShape);

            // Draw shape center plus sign
            g.DrawLine(new Pen(Color.Red, 1), new Point(ShapeCenter.X, ShapeCenter.Y - 5), new Point(ShapeCenter.X, ShapeCenter.Y + 5));
            g.DrawLine(new Pen(Color.Red, 1), new Point(ShapeCenter.X - 5, ShapeCenter.Y), new Point(ShapeCenter.X + 5, ShapeCenter.Y));
        }
    }
}
