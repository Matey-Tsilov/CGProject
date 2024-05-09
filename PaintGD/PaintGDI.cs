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
                        case "LineShape": break;
                        case "TrapezoidShape": break;
                        case "TriangleShape": break;
                    }
                }
                else
                {
                    // Based on selected option, we create a different shape
                    if (DrawCircle.Checked)
                    {
                        int width = endLocation.X - startLocation.X;
                        int height = endLocation.Y - startLocation.Y;

                        curShape = new EllipseShape(startLocation.X, startLocation.Y, width, height);
                    }
                    else if (DrawLine.Checked)
                    {
                        curShape = new LineShape(startLocation.X, startLocation.Y, endLocation.X, endLocation.Y);
                    }
                    else if (DrawSquare.Checked)
                    {
                        int width = Math.Abs(endLocation.X - startLocation.X);
                        int height = Math.Abs(endLocation.Y - startLocation.Y);

                        // Ensure that the width and height are positive
                        int x = Math.Min(startLocation.X, endLocation.X);
                        int y = Math.Min(startLocation.Y, endLocation.Y);

                        curShape = new SquareShape(x, y, width, height);
                    }
                    else if (DrawTriangle.Checked)
                    {
                        var sideOfPerfectTriangle = endLocation.X - startLocation.X;

                        // The height will always be upwards
                        double height = Math.Abs(sideOfPerfectTriangle * Math.Sqrt(3) / 2);

                        // If we start drawing the triangle backwards we need to handle the position of the topX
                        int topX = startLocation.X + sideOfPerfectTriangle / 2;
                        var topY = startLocation.Y - (int)height;

                        curShape = new TriangleShape(startLocation.X, startLocation.Y, endLocation.X, startLocation.Y, topX, topY, (int)height);
                    }
                    else if (DrawTrapezoid.Checked)
                    {
                        // This is hard-coded, we need to adjust it based on the trapezoid
                        int upperIndednt = Math.Abs(endLocation.X - startLocation.X) / 4;

                        // Because we want backwards compatibility we need to have both the 
                        // Start and End locations and decide which should be the left and right one

                        // Left Down Corner
                        int ldcX = Math.Min(startLocation.X, endLocation.X);
                        int ldcY = startLocation.Y;

                        // Right Down Corner
                        int rdcX = Math.Max(startLocation.X, endLocation.X);
                        int rdcY = startLocation.Y;

                        // Left Upper Corner    
                        int lucX = rdcX - upperIndednt;
                        int lucY = startLocation.Y - upperIndednt * 2;

                        // Right Upper Corner
                        int rucX = ldcX + upperIndednt;
                        int rucY = startLocation.Y - upperIndednt * 2;

                        curShape = new TrapezoidShape(
                            ldcX, ldcY,
                            rdcX, rdcY,
                            lucX, lucY,
                            rucX, rucY
                        );
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

                // We handle this specific case, to allow resizing and dragging
                if (SelectShape.Checked && isDragging)
                {


                }
                else
                {
                    // Based on selected option, we create a different shape
                    if (DrawCircle.Checked)
                    {
                        int width = endLocation.X - startLocation.X;
                        int height = endLocation.Y - startLocation.Y;

                        curShape = new EllipseShape(startLocation.X, startLocation.Y, width, height);
                    }
                    else if (DrawLine.Checked)
                    {
                        curShape = new LineShape(startLocation.X, startLocation.Y, endLocation.X, endLocation.Y);
                    }
                    else if (DrawSquare.Checked)
                    {
                        int width = Math.Abs(endLocation.X - startLocation.X);
                        int height = Math.Abs(endLocation.Y - startLocation.Y);

                        // Ensure that the width and height are positive
                        int x = Math.Min(startLocation.X, endLocation.X);
                        int y = Math.Min(startLocation.Y, endLocation.Y);

                        curShape = new SquareShape(x, y, width, height);
                    }
                    else if (DrawTriangle.Checked)
                    {
                        var sideOfPerfectTriangle = endLocation.X - startLocation.X;

                        // The height will always be upwards
                        double height = Math.Abs(sideOfPerfectTriangle * Math.Sqrt(3) / 2);

                        // If we start drawing the triangle backwards we need to handle the position of the topX
                        int topX = startLocation.X + sideOfPerfectTriangle / 2;
                        var topY = startLocation.Y - (int)height;

                        curShape = new TriangleShape(startLocation.X, startLocation.Y, endLocation.X, startLocation.Y, topX, topY, (int)height);
                    }
                    else if (DrawTrapezoid.Checked)
                    {
                        // This is hard-coded, we need to adjust it based on the trapezoid
                        int upperIndednt = Math.Abs(endLocation.X - startLocation.X) / 4;

                        // Because we want backwards compatibility we need to have both the 
                        // Start and End locations and decide which should be the left and right one

                        // Left Down Corner
                        int ldcX = Math.Min(startLocation.X, endLocation.X);
                        int ldcY = startLocation.Y;

                        // Right Down Corner
                        int rdcX = Math.Max(startLocation.X, endLocation.X);
                        int rdcY = startLocation.Y;

                        // Left Upper Corner    
                        int lucX = rdcX - upperIndednt;
                        int lucY = startLocation.Y - upperIndednt * 2;

                        // Right Upper Corner
                        int rucX = ldcX + upperIndednt;
                        int rucY = startLocation.Y - upperIndednt * 2;

                        curShape = new TrapezoidShape(
                            ldcX, ldcY,
                            rdcX, rdcY,
                            lucX, lucY,
                            rucX, rucY
                        );
                    }
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

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            p.Width = trackBar1.Value;
        }
    }
}
