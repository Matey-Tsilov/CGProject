using PaintGD.Model;

namespace PaintGD
{
    public partial class PaintGDI : Form
    {
        List<Shape> allShapes;
        Shape curShape;
        Graphics g;
        Pen p;

        Point startLocation;
        Point endLocation;

        int previousSizeScrollValue = 0;

        bool isMouseDown = false;
        bool isDragging = false;
        public PaintGDI()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            p = new Pen(colorDialog1.Color, trackBar1.Value);
            allShapes = new List<Shape>();
        }

        private void Mouse_Down(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            startLocation = e.Location;

            if (SelectShape.Checked)
            {
                // We need to check if the list is empty
                if (allShapes.Count > 0)
                {
                    // If not then we select the most recent record in whose Bounds we currently clicked
                    allShapes.Reverse();

                    // We get the current shape and select it
                    var selectedShape = allShapes.FirstOrDefault(s => s.IsInBounds(startLocation));

                    // This is the toggle select functionality
                    if (selectedShape != null)
                    {
                        curShape = selectedShape;

                        if (!selectedShape.IsSelected)
                        {
                            // If we have several shapes one onto another we will first select them all before unselecting only top layer!
                            selectedShape.SelectShape(g);
                            isMouseDown = false;
                        }
                        else
                        {
                            // Is it the area in the center
                            Rectangle centerPlus = new Rectangle(curShape.ShapeCenter.X - 15, curShape.ShapeCenter.Y - 15, 30, 30);

                            // If he clicked in the center we don't want to deselect the shape but to start dragging it
                            if (centerPlus.Contains(e.Location))
                            {
                                isDragging = true;
                            }
                            else
                            {
                                // We do it manually from here
                                selectedShape.IsSelected = false;
                            }
                        }
                    }
                    else
                    {
                        // If we just clicked outside of the bound of the element, we don't do or refresh anything
                        // We need to stop it before the Mouse_Move event since it will be triggered before the regular
                        // changing of the isMouseDown property in Mouse_Up event
                        isMouseDown = false;
                    }

                    // Fix the order of the elements
                    allShapes.Reverse();
                }
            }
        }

        private void Mouse_Move(object sender, MouseEventArgs e)
        {
            // The second part we have in cases where we drag or resize elements
            if (isMouseDown)
            {
                endLocation = e.Location;

                // We handle this specific case, to allow dragging
                if (SelectShape.Checked && isDragging)
                {
                    // We remove it, because we substitude it by the newly dragged one
                    allShapes.Remove(curShape);

                    string curShapeClass = curShape.GetType().Name;

                    // We create new shapes based on the center location
                    Point newCenter = new Point(e.X, e.Y);

                    // Those values are unchangable throughout the whole dragging, since curShape won't be changed until 
                    int shapeHalfWidth = Math.Abs(curShape.Points[0].X - curShape.Points[1].X) / 2;
                    int shapeHalfHeight = Math.Abs(curShape.Points[0].Y - curShape.Points[1].Y) / 2;

                    switch (curShapeClass)
                    {
                        case "SquareShape":
                            curShape = new SquareShape(newCenter, shapeHalfWidth, shapeHalfHeight);
                            break;
                        case "EllipseShape":
                            curShape = new EllipseShape(newCenter, shapeHalfWidth, shapeHalfHeight);
                            break;
                        case "LineShape":
                            curShape = new LineShape(newCenter, shapeHalfWidth, shapeHalfHeight);
                            break;
                        case "TriangleShape":
                            shapeHalfHeight = Math.Abs(curShape.Points[0].Y - curShape.Points[2].Y) / 2;
                            curShape = new TriangleShape(newCenter, shapeHalfWidth, shapeHalfHeight);
                            break;
                        case "TrapezoidShape":
                            shapeHalfHeight = Math.Abs(curShape.Points[0].Y - curShape.Points[2].Y) / 2;
                            int indent = curShape.Points[3].X - curShape.Points[0].X;
                            curShape = new TrapezoidShape(newCenter, shapeHalfWidth, shapeHalfHeight, indent);
                            break;
                    }
                }
                else
                {
                    // Based on selected option, we create a different shape
                    if (DrawCircle.Checked)
                    {
                        curShape = createEllipse();
                    }
                    else if (DrawLine.Checked)
                    {
                        curShape = createLine();
                    }
                    else if (DrawSquare.Checked)
                    {
                        curShape = createSquare();
                    }
                    else if (DrawTriangle.Checked)
                    {
                        curShape = createTriangle();
                    }
                    else if (DrawTrapezoid.Checked)
                    {
                        curShape = createTrapezoid();
                    }
                }

                // Apply changes to canva
                panel1.Refresh();
            }
        }

