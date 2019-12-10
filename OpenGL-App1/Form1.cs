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
using System.Threading;

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
        int Selected_shape;
        Point Selected_point;
        Boolean renderMode = false;
        Point pTmp;
        Affine affine;
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
            startup = true;  //
            Selected_point = new Point();
            affine = new Affine();
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

        private void reDraw(int index)
        {
            OpenGL gl = openGLControl.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.ClearColor(1, 1, 1, 1);
            for (int i = 0; i < listShapes.Count; i++)
            {
                if (i != index)
                    listShapes[i].Draw(gl);
            }

        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs e)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            
            // đoạn code này để đổi backGround thành màu trắng
            if (startup) 
            {
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
                gl.ClearColor(1, 1, 1, 1);
                
                startup = false;
                return;
            }
            /* **************************************/

            if ((int)openGLControl.Tag == OPENGL_IDLE || renderMode == false)
                return;
            reDraw(-1);
            ShapeType newShape;
            if (labelMode.Text == strMode + "Translate")
            {
                newShape = listShapes[Selected_shape].Clone();
                newShape.Transform(affine);
                
                reDraw(Selected_shape);
                newShape.Draw(gl);
                //Đã kéo xong
                if ((int)openGLControl.Tag == OPENGL_DRAWN)
                {
                    listShapes[Selected_shape].p1 = newShape.p1;
                    listShapes[Selected_shape].p2 = newShape.p2;
                    listShapes[Selected_shape].Update(gl);
                    reDraw(-1);
                    affine = new Affine();
                    openGLControl.Tag = OPENGL_IDLE;
                }
                return;
            }

            // Clear the color and depth buffer.

            gl.Color(userColor.R / 255.0, userColor.G / 255.0, userColor.B / 255.0);

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
                case SHAPE_POLYGON:
                    newShape = new Polygon();
                    newShape = listShapes.Last();
                    if (newShape.id != SHAPE_POLYGON || newShape.Done == true)
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
                newShape.Create(gl);
                openGLControl.Tag = OPENGL_IDLE;
            }
        }



        private void changeToSelectMode()
        {
            System.UInt32[] selectO;
            selectO = new System.UInt32[512];

            OpenGL gl = openGLControl.OpenGL;
            gl.SelectBuffer(512, selectO);
            gl.RenderMode(OpenGL.GL_SELECT);
            renderMode = false;
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
            if (labelMode.Text == strMode + "Select")
            {
                openGLControl.Tag = OPENGL_IDLE;
                return;
            }
            if (labelMode.Text == strMode + "Polygon") // không xử lý event này trong mode polygon
                return;
            if (labelMode.Text == strMode + "Translate")
            {
                Selected_point = e.Location;
                openGLControl.Tag = OPENGL_DRAWING;
                return;
            }
            openGLControl.Tag = OPENGL_DRAWING;
            pStart = e.Location;
            pEnd = pStart;
        }

        private void openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (labelMode.Text == strMode + "Select")
            {
                openGLControl.Tag = OPENGL_IDLE;
                return;
            }
            if ((int)openGLControl.Tag == OPENGL_DRAWING)
            {


                if (labelMode.Text == strMode + "Polygon")
                {
                    listShapes.Last().p2 = pEnd;
                    pEnd = e.Location;
                    return;
                }
                if (labelMode.Text == strMode + "Translate")
                {

                    affine.Transform(e.Location.X - Selected_point.X, e.Location.Y - Selected_point.Y);
                    if (listShapes[Selected_shape].id == SHAPE_POLYGON)
                        affine.Transform(e.Location.X - Selected_point.X, e.Location.Y - Selected_point.Y);
                    return;
                }
                pEnd = e.Location;
            }
        }
        private void openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (renderMode == false)
            {
                openGLControl.Tag = OPENGL_IDLE;
                return;
            }
            if (labelMode.Text == strMode + "Polygon") // không xử lý event này trong mode polygon
                return;
            if (labelMode.Text == strMode + "Translate")
            {
                openGLControl.Tag = OPENGL_DRAWN;
                affine.Transform(e.Location.X - Selected_point.X, e.Location.Y - Selected_point.Y);
                return;
            }

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

        private void openGLControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (renderMode == false) // mode select hanlde event
            {
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
                    openGLControl.OpenGL.RenderMode(OpenGL.GL_RENDER);
                    reDraw(-1);
                    listShapes[index].DrawControlPoints(openGLControl.OpenGL);
                    Selected_shape = index;
                    changeToSelectMode();
                }
            }
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
                        tmp.p1 = tmp.controlPoints.Last();
                        tmp.p2 = tmp.controlPoints[0];
                        tmp.Create(openGLControl.OpenGL);
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

                    Point t = new Point(e.Location.X, openGLControl.OpenGL.RenderContextProvider.Height - e.Location.Y);
                    //t = e.Location;
                    listShapes.Last().controlPoints.Add(t);
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
            if (labelMode.Text == strMode + "Translate")
            {

                return;
            }

        }



        private void btn_Polygon_Click(object sender, EventArgs e)
        {
            // button Polygon
            shape = SHAPE_POLYGON;
            labelMode.Text = strMode + "Polygon";
            OpenGL gl = openGLControl.OpenGL;
            gl.RenderMode(OpenGL.GL_RENDER);
            renderMode = true;
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            labelMode.Text = strMode + "Select";
            changeToSelectMode();
        }

        private void btn_Translate_Click(object sender, EventArgs e)
        {
            if (labelMode.Text == strMode + "Select")
            {
                labelMode.Text = strMode + "Translate";
                OpenGL gl = openGLControl.OpenGL;
                gl.RenderMode(OpenGL.GL_RENDER);
                renderMode = true;
            }

        }

        private void btn_Ellipse_Click(object sender, EventArgs e)
        {
            shape = SHAPE_ELLIPSE;
            labelMode.Text = strMode + "Ellipse";
            OpenGL gl = openGLControl.OpenGL;
            gl.RenderMode(OpenGL.GL_RENDER);
            renderMode = true;
        }

    }


}