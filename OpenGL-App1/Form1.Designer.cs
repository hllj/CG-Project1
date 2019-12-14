﻿namespace OpenGL_App1
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
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // vertexControl1
            // 
            this.vertexControl1.Location = new System.Drawing.Point(405, 0);
            this.vertexControl1.Margin = new System.Windows.Forms.Padding(2);
            this.vertexControl1.Name = "vertexControl1";
            this.vertexControl1.Size = new System.Drawing.Size(6, 6);
            this.vertexControl1.TabIndex = 0;
            // 
            // openGLControl
            // 
            this.openGLControl.DrawFPS = false;
            this.openGLControl.ForeColor = System.Drawing.SystemColors.Window;
            this.openGLControl.Location = new System.Drawing.Point(10, 114);
            this.openGLControl.Margin = new System.Windows.Forms.Padding(4);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(645, 452);
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
            this.btnLine.Location = new System.Drawing.Point(10, 10);
            this.btnLine.Margin = new System.Windows.Forms.Padding(2);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(59, 44);
            this.btnLine.TabIndex = 2;
            this.btnLine.Text = "Line";
            this.btnLine.UseVisualStyleBackColor = true;
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnCircle
            // 
            this.btnCircle.Location = new System.Drawing.Point(74, 10);
            this.btnCircle.Margin = new System.Windows.Forms.Padding(2);
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(49, 44);
            this.btnCircle.TabIndex = 3;
            this.btnCircle.Text = "Circle";
            this.btnCircle.UseVisualStyleBackColor = true;
            this.btnCircle.Click += new System.EventHandler(this.btnCircle_Click);
            // 
            // colorPalette
            // 
            this.colorPalette.Location = new System.Drawing.Point(757, 11);
            this.colorPalette.Margin = new System.Windows.Forms.Padding(2);
            this.colorPalette.Name = "colorPalette";
            this.colorPalette.Size = new System.Drawing.Size(68, 43);
            this.colorPalette.TabIndex = 4;
            this.colorPalette.Text = "Color Palette";
            this.colorPalette.UseVisualStyleBackColor = true;
            this.colorPalette.Click += new System.EventHandler(this.colorPalette_Click);
            // 
            // labelMode
            // 
            this.labelMode.AutoSize = true;
            this.labelMode.Location = new System.Drawing.Point(665, 542);
            this.labelMode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelMode.Name = "labelMode";
            this.labelMode.Size = new System.Drawing.Size(43, 13);
            this.labelMode.TabIndex = 6;
            this.labelMode.Text = "Mode : ";
            // 
            // btn_rectangle
            // 
            this.btn_rectangle.Location = new System.Drawing.Point(200, 10);
            this.btn_rectangle.Margin = new System.Windows.Forms.Padding(2);
            this.btn_rectangle.Name = "btn_rectangle";
            this.btn_rectangle.Size = new System.Drawing.Size(67, 44);
            this.btn_rectangle.TabIndex = 3;
            this.btn_rectangle.Text = "Rectangle";
            this.btn_rectangle.UseVisualStyleBackColor = true;
            this.btn_rectangle.Click += new System.EventHandler(this.btnRectangle_Click);
            // 
            // btn_Triangles
            // 
            this.btn_Triangles.Location = new System.Drawing.Point(128, 10);
            this.btn_Triangles.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Triangles.Name = "btn_Triangles";
            this.btn_Triangles.Size = new System.Drawing.Size(67, 44);
            this.btn_Triangles.TabIndex = 3;
            this.btn_Triangles.Text = "Triangle";
            this.btn_Triangles.UseVisualStyleBackColor = true;
            this.btn_Triangles.Click += new System.EventHandler(this.btn_Triangles_click);
            // 
            // btn_equipentagon
            // 
            this.btn_equipentagon.Location = new System.Drawing.Point(433, 10);
            this.btn_equipentagon.Name = "btn_equipentagon";
            this.btn_equipentagon.Size = new System.Drawing.Size(82, 44);
            this.btn_equipentagon.TabIndex = 7;
            this.btn_equipentagon.Text = "EquiPentagon";
            this.btn_equipentagon.UseVisualStyleBackColor = true;
            this.btn_equipentagon.Click += new System.EventHandler(this.btn_equipentagon_Click);
            // 
            // btn_EquiHexagon
            // 
            this.btn_EquiHexagon.Location = new System.Drawing.Point(345, 10);
            this.btn_EquiHexagon.Name = "btn_EquiHexagon";
            this.btn_EquiHexagon.Size = new System.Drawing.Size(82, 44);
            this.btn_EquiHexagon.TabIndex = 8;
            this.btn_EquiHexagon.Text = "EquiHexagon";
            this.btn_EquiHexagon.UseVisualStyleBackColor = true;
            this.btn_EquiHexagon.Click += new System.EventHandler(this.btn_EquiHexagon_Click);
            // 
            // btn_Ellipse
            // 
            this.btn_Ellipse.Location = new System.Drawing.Point(272, 10);
            this.btn_Ellipse.Name = "btn_Ellipse";
            this.btn_Ellipse.Size = new System.Drawing.Size(67, 44);
            this.btn_Ellipse.TabIndex = 9;
            this.btn_Ellipse.Text = "Ellipse";
            this.btn_Ellipse.UseVisualStyleBackColor = true;
            this.btn_Ellipse.Click += new System.EventHandler(this.btn_Ellipse_Click);
            // 
            // btn_Select
            // 
            this.btn_Select.Location = new System.Drawing.Point(10, 58);
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Size = new System.Drawing.Size(59, 44);
            this.btn_Select.TabIndex = 10;
            this.btn_Select.Text = "Select";
            this.btn_Select.UseVisualStyleBackColor = true;
            this.btn_Select.Click += new System.EventHandler(this.btn_Select_Click);
            // 
            // btn_ColorFilling
            // 
            this.btn_ColorFilling.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_ColorFilling.BackgroundImage")));
            this.btn_ColorFilling.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ColorFilling.Location = new System.Drawing.Point(74, 58);
            this.btn_ColorFilling.Name = "btn_ColorFilling";
            this.btn_ColorFilling.Size = new System.Drawing.Size(49, 44);
            this.btn_ColorFilling.TabIndex = 11;
            this.btn_ColorFilling.UseVisualStyleBackColor = true;
            this.btn_ColorFilling.Click += new System.EventHandler(this.btn_ColorFilling_Click);
            // 
            // btn_Translate
            // 
            this.btn_Translate.Location = new System.Drawing.Point(128, 58);
            this.btn_Translate.Name = "btn_Translate";
            this.btn_Translate.Size = new System.Drawing.Size(67, 44);
            this.btn_Translate.TabIndex = 12;
            this.btn_Translate.Text = "Translate";
            this.btn_Translate.UseVisualStyleBackColor = true;
            this.btn_Translate.Click += new System.EventHandler(this.btn_Translate_Click);
            // 
            // btn_Rotate
            // 
            this.btn_Rotate.Location = new System.Drawing.Point(200, 58);
            this.btn_Rotate.Name = "btn_Rotate";
            this.btn_Rotate.Size = new System.Drawing.Size(67, 44);
            this.btn_Rotate.TabIndex = 13;
            this.btn_Rotate.Text = "Rotate";
            this.btn_Rotate.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtn_Bound);
            this.groupBox1.Controls.Add(this.rbtn_ScanLine);
            this.groupBox1.Location = new System.Drawing.Point(668, 114);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(119, 68);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Color Filling";
            // 
            // rbtn_Bound
            // 
            this.rbtn_Bound.AutoSize = true;
            this.rbtn_Bound.Location = new System.Drawing.Point(7, 44);
            this.rbtn_Bound.Name = "rbtn_Bound";
            this.rbtn_Bound.Size = new System.Drawing.Size(85, 17);
            this.rbtn_Bound.TabIndex = 1;
            this.rbtn_Bound.Text = "Boundary Fill";
            this.rbtn_Bound.UseVisualStyleBackColor = true;
            // 
            // rbtn_ScanLine
            // 
            this.rbtn_ScanLine.AutoSize = true;
            this.rbtn_ScanLine.Checked = true;
            this.rbtn_ScanLine.Location = new System.Drawing.Point(7, 20);
            this.rbtn_ScanLine.Name = "rbtn_ScanLine";
            this.rbtn_ScanLine.Size = new System.Drawing.Size(73, 17);
            this.rbtn_ScanLine.TabIndex = 0;
            this.rbtn_ScanLine.TabStop = true;
            this.rbtn_ScanLine.Text = "Scan Line";
            this.rbtn_ScanLine.UseVisualStyleBackColor = true;
            // 
            // btn_Polygon
            // 
            this.btn_Polygon.Location = new System.Drawing.Point(522, 10);
            this.btn_Polygon.Name = "btn_Polygon";
            this.btn_Polygon.Size = new System.Drawing.Size(75, 43);
            this.btn_Polygon.TabIndex = 15;
            this.btn_Polygon.Text = "Polygon";
            this.btn_Polygon.UseVisualStyleBackColor = true;
            this.btn_Polygon.Click += new System.EventHandler(this.btn_Polygon_Click);
            // 
            // btn_Scale
            // 
            this.btn_Scale.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Scale.Location = new System.Drawing.Point(274, 58);
            this.btn_Scale.Name = "btn_Scale";
            this.btn_Scale.Size = new System.Drawing.Size(65, 44);
            this.btn_Scale.TabIndex = 16;
            this.btn_Scale.Text = "Scale";
            this.btn_Scale.UseVisualStyleBackColor = true;
            this.btn_Scale.Click += new System.EventHandler(this.btn_Scale_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 571);
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
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "SimpleDraw";
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
    }
}