        private void Mouse_Up(object sender, MouseEventArgs e)
        {
            // The second part we have in cases where we drag or resize elements
            if (isMouseDown)
            {
                endLocation = e.Location;

                // Based on selected option, we create a different shape
                if (DrawCircle.Checked)
                {
                    curShape = createEllipse();
                }
                else if (DrawLine.Checked)
                {
                    curShape = createLine();
                }
                else if (DrawSquare.Checked)
                {
                    curShape = createSquare();
                }
                else if (DrawTriangle.Checked)
                {
                    curShape = createTriangle();
                }
                else if (DrawTrapezoid.Checked)
                {
                    curShape = createTrapezoid();
                }

                // That way we avoid adding null shapes or the selected ones to the collection while selecting or dragging, or adding the same one again
                if (curShape != null && !curShape.IsSelected && !allShapes.Contains(curShape))
                {
                    allShapes.Add(curShape);
                }

                panel1.Refresh();

                // We go back to default values for everything
                isMouseDown = false;
                isDragging = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            button1.BackColor = colorDialog1.Color;
            p.Color = colorDialog1.Color;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            g.Clear(Color.White);

            // Make all shapes disappear
            allShapes.Clear();
            curShape = null;

            panel1.Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // This lines are for the dynamic drawing, before adding to the collection,
            // not connected with persisting the drawings
            if (curShape != null)
            {
                curShape.DrawShape(e.Graphics, new Pen(colorDialog1.Color, trackBar1.Value));

                // We have a special treatment for the selected shapes between renders, for it to persist, for example during resizing
                if (curShape.IsSelected) curShape.SelectShape(g);
            }

            // We want to redraw all other drawings, the currently drawn one, won't be included as it is still not finished = added to the list with drawings
            if (allShapes.Count != 0)
            {
                allShapes.Where(s => s != curShape).ToList()
                    .ForEach(cur =>
                    {
                        // We can access the drawnPen property as this won't be the first time they are redrawn = they have it set!
                        cur.DrawShape(e.Graphics, cur.DrawnPen);

                        // We have a special treatment for the selected shapes between renders, for them to persist
                        if (cur.IsSelected) cur.SelectShape(g);
                    });
            }
        }

        // Thickness adjustments trackbar
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            p.Width = trackBar1.Value;
        }
        // Size adjustments trackbar
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            var curentValue = trackBar2.Value;
            var selectedShapes = allShapes.Where(s => s.IsSelected).ToList();

            // This will be the hardcoded value we use to increment the shapes with on each scroll
            int valueToIncrementSize = 5;

