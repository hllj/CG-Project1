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
/*Huan
 * Thêm: 
 * 2 button: Ellipse, Select
 * Hàm changeToSelect: Chuyển GL_RENDER thành GL_SELECT, Mặc định là Select, nhấn vào button vẽ mới vẽ được
 * Hàm ReDraw: Vẽ lại các shape
 * Biến Boolean renderMode: Kiểm tra xem đang ở SELECT hay RENDER
 * Action: MouseClick: Khi nhấn vào thì hiện lên control point
 */

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


        // Vẽ ellipse cần bao nhiêu điểm điều khiển?

        int shapeSelected = -1;
        List<ShapeType> listShapes;

        Color userColor;
        short shape;
        Point pStart, pEnd, pointSelected;

        Boolean renderMode = false;

        string strMode;

        private void changeToSelectMode()
        {
            System.UInt32[] selectO;
            selectO = new System.UInt32[512];

            OpenGL gl = openGLControl.OpenGL;
            gl.SelectBuffer(512, selectO);
            gl.RenderMode(OpenGL.GL_SELECT);
            renderMode = false;
        }

        private void ReDraw()
        {
            OpenGL gl = openGLControl.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            for (int i = 0; i < listShapes.Count; i++)
            {
                listShapes[i].Draw(gl);
                if (listShapes[i].filling) listShapes[i].ScanLine(gl);
            }
        }

        public Form1()
        {
            InitializeComponent();
            listShapes = new List<ShapeType>();
            List<Point> pointsOfShape = new List<Point>();
            userColor = Color.White;
            shape = SHAPE_LINE;
            openGLControl.Tag = OPENGL_IDLE;
            strMode = labelMode.Text;
            labelMode.Text = strMode + "Select";
            renderMode = false;
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
            if ((int)openGLControl.Tag == OPENGL_IDLE || renderMode == false)
                return;
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            // Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            ReDraw();

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
                newShape.Create(gl);
                listShapes.Add(newShape);
                openGLControl.Tag = OPENGL_IDLE;
                
            }
            
        }

  
        // Event handlers

        private void btnLine_Click(object sender, EventArgs e)
        {
            shape = SHAPE_LINE;
            labelMode.Text = strMode + "Line";
            OpenGL gl = openGLControl.OpenGL;
            gl.RenderMode(OpenGL.GL_RENDER);
            renderMode = true;
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            shape = SHAPE_CIRCLE;
            labelMode.Text = strMode + "Circle";
            OpenGL gl = openGLControl.OpenGL;
            gl.RenderMode(OpenGL.GL_RENDER);
            renderMode = true;
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
            OpenGL gl = openGLControl.OpenGL;
            gl.RenderMode(OpenGL.GL_RENDER);
            renderMode = true;
        }

        private void btn_Triangles_click(object sender, EventArgs e)
        {
            shape = SHAPE_EQUI_TRIANGLE;
            labelMode.Text = strMode + "Triangle";
            OpenGL gl = openGLControl.OpenGL;
            gl.RenderMode(OpenGL.GL_RENDER);
            renderMode = true;
        }

        private void btn_equipentagon_Click(object sender, EventArgs e)
        {
            shape = SHAPE_EQUI_PENTAGON;
            labelMode.Text = strMode + "EquiPentagon";
            OpenGL gl = openGLControl.OpenGL;
            gl.RenderMode(OpenGL.GL_RENDER);
            renderMode = true;
        }

        private void btn_EquiHexagon_Click(object sender, EventArgs e)
        {
            shape = SHAPE_EQUI_HEXAGON;
            labelMode.Text = strMode + "EquiHexagon";
            OpenGL gl = openGLControl.OpenGL;
            gl.RenderMode(OpenGL.GL_RENDER);
            renderMode = true;
        }

        private void btn_Ellipse_Click(object sender, EventArgs e)
        {
            shape = SHAPE_ELLIPSE;
            labelMode.Text = strMode + "Ellipse";
            OpenGL gl = openGLControl.OpenGL;
            gl.RenderMode(OpenGL.GL_RENDER);
            renderMode = true;
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            labelMode.Text = strMode + "Select";
            changeToSelectMode();
        }

        private void openGLControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (renderMode == true)
            {
                return;
            }
            pointSelected = e.Location;
            double minDist = double.MaxValue;
            double esp = 10;
            int index = -1;
            for (int i = 0; i < listShapes.Count; i++)
            {
                for (int j = 0; j < listShapes[i].controlPoints.Count; j++)
                {
                    int x = listShapes[i].controlPoints[j].X;
                    int y = listShapes[i].controlPoints[j].Y;
                    double dist = Math.Sqrt((e.Location.X - x) * (e.Location.X - x)
                    + (openGLControl.OpenGL.RenderContextProvider.Height - e.Location.Y - y) * (openGLControl.OpenGL.RenderContextProvider.Height - e.Location.Y - y));
                    if (dist <= esp)
                    {
                        if (dist < minDist)
                        {
                            minDist = dist;
                            index = i;
                        }
                    }
                }
            }

            if (index != -1)
            {
                shapeSelected = index;
                openGLControl.OpenGL.RenderMode(OpenGL.GL_RENDER);
                ReDraw();
                listShapes[index].DrawControlPoints(openGLControl.OpenGL);
                changeToSelectMode();
            }
            else shapeSelected = index;
        }

        private void btn_ColorFilling_Click(object sender, EventArgs e)
        {
            if (renderMode == true || shapeSelected == -1) return;
            labelMode.Text = strMode + "Color Filling";
            //ColorFilling cl = new ColorFilling();
            //cl.init(openGLControl.OpenGL);
            //RGBColor F, B;
            //F.r = userColor.R;
            //F.g = userColor.G;
            //F.b = userColor.B;
            //B.r = userColor.R;
            //B.g = userColor.G;
            //B.b = userColor.B;
            //cl.BoudaryFill(pointSelected.X, pointSelected.Y, F, B);
            
            openGLControl.OpenGL.RenderMode(OpenGL.GL_RENDER);
            listShapes[shapeSelected].colorFilling = userColor;
            listShapes[shapeSelected].filling = true;
           listShapes[shapeSelected].BoudaryFill(openGLControl.OpenGL);
          //  listShapes[shapeSelected].ScanLine(openGLControl.OpenGL);
            changeToSelectMode();
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
