namespace PaintGD
{
    public partial class PaintGDI : Form
    {
        List<Shape> allShapes;
        Rectangle curShape;
        Graphics g;
        Pen p;

        Point StartLocation;
        Point EndLocation;

        bool isMouseDown = false;
        public PaintGDI()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            p = new Pen(Color.Black, 3);
            allShapes = new List<Shape>();
        }

        private void Mouse_Down(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            StartLocation = e.Location;
        }

        private void Mouse_Move(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                EndLocation = e.Location;

                // Calculate the dimensions of the square
                int width = EndLocation.X - StartLocation.X;
                int height = EndLocation.Y - StartLocation.Y;

                // Decide which is the curShape of the updating form
                if (DrawCircle.Checked)
                {
                }
                else if (DrawLine.Checked)
                {
                }
                else if (DrawSquare.Checked)
                {
                    // Update the curShape to show the preview
                    curShape = new Rectangle(StartLocation.X, StartLocation.Y, width, height);
                }

                // Redraw the panel to display the updated preview
                panel1.Invalidate();
            }
        }

        private void Mouse_Up(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                EndLocation = e.Location;

                // Update the curShape with the final dimensions
                int width = EndLocation.X - StartLocation.X;
                int height = EndLocation.Y - StartLocation.Y;

                // Decide which is the curShape of the final form
                if (DrawCircle.Checked)
                {
                }
                else if (DrawLine.Checked)
                {
                }
                else if (DrawSquare.Checked)
                {
                    // Update the curShape to show the preview
                    curShape = new Rectangle(StartLocation.X, StartLocation.Y, width, height);
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
                // Draw the curShape with the specified pen
                if (DrawCircle.Checked)
                {
                }
                else if (DrawLine.Checked)
                {
                }
                else if (DrawSquare.Checked)
                {
                    e.Graphics.DrawRectangle(p, curShape);
                }
            }
        }
    }
}
