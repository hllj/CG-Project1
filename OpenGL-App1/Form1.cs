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
        // States of OpenGL screen
        const int OPENGL_IDLE = 0;
        const int OPENGL_DRAWING = 1;
        const int OPENGL_DRAWN = 2;

        const int SHAPE_LINE = 0;
        const int SHAPE_CIRCLE = 1;
        const int SHAPE_RECTANGLE = 2;
        const int SHAPE_ELLIPSE = 3;
        const int SHAPE_EQUI_TRIANGLE = 4;
        const int SHAPE_EQUI_PENTAGON = 5;
        const int SHAPE_EQUI_HEXAGON = 6;
        const int CL = 7;

        // Vẽ ellipse cần bao nhiêu điểm điều khiển?
        

        List<ShapeType> listShapes;

        Color userColor;
        short shape;
        Point pStart, pEnd;
        
       
        string strMode;

        public Form1()
        {
            InitializeComponent();
            listShapes = new List<ShapeType>();
            List<Point> pointsOfShape = new List<Point>();
            userColor = Color.White;
            shape = SHAPE_LINE;
            openGLControl.Tag = OPENGL_IDLE;
            strMode = labelMode.Text;
            labelMode.Text = strMode + "Line";
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
            if ((int)openGLControl.Tag == OPENGL_IDLE)
                return;
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            // Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);



            for (int i = 0; i < listShapes.Count; i++)
            {
                listShapes[i].Draw(gl);
            }

            gl.Color(userColor.R / 255.0, userColor.G / 255.0, userColor.B / 255.0);


            ShapeType newShape;

            // Vẽ vời chỗ này. Ví dụ:
            switch (shape)
            {
                case SHAPE_LINE:
                    newShape = new Line()
                    {
                        id = SHAPE_LINE
                    };
                    break;
                case SHAPE_CIRCLE:
                    newShape = new Circle()
                    {
                        id = SHAPE_CIRCLE
                    };
                    break;
                case SHAPE_RECTANGLE:
                    newShape = new Rectangle()
                    {
                        id = SHAPE_RECTANGLE
                    };
                    //gl.Begin(OpenGL.GL_POLYGON);
                    ///* xác định các đỉnh của hình chữ nhật */
                    //gl.Vertex(pStart.X,gl.RenderContextProvider.Height - pStart.Y);
                    //gl.Vertex(pEnd.X, gl.RenderContextProvider.Height -pStart.Y);
                    //gl.Vertex(pEnd.X, gl.RenderContextProvider.Height - pEnd.Y);
                    //gl.Vertex(pStart.X, gl.RenderContextProvider.Height - pEnd.Y);
                    //gl.End();
                    //gl.Flush();
                    break;
                case SHAPE_ELLIPSE:
                    newShape = new Ellipse()
                    {
                        id = SHAPE_ELLIPSE
                    };
                    break;

                case SHAPE_EQUI_TRIANGLE:
                    newShape = new EquiTriangle()
                    {
                        id = SHAPE_EQUI_TRIANGLE
                    };


                    break;
                case SHAPE_EQUI_PENTAGON:
                    newShape = new EquiPentagon()
                    {
                        id = SHAPE_EQUI_PENTAGON
                    };
                    break;
                case CL:
                    newShape = new Line();
                    ColorFilling cl = new ColorFilling();
                    RGBColor F, B;
                    byte[] ptr = new byte[3];
                    for (int i = 0; i < 400000; i++)
                    {
                        gl.ReadPixels(pStart.X, gl.RenderContextProvider.Height - pStart.Y, 1, 1, format: OpenGL.GL_RGB, type: OpenGL.GL_BYTE, ptr);
                    }

                    F.r = userColor.R;
                    F.g = userColor.G;
                    F.b = userColor.B;
                    B.r = userColor.R;
                    B.g = userColor.G;
                    B.b = userColor.B;
                    RGBColor X;
                    X.r = 255;
                    X.g = 72;
                    X.b = 0;
                    cl.init(openGLControl.OpenGL, openGLControl.Width, openGLControl.Height);
                    RGBColor curColor = cl.GetPixel(pStart.X, pStart.Y);
                    //cl.isSameColor()
                    
                    if ((!cl.isSameColor(curColor, F)) && (!cl.isSameColor(curColor, B))) ;
                        cl.BoudaryFill(pStart.X, pStart.Y, X, B);
                    break;
                default:
                    newShape = new EquiHexagon()
                    {
                        id = SHAPE_EQUI_HEXAGON
                    };
                    break;
                    
            }

            newShape.color = userColor;
            newShape.p1 = new Point(pStart.X, pStart.Y);
            newShape.p2 = new Point(pEnd.X, pEnd.Y);
            newShape.Draw(gl);
            if ((int)openGLControl.Tag == OPENGL_DRAWN)
            {
                listShapes.Add(newShape);
                openGLControl.Tag = OPENGL_IDLE;
            }
            
        }


        // Event handlers

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

        private void openGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            openGLControl.Tag = OPENGL_DRAWING;
            pStart = e.Location;
            pEnd = pStart;
        }

        private void openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            openGLControl.Tag = OPENGL_DRAWN;
            pEnd = e.Location;
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            shape = SHAPE_RECTANGLE;
            labelMode.Text = strMode + "Rectangle";
        }

        private void btn_Triangles_click(object sender, EventArgs e)
        {
            shape = SHAPE_EQUI_TRIANGLE;
            labelMode.Text = strMode + "Triangle";
        }

        private void btn_equipentagon_Click(object sender, EventArgs e)
        {
            shape = SHAPE_EQUI_PENTAGON;
            labelMode.Text = strMode + "EquiPentagon";
        }

        private void btn_EquiHexagon_Click(object sender, EventArgs e)
        {
            shape = SHAPE_EQUI_HEXAGON;
            labelMode.Text = strMode + "EquiHexagon";
        }

        private void openGLControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                pStart = e.Location;
                shape = CL;
            }
        }

        private void openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if ((int)openGLControl.Tag == OPENGL_DRAWING)
            {
                pEnd = e.Location;
            }
        }
        
    }
}
