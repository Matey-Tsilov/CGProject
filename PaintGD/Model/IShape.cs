namespace PaintGD.Model
{
    public interface IShape
    {
        public bool IsSelected { get; set; }
        public IReadOnlyCollection<Point> Points { get; }
        public abstract void DrawShape(Graphics g, Pen p);
        public abstract void SelectShape(Graphics g);

        public abstract bool IsInBounds(Point click);
    }
}
