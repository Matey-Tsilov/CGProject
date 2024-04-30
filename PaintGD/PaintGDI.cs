using PaintGD.Model;

namespace PaintGD
{
    public partial class PaintGDI : Form
    {
        List<IShape> allShapes;
        IShape curShape;
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
            allShapes = new List<IShape>();
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
                        }
                        else
                        {
                            // This is the area in the center
                            Rectangle centerPlus = new Rectangle(curShape.ShapeCenter.X, curShape.ShapeCenter.Y, 7, 7);

                            // If he clicked in the center we don't want to deselect the shape but to start dragging it
                            if (centerPlus.Contains(e.Location))
                            {
                                isDragging = true;
                            }
                            else
                            {
                                // We do it manually from here since we don't have a deselect method made for the public
                                selectedShape.IsSelected = false;

                                // Because we didn't add the highlight in the array it won't persist so a simple refresh fixes the highlight
                                panel1.Refresh();
                            }

                        }

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

                // We handle this specific case, to allow resizing and dragging
                if (SelectShape.Checked && isDragging)
                {
                    string curShapeClass = curShape.GetType().Name;
                    Point newCenter = new Point(e.X, e.Y);
                    int widthOfShape = (curShape.ShapeCenter.X - curShape.Points[0].X) * 2;
                    int heightOfShape = (curShape.ShapeCenter.Y - curShape.Points[0].Y) * 2;

                    // That way we get rid of the after images for dragging
                    allShapes.Remove(curShape);

                    switch (curShapeClass)
                    {
                        case "SquareShape": curShape = new SquareShape(newCenter, widthOfShape, heightOfShape); break;
                        case "LineShape": 
                            break;
                        //case "ElipseShape": curShape = new ElipseShape(newCenter, widthOfShape, heightOfShape);  break;
                        case "TrapezoidShape": 
                            break;
                        case "TriangleShape": 
                            break;
                    }

                    panel1.Refresh();
                }
                else
                {
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

                    panel1.Refresh();
                }
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

                // Tyka ima problem s refreshvaneto i zadawaneto na select shape!
                allShapes.Add(curShape);
                panel1.Refresh();

                // We go back to default values for everything
                isMouseDown = false;
                isDragging = false;
                curShape = null;

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
            // This block will be executed only when dragging the shape to make it,
            // after that the shape becomes again null and the letter if will take care of rendering
            if (curShape != null)
            {
                curShape.DrawShape(e.Graphics, new Pen(colorDialog1.Color, trackBar1.Value));
            }

            // We want to redraw the other drawings after adding the current one
            if (allShapes.Count != 0)
            {
                // We exclude the already redrawn shape, and redraw the others
                var shapesLeftToBeRedrawn = allShapes.Where(shape => shape != curShape).ToList();

                shapesLeftToBeRedrawn.ForEach(cur =>
                {
                    // We have a special treatment for the selected shapes between renders, for them to persist
                    if (cur.IsSelected)
                    {
                        cur.DrawShape(e.Graphics, cur.drawnPen);
                        cur.SelectShape(g);
                        cur.IsSelected = true; // Neutralize the in-method IsSelected assignment
                    }
                    else
                    {
                        // We can access the drawnPen property as this won't be the first time they are redrawn = they have it set!
                        cur.DrawShape(e.Graphics, cur.drawnPen);
                    }
                });
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            p.Width = trackBar1.Value;
        }
    }
}
