using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintGD.Model
{
    public interface IShape
    {
        public abstract IReadOnlyCollection<Point> Points { get; }
        public abstract void DrawShape(Graphics g, Pen p);
    }
}
