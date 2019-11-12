using SharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenGL_App1
{
    public partial class Form1 : Form
    {
        const int SHAPE_LINE = 0;
        const int SHAPE_CIRCLE = 1;
        const int SHAPE_RECTANGLE = 2;
        Color userColor;
        short shape;
        Point pStart, pEnd;
        bool isMouseDown;
        string strMode;

        public Form1()
        {
            InitializeComponent();
            userColor = Color.White;
            shape = SHAPE_LINE;
            isMouseDown = false;
            strMode = labelMode.Text;
        }

        private void openGLControl_Load(object sender, EventArgs e)
        {

        }

        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            // Set the clear color.
            gl.ClearColor(0, 0, 0, 0);
            // Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            // Load the identity.
            gl.LoadIdentity();
        }

        private void openGLControl_Resized(object sender, EventArgs e)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            // Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            // Load the identity.
            gl.LoadIdentity();
            // Create a perspective transformation.
            gl.Viewport(0, 0, openGLControl.Width, openGLControl.Height);
            gl.Ortho2D(0, openGLControl.Width, 0, openGLControl.Height);
        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs e)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            // Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            gl.Color(userColor.R / 255.0, userColor.G / 255.0, userColor.B / 255.0);

            // Vẽ vời chỗ này. Ví dụ:
            switch (shape)
            {
                
                case SHAPE_LINE:
                    gl.Begin(OpenGL.GL_LINES);
                    gl.Vertex(pStart.X, gl.RenderContextProvider.Height - pStart.Y);
                    gl.Vertex(pEnd.X, gl.RenderContextProvider.Height - pEnd.Y);
                    gl.End();
                    gl.Flush();
                    break;
                case SHAPE_CIRCLE:
                    break;
                case SHAPE_RECTANGLE:
                    /* xác định các đỉnh của đa giác */
                    gl.Vertex(pStart.X,gl.RenderContextProvider.Height - pStart.Y); 
                    gl.Vertex(pEnd.X, gl.RenderContextProvider.Height -pStart.Y);
                    gl.Vertex(pEnd.X, gl.RenderContextProvider.Height - pEnd.Y);
                    gl.Vertex(pStart.X, gl.RenderContextProvider.Height - pEnd.Y);
                    gl.End();
                    gl.Flush();
                    break;
                default: break;
            }

        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            shape = SHAPE_LINE;
            labelMode.Text = strMode + "Line";
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            shape = SHAPE_CIRCLE;
            labelMode.Text = strMode + "Circle";
        }

        private void colorPalette_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                userColor = colorDialog1.Color;
            }
        }

        private void openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            pEnd = e.Location;
        }

        private void openGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            pStart = e.Location;
            pEnd = pStart;
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            shape = SHAPE_RECTANGLE;
            labelMode.Text = strMode + "Rectangle";;
        }

        private void openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                pEnd = e.Location;
            }
        }
        
    }
}
