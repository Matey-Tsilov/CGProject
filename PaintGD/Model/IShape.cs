namespace PaintGD.Model
{
    public interface IShape
    {
        public bool IsSelected { get; set; }
        public IReadOnlyCollection<Point> Points { get; }
        public abstract void DrawShape(Graphics g, Pen p);
        public abstract void SelectShape(Graphics g);
        public Pen drawnPen { get; set; }
        public abstract bool IsInBounds(Point click);
        public Point ShapeCenter { get; set; }
    }
}
