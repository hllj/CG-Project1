﻿using SharpGL;
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

        public const int SHAPE_LINE = 0;
        public const int SHAPE_CIRCLE = 1;
        public const int SHAPE_RECTANGLE = 2;
        public const int SHAPE_ELLIPSE = 3;
        public const int SHAPE_EQUI_TRIANGLE = 4;
        public const int SHAPE_EQUI_PENTAGON = 5;
        public const int SHAPE_EQUI_HEXAGON = 6;
        public const int SHAPE_POLYGON = 7;

        // Đồng hồ đếm thời gian
        Timer drawingTime;
        int time_elapsed = 0; // millisecond
        BindingList<string> listTimeSpans;
        
        int selectedShape = -1;
        List<ShapeType> listShapes;
        private bool startup; // biến kiểm tra chương trình được khởi động lần đầu
        Affine affine;

        Color userColor;
        short shape;
        Point pStart, pEnd, pointSelected;

        Point selectedPoint;
        Boolean renderMode = false;

        string strMode;

        Timer initializeNewTimer()
        {
            Timer res = new Timer();
            res.Interval = 100;
            res.Tick += timer1_Tick;
            return res;
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

        private void reDraw(int index)
        {
            OpenGL gl = openGLControl.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.ClearColor(1, 1, 1, 1);

            for (int i = 0; i < listShapes.Count; i++)
            {
                if (i != index)
                {
                    gl.Color(listShapes[i].color.R / 255.0, listShapes[i].color.G / 255.0, listShapes[i].color.B / 255.0);
                    listShapes[i].Draw(gl);
                    if (listShapes[i].filling)
                    {
                        if (listShapes[i].scanLine)
                            listShapes[i].ScanLine(gl);
                        else if (listShapes[i].boundaryFill)
                            listShapes[i].BoundaryFill(gl);
                    }
                }

            }

        }

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
            selectedPoint = new Point();
            affine = new Affine();
            labelMode.Text = strMode + "Select";
            renderMode = false;
            drawingTime = initializeNewTimer();
            listTimeSpans = new BindingList<string>();
            listBoxTimeSpans.DataSource = listTimeSpans;
        }

        private void openGLControl_Load(object sender, EventArgs e)
        {

        }

        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            // Set the clear color.
            gl.ClearColor(1, 1, 1, 1);
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

            // đoạn code này để đổi backGround thành màu trắng
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            if (startup)
            {
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
                gl.ClearColor(1, 1, 1, 1);

                startup = false;
                return;
            }

            if ((int)openGLControl.Tag == OPENGL_IDLE || renderMode == false)
                return;
            // Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            reDraw(-1);


            ShapeType newShape;

            if (labelMode.Text == strMode + "Translate" || labelMode.Text == strMode + "Rotate" || labelMode.Text == strMode + "Scale")
            {
                newShape = listShapes[selectedShape].Clone(gl);
                newShape.Transform(affine, gl);
                //Không vẽ selectedShape
                reDraw(selectedShape);
                //Vẽ lại clone
                newShape.Draw(gl);

                if (newShape.filling)
                {
                    if (newShape.scanLine) newShape.ScanLine(gl);
                    else newShape.BoundaryFill(gl);
                }

                //Đã kéo xong
                if ((int)openGLControl.Tag == OPENGL_DRAWN)
                {
                    listShapes[selectedShape].p1 = newShape.p1;
                    listShapes[selectedShape].p2 = newShape.p2;
                    listShapes[selectedShape] = newShape.Clone(gl);
                    reDraw(-1);
                    affine = new Affine();
                    openGLControl.Tag = OPENGL_IDLE;
                }
                return;
            }


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
                    //newShape = new Polygon();
                    newShape = listShapes.Last();
                    if (newShape.id != SHAPE_POLYGON || newShape.Done == true)
                    {
                        openGLControl.Tag = OPENGL_IDLE;
                        return;
                    }
                    newShape.color = userColor;
                    newShape.thickness = (float)numericUpDown1.Value;
                    return;

                default:
                    newShape = new EquiHexagon()
                    {
                        id = SHAPE_EQUI_HEXAGON
                    };
                    break;
            }

            newShape.color = userColor;
            newShape.thickness = (float)numericUpDown1.Value;
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
            time_elapsed = 0;
            //drawingTime = initializeNewTimer();
            drawingTime.Start();
            if (labelMode.Text == strMode + "Select")
            {
                openGLControl.Tag = OPENGL_IDLE;
                return;
            }
            if (labelMode.Text == strMode + "Polygon") // không xử lý event này trong mode polygon
                return;
            if (labelMode.Text == strMode + "Translate" || labelMode.Text == strMode + "Rotate" || labelMode.Text == strMode + "Scale")
            {
                selectedPoint = e.Location;
                openGLControl.Tag = OPENGL_DRAWING;
                return;
            }
            openGLControl.Tag = OPENGL_DRAWING;
            pStart = e.Location;
            pEnd = pStart;
        }

        private void openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            drawingTime.Stop();
            string newTimeSpan = labelTimer.Text;
            listTimeSpans.Add(newTimeSpan);
            listBoxTimeSpans.SelectedIndex = listTimeSpans.Count - 1;
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
                //Sua o day
                affine = new Affine();
                affine.Translate(e.Location.X - selectedPoint.X, -e.Location.Y + selectedPoint.Y);
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
                    selectedShape = index;
                    changeToSelectMode();
                }
                return;
            }
            if (labelMode.Text == strMode + "Polygon")
            {
                // xử lý event mouse click trong chế độ polygon 

                if (e.Button == MouseButtons.Right)
                {

                    // chuột phải thì kết thúc quá trình vẽ 
                    if ((int)openGLControl.Tag == OPENGL_DRAWING)
                    {

                        ShapeType tmp = new Polygon();
                        tmp = listShapes.Last();
                        tmp.Done = true;
                        tmp.p1 = tmp.controlPoints.Last();
                        tmp.p2 = tmp.controlPoints[0];
                        pEnd = tmp.controlPoints.Last();
                        tmp.AddEdge(openGLControl.OpenGL);


                    }


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
                            p2 = new Point(pStart.X, pStart.Y),
                            Vertex = new List<Point>()


                        };
                        listShapes.Add(tmp);


                        pEnd = e.Location;
                    }

                    Point t = new Point(e.Location.X, openGLControl.OpenGL.RenderContextProvider.Height - e.Location.Y);
                    listShapes.Last().controlPoints.Add(t);
                    listShapes.Last().Vertex.Add(t);
                    listShapes.Last().p1 = pStart;
                    listShapes.Last().p2 = pEnd;

                    pEnd = pStart;
                }
                return;
            }
            if (labelMode.Text == strMode + "Translate")
            {

                return;
            }
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

        private void btn_Polygon_Click(object sender, EventArgs e)
        {
            shape = SHAPE_POLYGON;
            labelMode.Text = strMode + "Polygon";
            OpenGL gl = openGLControl.OpenGL;
            gl.RenderMode(OpenGL.GL_RENDER);
            renderMode = true;
        }

        private void btn_Scale_Click(object sender, EventArgs e)
        {
            if (labelMode.Text == strMode + "Select")
            {
                labelMode.Text = strMode + "Scale";
                OpenGL gl = openGLControl.OpenGL;
                gl.RenderMode(OpenGL.GL_RENDER);
                renderMode = true;
            }
        }

        private void btn_Rotate_Click(object sender, EventArgs e)
        {
            if (labelMode.Text == strMode + "Select")
            {
                labelMode.Text = strMode + "Rotate";
                OpenGL gl = openGLControl.OpenGL;
                gl.RenderMode(OpenGL.GL_RENDER);
                renderMode = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time_elapsed += 100;
            //int minute = time_elapsed / 1000 / 60;
            //int second = time_elapsed / 1000 % 60;
            //int milsec = time_elapsed % 1000;
            int minute = time_elapsed / 1000 / 60;
            int second = time_elapsed / 1000 % 60;
            int milsec = time_elapsed % 1000 / 100;
            labelTimer.Text = $"{minute.ToString().PadLeft(2,'0')}:{second.ToString().PadLeft(2,'0')}.{milsec.ToString()}";
        }


        private void btn_ColorFilling_Click(object sender, EventArgs e)
        {
            if (renderMode == true || selectedShape == -1) return;
            labelMode.Text = strMode + "Color Filling";
            openGLControl.OpenGL.RenderMode(OpenGL.GL_RENDER);
            listShapes[selectedShape].colorFilling = userColor;
            listShapes[selectedShape].filling = true;
            if (rbtn_ScanLine.Checked) listShapes[selectedShape].scanLine = true;
            if (rbtn_Bound.Checked) listShapes[selectedShape].boundaryFill = true;
            if (listShapes[selectedShape].scanLine)
                listShapes[selectedShape].ScanLine(openGLControl.OpenGL);
            else if (listShapes[selectedShape].boundaryFill)
            {
                listShapes[selectedShape].BoundaryFill(openGLControl.OpenGL);  
            }
                
            changeToSelectMode();

        }
        
        private void openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
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
                    //sua o day
                    affine = new Affine();
                    affine.Translate(e.Location.X - selectedPoint.X, -e.Location.Y + selectedPoint.Y);

                    return;
                }
                if (labelMode.Text == strMode + "Scale")
                {
                    affine = new Affine();
                    int xmin = listShapes[selectedShape].controlPoints[0].X,
                        xmax = xmin,
                        ymin = listShapes[selectedShape].controlPoints[0].Y,
                        ymax = ymin;
                    int x = 0, y = 0, n = listShapes[selectedShape].controlPoints.Count;
                    for (int i = 0; i < n; i++)
                    {
                        xmin = listShapes[selectedShape].controlPoints[i].X < xmin ? listShapes[selectedShape].controlPoints[i].X : xmin;
                        xmax = listShapes[selectedShape].controlPoints[i].X > xmax ? listShapes[selectedShape].controlPoints[i].X : xmax;
                        ymin = listShapes[selectedShape].controlPoints[i].Y < ymin ? listShapes[selectedShape].controlPoints[i].Y : ymin;
                        ymax = listShapes[selectedShape].controlPoints[i].Y > ymax ? listShapes[selectedShape].controlPoints[i].Y : ymax;
                    }
                    x /= n;
                    y /= n;
                    Point center = new Point(x, y);

                    float Sx=1,
                        Sy=1;
                    int dx=xmin, dy=ymin;
                    
                    if (selectedPoint.X > xmin && openGLControl.OpenGL.RenderContextProvider.Height-selectedPoint.Y > ymin)
                    {   
                        // GOC PHAN TU THU 1
                        dx = xmin;
                        dy = ymin;
                        Sx = (float)(e.Location.X - dx) / (float)(xmax - xmin);
                        Sy = (float)(openGLControl.OpenGL.RenderContextProvider.Height - e.Location.Y - ymin) / (float)(ymax - ymin);
                    }
                    if(selectedPoint.X > xmin && openGLControl.OpenGL.RenderContextProvider.Height - selectedPoint.Y < ymin)
                    {
                        // GOC Phan tu thu 4
                        dx = xmin;
                        dy = ymax;
                        Sx = (float)(e.Location.X - xmin)/ (float)(xmax - xmin);
                        Sy = (float)(ymax - openGLControl.OpenGL.RenderContextProvider.Height + e.Location.Y ) / (float)(ymax - ymin);
                    }
                    if (selectedPoint.X < xmin && openGLControl.OpenGL.RenderContextProvider.Height - selectedPoint.Y > ymin)
                    {
                        // goc phan tu thu 2
                        dx = xmax;
                        dy = ymin;
                        Sx = (float)(xmax - e.Location.X ) / (float)(xmax - xmin);
                        Sy = (float)(openGLControl.OpenGL.RenderContextProvider.Height - e.Location.Y - ymin) / (float)(ymax - ymin);
                    }
                    if (selectedPoint.X < xmin && openGLControl.OpenGL.RenderContextProvider.Height - selectedPoint.Y < ymin)
                    {
                        // goc phan tu thu 3
                        dx = xmax;
                        dy = ymax;
                        Sx = (float)(xmax -e.Location.X) / (float)(xmax - xmin);
                        Sy = (float)(ymax - openGLControl.OpenGL.RenderContextProvider.Height + e.Location.Y ) / (float)(ymax - ymin);
                    }

                    affine.Translate(dx, dy);
                    
                    affine.Scale(Sx, Sy);
                    affine.Translate(-dx, -dy);                

                    return;
                }
                if (labelMode.Text == strMode + "Rotate")
                {
                    affine = new Affine();

                    int xmin = listShapes[selectedShape].controlPoints[0].X,
                        xmax = xmin,
                        ymin = listShapes[selectedShape].controlPoints[0].Y,
                        ymax = ymin;
                    int x = 0, y = 0, n = listShapes[selectedShape].controlPoints.Count;
                    for (int i = 0; i < n; i++)
                    {
                        x += listShapes[selectedShape].controlPoints[i].X;
                        y += listShapes[selectedShape].controlPoints[i].Y;
                        xmin = listShapes[selectedShape].controlPoints[i].X < xmin ? listShapes[selectedShape].controlPoints[i].X : xmin;
                        xmax = listShapes[selectedShape].controlPoints[i].X > xmax ? listShapes[selectedShape].controlPoints[i].X : xmax;
                        ymin = listShapes[selectedShape].controlPoints[i].Y < ymin ? listShapes[selectedShape].controlPoints[i].Y : ymin;
                        ymax = listShapes[selectedShape].controlPoints[i].Y > ymax ? listShapes[selectedShape].controlPoints[i].Y : ymax;
                    }
                    x /= n;
                    y /= n;

                    affine.Translate(x, y);
                    Point u = new Point(selectedPoint.X - x, openGLControl.OpenGL.RenderContextProvider.Height-selectedPoint.Y - y);
                    Point v = new Point(e.Location.X - x, openGLControl.OpenGL.RenderContextProvider.Height - e.Location.Y - y);
                    double a = (double)(openGLControl.OpenGL.RenderContextProvider.Height - selectedPoint.Y - y) / (double)(selectedPoint.X - x);
                    double b = y - a * x;

                    double tu = u.X * v.X+u.Y*v.Y;
                    double mau = Math.Sqrt(u.X * u.X + u.Y * u.Y) * Math.Sqrt(v.X * v.X + v.Y * v.Y);
                   
                    double alpha = 0;
                    alpha = Math.Acos(tu / mau);
                    if (selectedPoint.X > x) alpha = -alpha;
                    
                    if (a * e.Location.X + b - openGLControl.OpenGL.RenderContextProvider.Height + e.Location.Y > 0)
                        affine.Rotate(alpha);
                    else
                        affine.Rotate(-alpha);
                    affine.Translate(-x, -y);
                    return;
                }
                pEnd = e.Location;
            }
            pEnd = e.Location;
         
        }

    }
}
