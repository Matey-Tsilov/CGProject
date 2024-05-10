namespace PaintGD.Model
{
    public class LineShape : Shape
    {
        public LineShape(int x, int y, int x1, int y1)
        {
            Points = new List<Point>() { new Point(x, y), new Point(x1, y1) };
            ShapeCenter = new Point((int)Points.Average(propa => propa.X), (int)Points.Average(propa => propa.Y));
        }
        public LineShape(Point center, int halfWidth, int halfHeight)
        {
            // We need those Points in all shapes to calculate the width, while dragging
            Points = new List<Point>() {
                new Point(center.X - halfWidth, center.Y - halfHeight),
                new Point(center.X + halfWidth, center.Y + halfHeight)
            };
            ShapeCenter = center;
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

            // We need to put the upper most Point as the begin for the select rectangle
            // * We have this logic here since the only problem with the LineShape will be during the select
            //   rectangle drawing, since we don't know if it was drawn backwards or not
            int upperY = Math.Min(Points[0].Y, Points[1].Y);
            int upperX = Math.Min(Points[0].X, Points[1].X);

            // We put that here for it to be calculated on each resize, which triggers a redraw and modification in the Points collection values
            var lineSelectRect = new Rectangle(upperX, upperY, Math.Abs(Points[1].X - Points[0].X), Math.Abs(Points[0].Y - Points[1].Y));

            g.DrawRectangle(new Pen(Color.Blue, 2), lineSelectRect);

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
