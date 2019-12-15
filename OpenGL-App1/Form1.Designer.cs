namespace OpenGL_App1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.vertexControl1 = new SharpGL.Controls.VertexControl();
            this.openGLControl = new SharpGL.OpenGLControl();
            this.btnLine = new System.Windows.Forms.Button();
            this.btnCircle = new System.Windows.Forms.Button();
            this.colorPalette = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.labelMode = new System.Windows.Forms.Label();
            this.btn_rectangle = new System.Windows.Forms.Button();
            this.btn_Triangles = new System.Windows.Forms.Button();
            this.btn_equipentagon = new System.Windows.Forms.Button();
            this.btn_EquiHexagon = new System.Windows.Forms.Button();
            this.btn_Ellipse = new System.Windows.Forms.Button();
            this.btn_Select = new System.Windows.Forms.Button();
            this.btn_ColorFilling = new System.Windows.Forms.Button();
            this.btn_Translate = new System.Windows.Forms.Button();
            this.btn_Rotate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtn_Bound = new System.Windows.Forms.RadioButton();
            this.rbtn_ScanLine = new System.Windows.Forms.RadioButton();
            this.btn_Polygon = new System.Windows.Forms.Button();
            this.btn_Scale = new System.Windows.Forms.Button();
            this.labelTimer = new System.Windows.Forms.Label();
            this.listBoxTimeSpans = new System.Windows.Forms.ListBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // vertexControl1
            // 
            this.vertexControl1.Location = new System.Drawing.Point(540, 0);
            this.vertexControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.vertexControl1.Name = "vertexControl1";
            this.vertexControl1.Size = new System.Drawing.Size(8, 7);
            this.vertexControl1.TabIndex = 0;
            // 
            // openGLControl
            // 
            this.openGLControl.DrawFPS = false;
            this.openGLControl.ForeColor = System.Drawing.SystemColors.Window;
            this.openGLControl.Location = new System.Drawing.Point(13, 140);
            this.openGLControl.Margin = new System.Windows.Forms.Padding(5);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(860, 556);
            this.openGLControl.TabIndex = 1;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInitialized);
            this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl_OpenGLDraw);
            this.openGLControl.Resized += new System.EventHandler(this.openGLControl_Resized);
            this.openGLControl.Load += new System.EventHandler(this.openGLControl_Load);
            this.openGLControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseClick);
            this.openGLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseDown);
            this.openGLControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseMove);
            this.openGLControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseUp);
            // 
            // btnLine
            // 
            this.btnLine.Location = new System.Drawing.Point(13, 12);
            this.btnLine.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(79, 54);
            this.btnLine.TabIndex = 2;
            this.btnLine.Text = "Line";
            this.btnLine.UseVisualStyleBackColor = true;
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnCircle
            // 
            this.btnCircle.Location = new System.Drawing.Point(99, 12);
            this.btnCircle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(65, 54);
            this.btnCircle.TabIndex = 3;
            this.btnCircle.Text = "Circle";
            this.btnCircle.UseVisualStyleBackColor = true;
            this.btnCircle.Click += new System.EventHandler(this.btnCircle_Click);
            // 
            // colorPalette
            // 
            this.colorPalette.Location = new System.Drawing.Point(1009, 14);
            this.colorPalette.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.colorPalette.Name = "colorPalette";
            this.colorPalette.Size = new System.Drawing.Size(91, 53);
            this.colorPalette.TabIndex = 4;
            this.colorPalette.Text = "Color Palette";
            this.colorPalette.UseVisualStyleBackColor = true;
            this.colorPalette.Click += new System.EventHandler(this.colorPalette_Click);
            // 
            // labelMode
            // 
            this.labelMode.AutoSize = true;
            this.labelMode.Location = new System.Drawing.Point(888, 677);
            this.labelMode.Name = "labelMode";
            this.labelMode.Size = new System.Drawing.Size(55, 17);
            this.labelMode.TabIndex = 6;
            this.labelMode.Text = "Mode : ";
            // 
            // btn_rectangle
            // 
            this.btn_rectangle.Location = new System.Drawing.Point(267, 12);
            this.btn_rectangle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_rectangle.Name = "btn_rectangle";
            this.btn_rectangle.Size = new System.Drawing.Size(89, 54);
            this.btn_rectangle.TabIndex = 3;
            this.btn_rectangle.Text = "Rectangle";
            this.btn_rectangle.UseVisualStyleBackColor = true;
            this.btn_rectangle.Click += new System.EventHandler(this.btnRectangle_Click);
            // 
            // btn_Triangles
            // 
            this.btn_Triangles.Location = new System.Drawing.Point(171, 12);
            this.btn_Triangles.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Triangles.Name = "btn_Triangles";
            this.btn_Triangles.Size = new System.Drawing.Size(89, 54);
            this.btn_Triangles.TabIndex = 3;
            this.btn_Triangles.Text = "Triangle";
            this.btn_Triangles.UseVisualStyleBackColor = true;
            this.btn_Triangles.Click += new System.EventHandler(this.btn_Triangles_click);
            // 
            // btn_equipentagon
            // 
            this.btn_equipentagon.Location = new System.Drawing.Point(577, 12);
            this.btn_equipentagon.Margin = new System.Windows.Forms.Padding(4);
            this.btn_equipentagon.Name = "btn_equipentagon";
            this.btn_equipentagon.Size = new System.Drawing.Size(109, 54);
            this.btn_equipentagon.TabIndex = 7;
            this.btn_equipentagon.Text = "EquiPentagon";
            this.btn_equipentagon.UseVisualStyleBackColor = true;
            this.btn_equipentagon.Click += new System.EventHandler(this.btn_equipentagon_Click);
            // 
            // btn_EquiHexagon
            // 
            this.btn_EquiHexagon.Location = new System.Drawing.Point(460, 12);
            this.btn_EquiHexagon.Margin = new System.Windows.Forms.Padding(4);
            this.btn_EquiHexagon.Name = "btn_EquiHexagon";
            this.btn_EquiHexagon.Size = new System.Drawing.Size(109, 54);
            this.btn_EquiHexagon.TabIndex = 8;
            this.btn_EquiHexagon.Text = "EquiHexagon";
            this.btn_EquiHexagon.UseVisualStyleBackColor = true;
            this.btn_EquiHexagon.Click += new System.EventHandler(this.btn_EquiHexagon_Click);
            // 
            // btn_Ellipse
            // 
            this.btn_Ellipse.Location = new System.Drawing.Point(363, 12);
            this.btn_Ellipse.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Ellipse.Name = "btn_Ellipse";
            this.btn_Ellipse.Size = new System.Drawing.Size(89, 54);
            this.btn_Ellipse.TabIndex = 9;
            this.btn_Ellipse.Text = "Ellipse";
            this.btn_Ellipse.UseVisualStyleBackColor = true;
            this.btn_Ellipse.Click += new System.EventHandler(this.btn_Ellipse_Click);
            // 
            // btn_Select
            // 
            this.btn_Select.Location = new System.Drawing.Point(13, 71);
            this.btn_Select.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Size = new System.Drawing.Size(79, 54);
            this.btn_Select.TabIndex = 10;
            this.btn_Select.Text = "Select";
            this.btn_Select.UseVisualStyleBackColor = true;
            this.btn_Select.Click += new System.EventHandler(this.btn_Select_Click);
            // 
            // btn_ColorFilling
            // 
            this.btn_ColorFilling.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_ColorFilling.BackgroundImage")));
            this.btn_ColorFilling.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ColorFilling.Location = new System.Drawing.Point(99, 71);
            this.btn_ColorFilling.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ColorFilling.Name = "btn_ColorFilling";
            this.btn_ColorFilling.Size = new System.Drawing.Size(65, 54);
            this.btn_ColorFilling.TabIndex = 11;
            this.btn_ColorFilling.UseVisualStyleBackColor = true;
            this.btn_ColorFilling.Click += new System.EventHandler(this.btn_ColorFilling_Click);
            // 
            // btn_Translate
            // 
            this.btn_Translate.Location = new System.Drawing.Point(171, 71);
            this.btn_Translate.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Translate.Name = "btn_Translate";
            this.btn_Translate.Size = new System.Drawing.Size(89, 54);
            this.btn_Translate.TabIndex = 12;
            this.btn_Translate.Text = "Translate";
            this.btn_Translate.UseVisualStyleBackColor = true;
            this.btn_Translate.Click += new System.EventHandler(this.btn_Translate_Click);
            // 
            // btn_Rotate
            // 
            this.btn_Rotate.Location = new System.Drawing.Point(267, 71);
            this.btn_Rotate.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Rotate.Name = "btn_Rotate";
            this.btn_Rotate.Size = new System.Drawing.Size(89, 54);
            this.btn_Rotate.TabIndex = 13;
            this.btn_Rotate.Text = "Rotate";
            this.btn_Rotate.UseVisualStyleBackColor = true;
            this.btn_Rotate.Click += new System.EventHandler(this.btn_Rotate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtn_Bound);
            this.groupBox1.Controls.Add(this.rbtn_ScanLine);
            this.groupBox1.Location = new System.Drawing.Point(891, 140);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(159, 84);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Color Filling";
            // 
            // rbtn_Bound
            // 
            this.rbtn_Bound.AutoSize = true;
            this.rbtn_Bound.Location = new System.Drawing.Point(9, 54);
            this.rbtn_Bound.Margin = new System.Windows.Forms.Padding(4);
            this.rbtn_Bound.Name = "rbtn_Bound";
            this.rbtn_Bound.Size = new System.Drawing.Size(111, 21);
            this.rbtn_Bound.TabIndex = 1;
            this.rbtn_Bound.Text = "Boundary Fill";
            this.rbtn_Bound.UseVisualStyleBackColor = true;
            // 
            // rbtn_ScanLine
            // 
            this.rbtn_ScanLine.AutoSize = true;
            this.rbtn_ScanLine.Checked = true;
            this.rbtn_ScanLine.Location = new System.Drawing.Point(9, 25);
            this.rbtn_ScanLine.Margin = new System.Windows.Forms.Padding(4);
            this.rbtn_ScanLine.Name = "rbtn_ScanLine";
            this.rbtn_ScanLine.Size = new System.Drawing.Size(92, 21);
            this.rbtn_ScanLine.TabIndex = 0;
            this.rbtn_ScanLine.TabStop = true;
            this.rbtn_ScanLine.Text = "Scan Line";
            this.rbtn_ScanLine.UseVisualStyleBackColor = true;
            // 
            // btn_Polygon
            // 
            this.btn_Polygon.Location = new System.Drawing.Point(696, 12);
            this.btn_Polygon.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Polygon.Name = "btn_Polygon";
            this.btn_Polygon.Size = new System.Drawing.Size(100, 53);
            this.btn_Polygon.TabIndex = 15;
            this.btn_Polygon.Text = "Polygon";
            this.btn_Polygon.UseVisualStyleBackColor = true;
            this.btn_Polygon.Click += new System.EventHandler(this.btn_Polygon_Click);
            // 
            // btn_Scale
            // 
            this.btn_Scale.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Scale.Location = new System.Drawing.Point(365, 71);
            this.btn_Scale.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Scale.Name = "btn_Scale";
            this.btn_Scale.Size = new System.Drawing.Size(87, 54);
            this.btn_Scale.TabIndex = 16;
            this.btn_Scale.Text = "Scale";
            this.btn_Scale.UseVisualStyleBackColor = true;
            this.btn_Scale.Click += new System.EventHandler(this.btn_Scale_Click);
            // 
            // labelTimer
            // 
            this.labelTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTimer.ForeColor = System.Drawing.Color.Red;
            this.labelTimer.Location = new System.Drawing.Point(946, 632);
            this.labelTimer.Name = "labelTimer";
            this.labelTimer.Size = new System.Drawing.Size(120, 32);
            this.labelTimer.TabIndex = 17;
            this.labelTimer.Text = "00:05.123";
            this.labelTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBoxTimeSpans
            // 
            this.listBoxTimeSpans.FormattingEnabled = true;
            this.listBoxTimeSpans.ItemHeight = 16;
            this.listBoxTimeSpans.Location = new System.Drawing.Point(891, 250);
            this.listBoxTimeSpans.Name = "listBoxTimeSpans";
            this.listBoxTimeSpans.Size = new System.Drawing.Size(175, 356);
            this.listBoxTimeSpans.TabIndex = 18;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(887, 43);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(105, 22);
            this.numericUpDown1.TabIndex = 19;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(887, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 20;
            this.label1.Text = "Thickness";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 703);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.listBoxTimeSpans);
            this.Controls.Add(this.labelTimer);
            this.Controls.Add(this.btn_Scale);
            this.Controls.Add(this.btn_Polygon);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Rotate);
            this.Controls.Add(this.btn_Translate);
            this.Controls.Add(this.btn_ColorFilling);
            this.Controls.Add(this.btn_Select);
            this.Controls.Add(this.btn_Ellipse);
            this.Controls.Add(this.btn_EquiHexagon);
            this.Controls.Add(this.btn_equipentagon);
            this.Controls.Add(this.labelMode);
            this.Controls.Add(this.colorPalette);
            this.Controls.Add(this.btn_Triangles);
            this.Controls.Add(this.btn_rectangle);
            this.Controls.Add(this.btnCircle);
            this.Controls.Add(this.btnLine);
            this.Controls.Add(this.openGLControl);
            this.Controls.Add(this.vertexControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "SimpleDraw";
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.Controls.VertexControl vertexControl1;
        private SharpGL.OpenGLControl openGLControl;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Button btnCircle;
        private System.Windows.Forms.Button colorPalette;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label labelMode;
        private System.Windows.Forms.Button btn_rectangle;
        private System.Windows.Forms.Button btn_Triangles;
        private System.Windows.Forms.Button btn_equipentagon;
        private System.Windows.Forms.Button btn_EquiHexagon;
        private System.Windows.Forms.Button btn_Ellipse;
        private System.Windows.Forms.Button btn_Select;
        private System.Windows.Forms.Button btn_ColorFilling;
        private System.Windows.Forms.Button btn_Translate;
        private System.Windows.Forms.Button btn_Rotate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtn_Bound;
        private System.Windows.Forms.RadioButton rbtn_ScanLine;
        private System.Windows.Forms.Button btn_Polygon;
        private System.Windows.Forms.Button btn_Scale;
        private System.Windows.Forms.Label labelTimer;
        private System.Windows.Forms.ListBox listBoxTimeSpans;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
    }
}