            foreach (Shape shape in selectedShapes)
            {
                string curShapeClass = shape.GetType().Name;

                // Since for drawing the Square and Ellipse we rely on the Shape property, which is not
                // available on the other shapes, we exclusively modify it on those shapes too!
                switch (curShapeClass)
                {
                    case "SquareShape":
                        SquareShape s = shape as SquareShape;

                        // We handle both directions, downsizing or upsizing the shapes
                        if (curentValue > previousSizeScrollValue)
                        {
                            s.Shape = new Rectangle(s.Shape.X - 5, s.Shape.Y - 5, s.Shape.Width + 10, s.Shape.Height + 10);
                        }
                        else if (curentValue < previousSizeScrollValue)
                        {
                            s.Shape = new Rectangle(s.Shape.X + 5, s.Shape.Y + 5, s.Shape.Width - 10, s.Shape.Height - 10);
                        }

                        // In case the value remains the same, we don't do anything - for example scrol with 
                        // the mouse to the maximum, and continue scrolling will trigger the scroll event handler with the maximum value over
                        break;
                    case "EllipseShape":
                        EllipseShape el = shape as EllipseShape;

                        // We handle both directions, downsizing or upsizing the shapes
                        if (curentValue > previousSizeScrollValue)
                        {
                            el.Shape = new Rectangle(el.Shape.X - 5, el.Shape.Y - 5, el.Shape.Width + 10, el.Shape.Height + 10);
                        }
                        else if (curentValue < previousSizeScrollValue)
                        {
                            el.Shape = new Rectangle(el.Shape.X + 5, el.Shape.Y + 5, el.Shape.Width - 10, el.Shape.Height - 10);
                        }

                        // In case the value remains the same, we don't do anything - for example scrol with 
                        // the mouse to the maximum, and continue scrolling will trigger the scroll event handler with the maximum value over and over again
                        break;
                }

                // Here we adjust the Points collection for all shapes based on their Points length:
                switch (curShapeClass)
                {
                    case "SquareShape":

                        // We handle both directions, downsizing or upsizing the shapes
                        if (curentValue > previousSizeScrollValue)
                        {
                            shape.Points = new List<Point>()
                            {
                                new Point(shape.Points[0].X - valueToIncrementSize, shape.Points[0].Y - valueToIncrementSize),
                                new Point(shape.Points[1].X + valueToIncrementSize, shape.Points[1].Y + valueToIncrementSize)
                            };
                        }
                        else if (curentValue < previousSizeScrollValue)
                        {
                            shape.Points = new List<Point>()
                            {
                                new Point(shape.Points[0].X + valueToIncrementSize, shape.Points[0].Y + valueToIncrementSize),
                                new Point(shape.Points[1].X - valueToIncrementSize, shape.Points[1].Y - valueToIncrementSize)
                            };
                        }

                        break;

                    case "EllipseShape":

                        // We handle both directions, downsizing or upsizing the shapes
                        if (curentValue > previousSizeScrollValue)
                        {
                            shape.Points = new List<Point>()
                            {
                                new Point(shape.Points[0].X + valueToIncrementSize, shape.Points[0].Y + valueToIncrementSize),
                                new Point(shape.Points[1].X - valueToIncrementSize, shape.Points[1].Y - valueToIncrementSize)
                            };
                        }
                        else if (curentValue < previousSizeScrollValue)
                        {
                            shape.Points = new List<Point>()
                            {
                                new Point(shape.Points[0].X + valueToIncrementSize, shape.Points[0].Y + valueToIncrementSize),
                                new Point(shape.Points[1].X - valueToIncrementSize, shape.Points[1].Y - valueToIncrementSize)
                            };
                        }
                        break;

                    case "LineShape":

                        // We need to decide here which is the start and endPoint or the Line, otherwise by increasing the size we might compress it instead
                        // * Because for drawing of the Line we cannot save as first Point the smalles and as second the biggest point, if we make it like that
                        //   this will crash line drawing. Since this is a unique case, we have the logic here
                        bool isBackwards = shape.Points[0].X > shape.Points[1].X;

                        // We need to know in which direction it is pointing also
                        bool isPoinitngDown = shape.Points[1].Y > shape.Points[0].Y;

                        // In LineShape case we have many more cases for directions resizings since Line start could be the bigger point than line end
                        if (curentValue > previousSizeScrollValue)
                        {
                            if (isPoinitngDown)
                            {
                                shape.Points = new List<Point>()
                                {
                                    new Point
                                    (
                                        isBackwards ? shape.Points[0].X + valueToIncrementSize : shape.Points[0].X - valueToIncrementSize,
                                        isBackwards ? shape.Points[0].Y - valueToIncrementSize : shape.Points[0].Y - valueToIncrementSize
                                    ),
                                    new Point
                                    (
                                        isBackwards ? shape.Points[1].X - valueToIncrementSize : shape.Points[1].X + valueToIncrementSize,
                                        isBackwards ? shape.Points[1].Y + valueToIncrementSize : shape.Points[1].Y + valueToIncrementSize
                                    )
                                };
                            }
                            else
                            {
                                shape.Points = new List<Point>()
                                {
                                    new Point
                                    (
                                        isBackwards ? shape.Points[0].X + valueToIncrementSize : shape.Points[0].X - valueToIncrementSize,
                                        isBackwards ? shape.Points[0].Y + valueToIncrementSize : shape.Points[0].Y + valueToIncrementSize
                                    ),
                                    new Point
                                    (
                                        isBackwards ? shape.Points[1].X - valueToIncrementSize : shape.Points[1].X + valueToIncrementSize,
                                        isBackwards ? shape.Points[1].Y - valueToIncrementSize : shape.Points[1].Y - valueToIncrementSize
                                    )
                                };
                            }
                        }
                        else if (curentValue < previousSizeScrollValue)
                        {
                            if (isPoinitngDown)
                            {
                                shape.Points = new List<Point>()
                                {
                                    new Point
                                    (
                                        isBackwards ? shape.Points[0].X - valueToIncrementSize : shape.Points[0].X + valueToIncrementSize,
                                        isBackwards ? shape.Points[0].Y + valueToIncrementSize : shape.Points[0].Y + valueToIncrementSize
                                    ),
                                    new Point
                                    (
                                        isBackwards ? shape.Points[1].X + valueToIncrementSize : shape.Points[1].X - valueToIncrementSize,
                                        isBackwards ? shape.Points[1].Y - valueToIncrementSize : shape.Points[1].Y - valueToIncrementSize
                                    )
                                };
                            }
                            else
                            {
                                shape.Points = new List<Point>()
                                {
                                    new Point
                                    (
                                        isBackwards ? shape.Points[0].X - valueToIncrementSize : shape.Points[0].X + valueToIncrementSize,
                                        isBackwards ? shape.Points[0].Y - valueToIncrementSize : shape.Points[0].Y - valueToIncrementSize
                                    ),
                                    new Point
                                    (
                                        isBackwards ? shape.Points[1].X + valueToIncrementSize : shape.Points[1].X - valueToIncrementSize,
                                        isBackwards ? shape.Points[1].Y + valueToIncrementSize : shape.Points[1].Y + valueToIncrementSize
                                    )
                                };
                            }
                        }
                        break;

                    case "TriangleShape":

                        // We handle both directions, downsizing or upsizing the shapes
                        if (curentValue > previousSizeScrollValue)
                        {
                            shape.Points = new List<Point>()
                            {
                                new Point(shape.Points[0].X - valueToIncrementSize, shape.Points[0].Y + valueToIncrementSize),
                                new Point(shape.Points[1].X + valueToIncrementSize, shape.Points[1].Y + valueToIncrementSize),
                                new Point(shape.Points[2].X, shape.Points[2].Y - valueToIncrementSize)
                            };
                        }
                        else if (curentValue < previousSizeScrollValue)
                        {
                            shape.Points = new List<Point>()
                            {
                                new Point(shape.Points[0].X + valueToIncrementSize, shape.Points[0].Y - valueToIncrementSize),
                                new Point(shape.Points[1].X - valueToIncrementSize, shape.Points[1].Y - valueToIncrementSize),
                                new Point(shape.Points[2].X, shape.Points[2].Y + valueToIncrementSize)
                            };
                        }
                        break;

                    case "TrapezoidShape":

                        // We handle both directions, downsizing or upsizing the shapes
                        if (curentValue > previousSizeScrollValue)
                        {
                            shape.Points = new List<Point>()
                            {
                                new Point(shape.Points[0].X - valueToIncrementSize, shape.Points[0].Y + valueToIncrementSize),
                                new Point(shape.Points[1].X + valueToIncrementSize, shape.Points[1].Y + valueToIncrementSize),
                                new Point(shape.Points[2].X + valueToIncrementSize, shape.Points[2].Y - valueToIncrementSize),
                                new Point(shape.Points[3].X - valueToIncrementSize, shape.Points[3].Y - valueToIncrementSize)
                            };
                        }
                        else if (curentValue < previousSizeScrollValue)
                        {
                            shape.Points = new List<Point>()
                            {
                                new Point(shape.Points[0].X + valueToIncrementSize, shape.Points[0].Y - valueToIncrementSize),
                                new Point(shape.Points[1].X - valueToIncrementSize, shape.Points[1].Y - valueToIncrementSize),
                                new Point(shape.Points[2].X - valueToIncrementSize, shape.Points[2].Y + valueToIncrementSize),
                                new Point(shape.Points[3].X + valueToIncrementSize, shape.Points[3].Y + valueToIncrementSize)
                            };
                        }
                        break;
                }
            }

