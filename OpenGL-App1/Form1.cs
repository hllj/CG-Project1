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
        Color userColor;
        short shape; // 0 - Line, 1 - Circle
        Point pStart, pEnd;
        public Form1()
        {
            InitializeComponent();
            userColor = Color.White;
            shape = 0;
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
            if (shape == 0)
            {
                gl.Begin(OpenGL.GL_LINES);
                gl.Vertex(pStart.X, gl.RenderContextProvider.Height - pStart.Y);
                gl.Vertex(pEnd.X, gl.RenderContextProvider.Height - pEnd.Y);
                gl.End();
                gl.Flush();
            }
            else
            {
                gl.Begin(OpenGL.GL_)

            }



            ////gl.Color(1f, 0, 0, 0); // Chọn màu đỏ
            //gl.Begin(OpenGL.GL_TRIANGLES); // Chọn chế độ vẽ tam giác
            //gl.Vertex2sv(new short[] { 0, 0 }); // Đỉnh thứ 1 tọa độ 0,0
            //gl.Vertex2sv(new short[] { 100, 100 }); // Đỉnh thứ 2 tọa độ 100, 100
            //gl.Vertex2sv(new short[] { 200, 0 }); // Đỉnh thứ 3 tọa độ 200, 0
            //gl.End();
            //gl.Flush();// Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            shape = 0;
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            shape = 1;
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
            pEnd = e.Location;
        }

        private void openGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            pStart = e.Location;
            pEnd = pStart;
        }
    }
}
