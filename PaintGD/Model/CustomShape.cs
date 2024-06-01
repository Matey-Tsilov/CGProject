using Newtonsoft.Json;

namespace PaintGD.Model
{
    public class CustomShape : Shape
    {
        //public Rectangle Shape { get; set; }
        public CustomShape(int x, int y, int x1, int y1, int x2, int y2)
        {
            //Points = new List<Point>() { new Point(x, y), new Point(x1, y1), new Point(x2, y2) };
            //ShapeCenter = new Point((x + x1) / 2, (y + y2) / 2);
            Type = "CustomShape";
        }

        public CustomShape(Point center, int halfWidth, int halfHeight)
        {
            // We need those Points in all shapes to calculate the width, while dragging
            //Points = new List<Point>() {
            //    new Point(center.X - halfWidth, center.Y + halfHeight),
            //    new Point(center.X + halfWidth, center.Y + halfHeight),
            //    new Point(center.X, center.Y - halfHeight),
            //};
            //ShapeCenter = center;
            Type = "CustomShape";
        }

        // This will be our JSONconstructor for the custom deserialization
        [JsonConstructor]
        public CustomShape(string[] Points, string ShapeCenter, string DrawnPenColor, bool IsSelected)
        {
            // TODO: If shape is from polygon thne we leave this, if from ellipse or sqare we need to add the Shape property!
            int[][] PointsNums = Points.Select(arr => arr.Split(", ").Select(int.Parse).ToArray()).ToArray();
            this.Points = PointsNums.Select(arr => new Point(arr[0], arr[1])).ToList();

            int[] ShapeCenterNumbers = ShapeCenter.Split(", ").Select(int.Parse).ToArray();
            this.ShapeCenter = new Point(ShapeCenterNumbers[0], ShapeCenterNumbers[1]);

            this.DrawnPenColor = ColorTranslator.FromHtml(DrawnPenColor);

            this.IsSelected = IsSelected;

            this.Type = "CustomShape";
        }

        public override void DrawShape(Graphics g, Pen p)
        {
            throw new NotImplementedException();
        }

        public override bool IsInBounds(Point click)
        {
            throw new NotImplementedException();
        }

        public override void SelectShape(Graphics g)
        {
            throw new NotImplementedException();
        }
    }
}
