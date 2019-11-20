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
        const int SHAPE_POLYGON = 7;

        // Vẽ ellipse cần bao nhiêu điểm điều khiển?


        List<ShapeType> listShapes;

        private bool startup; // biến kiểm tra chương trình được khởi động lần đầu
        Color userColor;
        short shape;
        Point pStart, pEnd;
        
        Point pTmp;
        
        string strMode;

        public Form1()
        {
            InitializeComponent();
            listShapes = new List<ShapeType>();
            List<Point> pointsOfShape = new List<Point>();
            userColor = Color.Black;
            shape = SHAPE_LINE;
            openGLControl.Tag = OPENGL_IDLE;
            strMode = labelMode.Text;
            labelMode.Text = strMode + "Line";
            startup = true;  // 

        }

        private void openGLControl_Load(object sender, EventArgs e)
        {

        }

        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            // Set the clear color.
            gl.ClearColor(1, 1, 1, 1); // set clearcolor to white
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

            /* **************************************/
            if (startup) // đoạn code này để đổi backGround thành màu trắng
            {
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
                gl.ClearColor(1, 1, 1, 1);
                Line tmp = new Line { p1 = new Point(0, 0), p2 = new Point(0, 0) };
                tmp.Draw(gl);
                startup = false;
                return;
            }
            /* **************************************/

            if ((int)openGLControl.Tag == OPENGL_IDLE)
            {
                return;
            }


            // Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.ClearColor(1, 1, 1, 1);



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
                case SHAPE_POLYGON:
                    newShape = new Polygon();
                    newShape = listShapes.Last();
                    if (newShape.id != SHAPE_POLYGON || newShape.Done == true )
                    {                        
                        return;
                    }
                    newShape.color = userColor;
                    return;
                    
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

        private void btn_Polygon_Click(object sender, EventArgs e)
        {
            // button Polygon
            shape = SHAPE_POLYGON;
            labelMode.Text = strMode + "Polygon";
        }
        private void openGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (labelMode.Text == strMode + "Polygon") // không xử lý event này trong mode polygon
                return;
            openGLControl.Tag = OPENGL_DRAWING;
            pStart = e.Location;
            pEnd = pStart;
        }

        private void openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (labelMode.Text == strMode + "Polygon") // không xử lý event này trong mode polygon
                return;
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

        private void openGLControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (labelMode.Text == strMode + "Polygon")
            {
                // xử lý event mouse click trong chế độ polygon 

                if (e.Button == MouseButtons.Right)
                {

                    // chuột phải thì kết thúc quá trình vẽ 
                    if ((int)openGLControl.Tag == OPENGL_DRAWING)
                    {
                        pEnd = e.Location;
                        ShapeType tmp = new Polygon();
                        tmp = listShapes.Last();
                        tmp.Done = true;
                        tmp.p1 = tmp.Control_points.Last();
                        tmp.p2 = tmp.Control_points[0];
                    }
                    openGLControl.Tag = OPENGL_DRAWN;
                }
                if (e.Button == MouseButtons.Left)
                {
                    // xử lý click chuột trái
                    pStart = e.Location;

                    openGLControl.Tag = OPENGL_DRAWING;
                    ShapeType tmp;
                    if (listShapes.Count == 0 || listShapes.Last().id != SHAPE_POLYGON || listShapes.Last().Done)
                    {
                        // nếu trong danh sách các hình đã vẽ, nếu hình cuối vẽ chưa xong, hoạc chưa có hình nào
                        //* hoặc ko phải là 1 polygon thì tạo 1 polygon mới thêm vào listshape
                        tmp = new Polygon()
                        {
                            id = SHAPE_POLYGON,
                            Done = false,
                            p1 = new Point(pStart.X, pStart.Y),
                            p2 = new Point(pStart.X, pStart.Y)

                        };
                        listShapes.Add(tmp);
                    }

                    Point t = new Point(pStart.X, pStart.Y);
                    t = pStart;
                    listShapes.Last().Control_points.Add(t);
                    listShapes.Last().p1 = pStart;
                    listShapes.Last().p2 = pEnd;
                    /* if (listShapes.Last().id == SHAPE_POLYGON && listShapes.Last().Done == false)
                     {

                         listShapes.Last().p1 = new Point(pStart.X, pStart.Y);
                         listShapes.Last().p2 = new Point(pEnd.X, pEnd.Y);
                     }*/

                    pEnd = pStart;
                }
            }

            return;

        }

        private void openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if ((int)openGLControl.Tag == OPENGL_DRAWING)
            {
                pEnd = e.Location;

                if (labelMode.Text == strMode + "Polygon")
                    listShapes.Last().p2 = pEnd;
            }
        }

    }
}