            // Rerender all of the shapes
            panel1.Refresh();
            previousSizeScrollValue = curentValue;

        }



        // Since the logic is hard we make some factory functions
        private TriangleShape createTriangle()
        {
            var sideOfPerfectTriangle = endLocation.X - startLocation.X;

            // The height will always be upwards
            double height = Math.Abs(sideOfPerfectTriangle * Math.Sqrt(3) / 2);

            // If we start drawing the triangle backwards we need to handle the position of the topX
            int topX = startLocation.X + sideOfPerfectTriangle / 2;
            int topY = startLocation.Y - (int)height;

            // In order to be able to draw backwards and have the correct Points collection later for select rect
            int startX = Math.Min(startLocation.X, endLocation.X);
            int startY = Math.Min(startLocation.Y, endLocation.Y);

            int endX = Math.Max(startLocation.X, endLocation.X);

            return new TriangleShape(startX, startY, endX, startY, topX, topY);
        }
        private TrapezoidShape createTrapezoid()
        {
            // This is hard-coded, we need to adjust it based on the trapezoid
            int upperIndent = Math.Abs(endLocation.X - startLocation.X) / 4;

            // Because we want backwards compatibility we need to have both the 
            // Start and End locations and decide which should be the left and right one

            // Left Down Corner
            int ldcX = Math.Min(startLocation.X, endLocation.X);
            int ldcY = startLocation.Y;

            // Right Down Corner
            int rdcX = Math.Max(startLocation.X, endLocation.X);
            int rdcY = startLocation.Y;

            // Left Upper Corner    
            int lucX = rdcX - upperIndent;
            int lucY = startLocation.Y - upperIndent * 2;

            // Right Upper Corner
            int rucX = ldcX + upperIndent;
            int rucY = startLocation.Y - upperIndent * 2;

            return new TrapezoidShape(
                ldcX, ldcY,
                rdcX, rdcY,
                lucX, lucY,
                rucX, rucY
            );
        }
        private SquareShape createSquare()
        {

            // We need positive values for the width and height property throughout all code
            int width = Math.Abs(endLocation.X - startLocation.X);
            int height = Math.Abs(endLocation.Y - startLocation.Y);

            // Ensure that we get the left most Points for the start, as sometimes we may draw backwards
            int x = Math.Min(startLocation.X, endLocation.X);
            int y = Math.Min(startLocation.Y, endLocation.Y);

            return new SquareShape(x, y, width, height);
        }
        private EllipseShape createEllipse()
        {
            // We need positive values for the width and height property throughout all code
            int width = Math.Abs(endLocation.X - startLocation.X);
            int height = Math.Abs(endLocation.Y - startLocation.Y);

            // In order to be able to draw backwards we need to get the correct start for our drawing
            int x = Math.Min(startLocation.X, endLocation.X);
            int y = Math.Min(startLocation.Y, endLocation.Y);

            return new EllipseShape(x, y, width, height);
        }
        private LineShape createLine()
        {
            return new LineShape(startLocation.X, startLocation.Y, endLocation.X, endLocation.Y);
        }
        private Shape createCustomShape()
        {
            return null;
        }
    }
}
