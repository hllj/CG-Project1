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
            this.vertexControl1 = new SharpGL.Controls.VertexControl();
            this.openGLControl = new SharpGL.OpenGLControl();
            this.btnLine = new System.Windows.Forms.Button();
            this.btnCircle = new System.Windows.Forms.Button();
            this.colorPalette = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.labelMode = new System.Windows.Forms.Label();
            this.btn_rectangle = new System.Windows.Forms.Button();
            this.btn_Triangles = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
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
            this.openGLControl.Location = new System.Drawing.Point(10, 72);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(554, 396);
            this.openGLControl.TabIndex = 1;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInitialized);
            this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl_OpenGLDraw);
            this.openGLControl.Resized += new System.EventHandler(this.openGLControl_Resized);
            this.openGLControl.Load += new System.EventHandler(this.openGLControl_Load);
            this.openGLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseDown);
            this.openGLControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseMove);
            this.openGLControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseUp);
            // 
            // btnLine
            // 
            this.btnLine.Location = new System.Drawing.Point(10, 10);
            this.btnLine.Margin = new System.Windows.Forms.Padding(2);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(68, 44);
            this.btnLine.TabIndex = 2;
            this.btnLine.Text = "Line";
            this.btnLine.UseVisualStyleBackColor = true;
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnCircle
            // 
            this.btnCircle.Location = new System.Drawing.Point(88, 10);
            this.btnCircle.Margin = new System.Windows.Forms.Padding(2);
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(68, 44);
            this.btnCircle.TabIndex = 3;
            this.btnCircle.Text = "Circle";
            this.btnCircle.UseVisualStyleBackColor = true;
            this.btnCircle.Click += new System.EventHandler(this.btnCircle_Click);
            // 
            // colorPalette
            // 
            this.colorPalette.Location = new System.Drawing.Point(496, 10);
            this.colorPalette.Margin = new System.Windows.Forms.Padding(2);
            this.colorPalette.Name = "colorPalette";
            this.colorPalette.Size = new System.Drawing.Size(68, 44);
            this.colorPalette.TabIndex = 4;
            this.colorPalette.Text = "Color Palette";
            this.colorPalette.UseVisualStyleBackColor = true;
            this.colorPalette.Click += new System.EventHandler(this.colorPalette_Click);
            // 
            // labelMode
            // 
            this.labelMode.AutoSize = true;
            this.labelMode.Location = new System.Drawing.Point(403, 10);
            this.labelMode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelMode.Name = "labelMode";
            this.labelMode.Size = new System.Drawing.Size(43, 13);
            this.labelMode.TabIndex = 6;
            this.labelMode.Text = "Mode : ";
            // 
            // btn_rectangle
            // 
            this.btn_rectangle.Location = new System.Drawing.Point(160, 11);
            this.btn_rectangle.Margin = new System.Windows.Forms.Padding(2);
            this.btn_rectangle.Name = "btn_rectangle";
            this.btn_rectangle.Size = new System.Drawing.Size(80, 44);
            this.btn_rectangle.TabIndex = 3;
            this.btn_rectangle.Text = "RECTANGLE";
            this.btn_rectangle.UseVisualStyleBackColor = true;
            this.btn_rectangle.Click += new System.EventHandler(this.btnRectangle_Click);
            // 
            // btn_Triangles
            // 
            this.btn_Triangles.Location = new System.Drawing.Point(244, 10);
            this.btn_Triangles.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Triangles.Name = "btn_Triangles";
            this.btn_Triangles.Size = new System.Drawing.Size(80, 44);
            this.btn_Triangles.TabIndex = 3;
            this.btn_Triangles.Text = "TRIANGLES";
            this.btn_Triangles.UseVisualStyleBackColor = true;
            this.btn_Triangles.Click += new System.EventHandler(this.btn_Triangles_click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 486);
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
    }
}

