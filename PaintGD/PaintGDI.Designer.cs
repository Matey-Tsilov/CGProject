namespace PaintGD
{
    partial class PaintGDI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DrawSquare = new RadioButton();
            DrawCircle = new RadioButton();
            DrawTriangle = new RadioButton();
            DrawLine = new RadioButton();
            label1 = new Label();
            colorDialog1 = new ColorDialog();
            button1 = new Button();
            button2 = new Button();
            panel1 = new Panel();
            label2 = new Label();
            SelectShape = new RadioButton();
            label3 = new Label();
            DrawTrapezoid = new RadioButton();
            trackBar1 = new TrackBar();
            ThicknessLevel = new Label();
            bindingSource1 = new BindingSource(components);
            trackBar2 = new TrackBar();
            trackBar3 = new TrackBar();
            label4 = new Label();
            Rotate = new Label();
            textBox1 = new TextBox();
            ExportBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar3).BeginInit();
            SuspendLayout();
            // 
            // DrawSquare
            // 
            DrawSquare.AutoSize = true;
            DrawSquare.Location = new Point(62, 88);
            DrawSquare.Name = "DrawSquare";
            DrawSquare.Size = new Size(76, 24);
            DrawSquare.TabIndex = 0;
            DrawSquare.TabStop = true;
            DrawSquare.Text = "Square";
            DrawSquare.UseVisualStyleBackColor = true;
            // 
            // DrawCircle
            // 
            DrawCircle.AutoSize = true;
            DrawCircle.Location = new Point(62, 118);
            DrawCircle.Name = "DrawCircle";
            DrawCircle.Size = new Size(67, 24);
            DrawCircle.TabIndex = 1;
            DrawCircle.TabStop = true;
            DrawCircle.Text = "Circle";
            DrawCircle.UseVisualStyleBackColor = true;
            // 
            // DrawTriangle
            // 
            DrawTriangle.AutoSize = true;
            DrawTriangle.Location = new Point(62, 148);
            DrawTriangle.Name = "DrawTriangle";
            DrawTriangle.Size = new Size(83, 24);
            DrawTriangle.TabIndex = 2;
            DrawTriangle.TabStop = true;
            DrawTriangle.Text = "Triangle";
            DrawTriangle.UseVisualStyleBackColor = true;
            // 
            // DrawLine
            // 
            DrawLine.AutoSize = true;
            DrawLine.Location = new Point(62, 58);
            DrawLine.Name = "DrawLine";
            DrawLine.Size = new Size(57, 24);
            DrawLine.TabIndex = 3;
            DrawLine.TabStop = true;
            DrawLine.Text = "Line";
            DrawLine.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(46, 35);
            label1.Name = "label1";
            label1.Size = new Size(73, 20);
            label1.TabIndex = 5;
            label1.Text = "Primitives";
            // 
            // button1
            // 
            button1.BackColor = SystemColors.Window;
            button1.Location = new Point(62, 344);
            button1.Name = "button1";
            button1.Size = new Size(92, 29);
            button1.TabIndex = 6;
            button1.Text = "Color";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(62, 309);
            button2.Name = "button2";
            button2.Size = new Size(92, 29);
            button2.TabIndex = 7;
            button2.Text = "Clear";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ButtonHighlight;
            panel1.Location = new Point(220, 36);
            panel1.Name = "panel1";
            panel1.Size = new Size(904, 698);
            panel1.TabIndex = 8;
            panel1.Paint += panel1_Paint;
            panel1.MouseDown += Mouse_Down;
            panel1.MouseMove += Mouse_Move;
            panel1.MouseUp += Mouse_Up;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(46, 286);
            label2.Name = "label2";
            label2.Size = new Size(58, 20);
            label2.TabIndex = 9;
            label2.Text = "Actions";
            // 
            // SelectShape
            // 
            SelectShape.AutoSize = true;
            SelectShape.Location = new Point(62, 248);
            SelectShape.Name = "SelectShape";
            SelectShape.Size = new Size(115, 24);
            SelectShape.TabIndex = 10;
            SelectShape.TabStop = true;
            SelectShape.Text = "Select Shape";
            SelectShape.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(46, 216);
            label3.Name = "label3";
            label3.Size = new Size(51, 20);
            label3.TabIndex = 11;
            label3.Text = "Cursor";
            // 
            // DrawTrapezoid
            // 
            DrawTrapezoid.AutoSize = true;
            DrawTrapezoid.Location = new Point(62, 178);
            DrawTrapezoid.Name = "DrawTrapezoid";
            DrawTrapezoid.Size = new Size(96, 24);
            DrawTrapezoid.TabIndex = 13;
            DrawTrapezoid.TabStop = true;
            DrawTrapezoid.Text = "Trapezoid";
            DrawTrapezoid.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(12, 458);
            trackBar1.Minimum = 1;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(185, 56);
            trackBar1.TabIndex = 14;
            trackBar1.Value = 3;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // ThicknessLevel
            // 
            ThicknessLevel.AutoSize = true;
            ThicknessLevel.Location = new Point(46, 423);
            ThicknessLevel.Name = "ThicknessLevel";
            ThicknessLevel.Size = new Size(71, 20);
            ThicknessLevel.TabIndex = 15;
            ThicknessLevel.Text = "Thickness";
            // 
            // trackBar2
            // 
            trackBar2.Cursor = Cursors.VSplit;
            trackBar2.LargeChange = 1;
            trackBar2.Location = new Point(12, 582);
            trackBar2.Maximum = 25;
            trackBar2.Name = "trackBar2";
            trackBar2.Size = new Size(185, 56);
            trackBar2.TabIndex = 16;
            trackBar2.Scroll += trackBar2_Scroll;
            // 
            // trackBar3
            // 
            trackBar3.Location = new Point(12, 654);
            trackBar3.Maximum = 360;
            trackBar3.Name = "trackBar3";
            trackBar3.Size = new Size(185, 56);
            trackBar3.TabIndex = 17;
            trackBar3.TickFrequency = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(46, 559);
            label4.Name = "label4";
            label4.Size = new Size(36, 20);
            label4.TabIndex = 18;
            label4.Text = "Size";
            // 
            // Rotate
            // 
            Rotate.AutoSize = true;
            Rotate.Location = new Point(46, 631);
            Rotate.Name = "Rotate";
            Rotate.Size = new Size(53, 20);
            Rotate.TabIndex = 19;
            Rotate.Text = "Rotate";
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.ControlLight;
            textBox1.Location = new Point(12, 511);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(185, 27);
            textBox1.TabIndex = 20;
            textBox1.Text = "Selected Shapes";
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // ExportBtn
            // 
            ExportBtn.BackColor = SystemColors.Window;
            ExportBtn.Location = new Point(62, 379);
            ExportBtn.Name = "ExportBtn";
            ExportBtn.Size = new Size(92, 29);
            ExportBtn.TabIndex = 21;
            ExportBtn.Text = "Export";
            ExportBtn.UseVisualStyleBackColor = false;
            // 
            // PaintGDI
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1151, 760);
            Controls.Add(ExportBtn);
            Controls.Add(textBox1);
            Controls.Add(Rotate);
            Controls.Add(label4);
            Controls.Add(trackBar3);
            Controls.Add(trackBar2);
            Controls.Add(ThicknessLevel);
            Controls.Add(trackBar1);
            Controls.Add(DrawTrapezoid);
            Controls.Add(label3);
            Controls.Add(SelectShape);
            Controls.Add(label2);
            Controls.Add(panel1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(DrawLine);
            Controls.Add(DrawTriangle);
            Controls.Add(DrawCircle);
            Controls.Add(DrawSquare);
            DoubleBuffered = true;
            Name = "PaintGDI";
            Text = "PaintGDI";
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RadioButton DrawSquare;
        private RadioButton DrawCircle;
        private RadioButton DrawTriangle;
        private RadioButton DrawLine;
        private Label label1;
        private ColorDialog colorDialog1;
        private Button button1;
        private Button button2;
        private Panel panel1;
        private Label label2;
        private RadioButton SelectShape;
        private Label label3;
        private RadioButton DrawTrapezoid;
        private TrackBar trackBar1;
        private Label ThicknessLevel;
        private BindingSource bindingSource1;
        private TrackBar trackBar2;
        private TrackBar trackBar3;
        private Label label4;
        private Label Rotate;
        private TextBox textBox1;
        private Button ExportBtn;
    }
}
