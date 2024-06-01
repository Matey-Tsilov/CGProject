namespace PaintGD.Model
{
    public abstract class Shape
    {
        public string Type { get; set; }
        public bool IsSelected { get; set; } = false;
        public List<Point>? Points { get; set; }
        public Color DrawnPenColor { get; set; }
        public float DrawnPenSize { get; set; }
        public Point ShapeCenter { get; set; }
        public abstract bool IsInBounds(Point click);
        public abstract void DrawShape(Graphics g, Pen p);
        public abstract void SelectShape(Graphics g);
        public void DeselectShape(Panel panel)
        {
            this.IsSelected = false;
            panel.Refresh();
        }
    }
}
