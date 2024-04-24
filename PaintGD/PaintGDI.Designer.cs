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
            radioButton5 = new RadioButton();
            label3 = new Label();
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
            button1.Location = new Point(62, 328);
            button1.Name = "button1";
            button1.Size = new Size(92, 29);
            button1.TabIndex = 6;
            button1.Text = "Color";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(62, 293);
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
            label2.Location = new Point(46, 256);
            label2.Name = "label2";
            label2.Size = new Size(58, 20);
            label2.TabIndex = 9;
            label2.Text = "Actions";
            // 
            // radioButton5
            // 
            radioButton5.AutoSize = true;
            radioButton5.Location = new Point(62, 218);
            radioButton5.Name = "radioButton5";
            radioButton5.Size = new Size(115, 24);
            radioButton5.TabIndex = 10;
            radioButton5.TabStop = true;
            radioButton5.Text = "Select Shape";
            radioButton5.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(46, 186);
            label3.Name = "label3";
            label3.Size = new Size(51, 20);
            label3.TabIndex = 11;
            label3.Text = "Cursor";
            // 
            // PaintGDI
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1151, 760);
            Controls.Add(label3);
            Controls.Add(radioButton5);
            Controls.Add(label2);
            Controls.Add(panel1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(DrawLine);
            Controls.Add(DrawTriangle);
            Controls.Add(DrawCircle);
            Controls.Add(DrawSquare);
            Name = "PaintGDI";
            Text = "PaintGDI";
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
        private RadioButton radioButton5;
        private Label label3;
    }
}
