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
        public PaintGDI()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            p = new Pen(Color.Black, 3);
            allShapes = new List<IShape>();
        }

        private void Mouse_Down(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            startLocation = e.Location;
        }

        private void Mouse_Move(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                endLocation = e.Location;

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

                    curShape = new TriangleShape(startLocation.X, startLocation.Y, endLocation.X, startLocation.Y, topX, topY);
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

                // Redraw the panel to display the updated preview
                panel1.Invalidate();
            }
        }

        private void Mouse_Up(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                endLocation = e.Location;

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

                    curShape = new TriangleShape(startLocation.X, startLocation.Y, endLocation.X, startLocation.Y, topX, topY);
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

                // Redraw the panel to display the final curShape
                panel1.Invalidate();

                isMouseDown = false;
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
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (curShape != null)
            {
                curShape.DrawShape(e.Graphics, p);
            }
        }
    }
}
