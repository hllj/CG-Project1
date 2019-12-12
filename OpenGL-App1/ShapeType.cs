﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;

/*Thêm:
 * Biến controlPoints: Lưu trữ các control của Shape
 * Hàm Create: Thêm các điểm control point vào
 * Hàm DrawControlPoints: Vẽ các điểm control point khi nhất Select
 * Cách vẽ hình Ellipse
 */
 public struct AEL
{
    public int y_upper, y_lower;
    public double x, slope;
}
namespace OpenGL_App1
{
    abstract public class ShapeType
    {
        public int id { get; set; }
        public bool Done;
        public int ymin, ymax;
        public Boolean filling = false, scanLine = false;
        public Color colorFilling;
        public Point p1 { get; set; } // starting point
        public Point p2 { get; set; } // ending point
        public List<Point> controlPoints;
        public Color color { get; set; }
        public List<List<AEL>> ET;
        public List<AEL> BegList, Edge;
        public List<Point> Vertex;
        public Point tam;
        
        abstract public void Draw(OpenGL gl);
        abstract public void Create(OpenGL gl);

        public ShapeType()
        {
            Done = false;
            controlPoints = new List<Point>();
            //Vertex = new List<Point>();
        }
        //cap nhat lai au khi tranform
        //khong can nua, Clone thoi
        public void Update(OpenGL gl)
        {
            controlPoints = new List<Point>();
            //them dinh? nua
            //
            Create(gl);
        }
        public void Transform(Affine at)
        {
            for (int i = 0; i < controlPoints.Count; i++)
            {
                controlPoints[i] = at.Transform(controlPoints[i]);
            }
            p1 = at.Transform(p1);
            p2 = at.Transform(p2);
        }

        public void AddEdge(OpenGL gl)
        {
            /*Them cac canh*/
            Edge = new List<AEL>();
            AEL t;
            ymin = Vertex[0].Y;
            ymax = Vertex[0].Y;
            for (int i = 0; i < Vertex.Count - 1; i++)
            {
                t = new AEL();
                t.y_upper = Math.Max(Vertex[i].Y, Vertex[i + 1].Y);
                t.y_lower = Math.Min(Vertex[i].Y, Vertex[i + 1].Y);
                t.x = Vertex[i].Y > Vertex[i + 1].Y ? Vertex[i + 1].X : Vertex[i].X;
                if (t.y_lower == t.y_upper)
                {
                    t.slope = 0;
                }
                else
                {
                    t.slope = (Vertex[i].X - Vertex[i + 1].X) * 1.0 / (Vertex[i].Y - Vertex[i + 1].Y) * 1.0;
                }
                
                // Tinh chỉnh lại dữ liệu
                //TH1: Giảm:
                //TH2: Tăng
                Console.Write(t.y_upper + " ");
                if (i == 0)
                {
                    if (Vertex[i + 1].Y < Vertex[i].Y && Vertex[i].Y < Vertex[Vertex.Count - 1].Y)
                    {
                        t.y_upper--;
                    }
                    else
                    if (Vertex[i].Y > Vertex[Vertex.Count - 1].Y && Vertex[i + 1].Y > Vertex[i].Y)
                    {
                        t.y_upper--;
                    }
                } else
                {
                    Console.Write(Vertex[i - 1].Y + " " + Vertex[i].Y + " " + Vertex[i + 1].Y + " ");
                    if (Vertex[i + 1].Y < Vertex[i].Y && Vertex[i].Y < Vertex[i - 1].Y)
                    {
                        t.y_upper--;
                    } else
                    if (Vertex[i].Y > Vertex[i - 1].Y && Vertex[i + 1].Y > Vertex[i].Y)
                    {
                        t.y_upper--;
                    }
                }
                Edge.Add(t);
                ymin = Math.Min(ymin, Vertex[i + 1].Y);
                ymax = Math.Max(ymax, Vertex[i + 1].Y);
                Console.WriteLine(t.y_upper + " " + t.y_lower + " " + t.x);
            }

            t = new AEL();
            t.y_upper = Math.Max(Vertex[Vertex.Count - 1].Y, Vertex[0].Y);
            t.y_lower = Math.Min(Vertex[Vertex.Count - 1].Y, Vertex[0].Y);
            t.x = Vertex[Vertex.Count - 1].Y > Vertex[0].Y ? Vertex[0].X : Vertex[Vertex.Count - 1].X;
            Console.Write(t.y_upper + " ");
            if (t.y_lower == t.y_upper)
            {
                t.slope = 0;
            }
            else
            {
                t.slope = (Vertex[Vertex.Count - 1].X - Vertex[0].X) * 1.0 / (Vertex[Vertex.Count - 1].Y - Vertex[0].Y) * 1.0;
            }

            if (Vertex[0].Y < Vertex[Vertex.Count - 1].Y && Vertex[Vertex.Count - 1].Y < Vertex[Vertex.Count - 2].Y)
            {
                t.y_upper--;
            }
            else
            if (Vertex[Vertex.Count - 1].Y > Vertex[Vertex.Count - 2].Y && Vertex[0].Y > Vertex[Vertex.Count - 1].Y)
            {
                t.y_upper--;
            }
            Edge.Add(t);
            Console.WriteLine(t.y_upper + " " + t.y_lower + " " + t.x);
        }
        virtual public ShapeType Clone(OpenGL gl)
        {
            ShapeType t = new Rectangle();

            for (int i = 0; i < controlPoints.Count; i++)
            {
                t.controlPoints.Add(controlPoints[i]);
            }
            t.Vertex = new List<Point>();
            for (int i = 0; i < Vertex.Count; i++)
            {
                Point p = new Point(Vertex[i].X,Vertex[i].Y);
                t.Vertex.Add(p);
            }
            t.AddEdge(gl);
            t.color = color;
            t.colorFilling = colorFilling;
            t.filling = filling;
            t.Done = Done;
            t.ymax = ymax;
            t.ymin = ymin;
            t.scanLine = scanLine;
            t.id = id;
            t.p1 = new Point(p1.X, p1.Y);
            t.p2 = new Point(p2.X, p2.Y);

            return t;
        }
        virtual public void DrawControlPoints(OpenGL gl)
        {
            for (int i = 0; i < controlPoints.Count; i++)
            {
                gl.Color(Color.Red.R, Color.Red.G, Color.Red.B);
                gl.PointSize(5);
                gl.Begin(OpenGL.GL_POINTS);
                Point a = controlPoints[i];
                gl.Vertex(a.X, a.Y);
                gl.End();
                gl.Flush();
            }
            gl.PointSize(1);
        }

        public void BoundaryFill(OpenGL gl)
        {
            Draw(gl);
            ColorFilling cl = new ColorFilling();
            cl.init(gl);
            RGBColor F, B;
            F.r = color.R; F.g = color.G; F.b = color.B;
            B.r = color.R; B.g = color.G; B.b = color.B;
            cl.BoudaryFill(tam.X, tam.Y, F, B);
        }
        virtual public void ScanLine(OpenGL gl)
        {
            ET = new List<List<AEL>>();
            BegList = new List<AEL>();
            /*Tạo Edge Table*/
            for (int y = ymin; y <= ymax; y++)
            {
                List<AEL> subList = new List<AEL>();
                ET.Add(subList);
            }
            
            for (int i = 0; i < Edge.Count; i++)
            {
                if (Edge[i].y_lower != Edge[i].y_upper)
                {
                    int y = Edge[i].y_lower;
                    ET[y - ymin].Add(Edge[i]);
                }
            }
            /**/
            for (int y = ymin; y <= ymax; y++)
            {
                
                for (int i = 0; i < ET[y - ymin].Count; i++) BegList.Add(ET[y - ymin][i]);
                Console.Write((y - ymin) + " " + BegList.Count + " ");
                /*Sort*/
                BegList = BegList.OrderBy(o => o.x).ToList();
                /*Lấp*/
                gl.Color(colorFilling.R / 255.0, colorFilling.G / 255.0, colorFilling.B / 255.0);
                gl.Begin(OpenGL.GL_LINES);
                for (int i = 0; i < BegList.Count; i += 2)
                {
                    if (i + 1 < BegList.Count)
                    {
                        gl.Vertex(BegList[i].x, y);
                        gl.Vertex(BegList[i + 1].x, y);
                        Console.Write(BegList[i].x + " " + BegList[i + 1].x);
                    }
                }
                gl.End();
                gl.Flush();
                /*Xóa*/
                int j = 0;
                while (j < BegList.Count)
                {
                    if (BegList[j].y_upper == y)
                    {
                        Console.Write(" Xoa ");
                        BegList.RemoveAt(j);
                    }
                    else j++;
                }
                Console.Write(" sl: " + BegList.Count + " ");
                /*Update*/
                for (int i = 0; i < BegList.Count; i++)
                {
                    AEL t = new AEL();
                    t.slope = BegList[i].slope;
                    t.y_lower = BegList[i].y_lower;
                    t.y_upper = BegList[i].y_upper;
                    t.x = BegList[i].x + BegList[i].slope;
                    BegList[i] = t;
                    Console.Write(" " + BegList[i].slope + " ");
                }

                Console.WriteLine("");
            }
        }
    }

    public class Line : ShapeType
    {
        public override void Draw(OpenGL gl)
        {
            gl.Color(color.R / 255.0, color.G / 255.0, color.B / 255.0);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(p1.X, gl.RenderContextProvider.Height - p1.Y);
            gl.Vertex(p2.X, gl.RenderContextProvider.Height - p2.Y);
            gl.End();
            gl.Flush();
        }
        public override void Create(OpenGL gl)
        {
            Vertex = new List<Point>();
            controlPoints = new List<Point>();
            controlPoints.Add(new Point(p1.X, gl.RenderContextProvider.Height - p1.Y));
            controlPoints.Add(new Point(p2.X, gl.RenderContextProvider.Height - p2.Y));
            Vertex.Add(new Point(p1.X, gl.RenderContextProvider.Height - p1.Y));
            Vertex.Add(new Point(p2.X, gl.RenderContextProvider.Height - p2.Y));
        }

        public override void ScanLine(OpenGL gl)
        {
            gl.Color(colorFilling.R / 255.0, colorFilling.G / 255.0, colorFilling.B / 255.0);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(p1.X, p2.Y);
            gl.Vertex(p2.X, p2.Y);
            gl.End();
            gl.Flush();
        }
    
    }

    public class Circle : ShapeType
    {
        public override void Draw(OpenGL gl)
        {

        }
        public override void Create(OpenGL gl)
        {

        }
        
        
    }

    public class Rectangle : ShapeType
    {
        public override void Draw(OpenGL gl)
        {
         
            gl.Color(color.R / 255.0, color.G / 255.0, color.B / 255.0);
            gl.Begin(OpenGL.GL_LINES);

            /*!!!!!! Chua sap xep cac dinh theo thu tu nguoc chieu kim dong ho*/
            /* xác định các đỉnh của hình chữ nhật */
            //Canh 1
            gl.Vertex(p1.X, gl.RenderContextProvider.Height - p1.Y);
            gl.Vertex(p2.X, gl.RenderContextProvider.Height - p1.Y);
            //Canh 2
            gl.Vertex(p2.X, gl.RenderContextProvider.Height - p1.Y);
            gl.Vertex(p2.X, gl.RenderContextProvider.Height - p2.Y);
            //Canh 3
            gl.Vertex(p2.X, gl.RenderContextProvider.Height - p2.Y);
            gl.Vertex(p1.X, gl.RenderContextProvider.Height - p2.Y);
            //Canh 4
            gl.Vertex(p1.X, gl.RenderContextProvider.Height - p2.Y);
            gl.Vertex(p1.X, gl.RenderContextProvider.Height - p1.Y);
            
            gl.End();
            gl.Flush();
        }
        public override void Create(OpenGL gl)
        {
            Vertex = new List<Point>();
            controlPoints = new List<Point>();
            /*Thêm các control point*/
            controlPoints.Add(new Point(p1.X, gl.RenderContextProvider.Height - p1.Y));
            controlPoints.Add(new Point(p2.X, gl.RenderContextProvider.Height - p1.Y));
            controlPoints.Add(new Point(p2.X, gl.RenderContextProvider.Height - p2.Y));
            controlPoints.Add(new Point(p1.X, gl.RenderContextProvider.Height - p2.Y));
            controlPoints.Add(new Point(p1.X, (gl.RenderContextProvider.Height - p1.Y + gl.RenderContextProvider.Height - p2.Y) / 2));
            controlPoints.Add(new Point(p2.X, (gl.RenderContextProvider.Height - p1.Y + gl.RenderContextProvider.Height - p2.Y) / 2));
            controlPoints.Add(new Point((p1.X + p2.X) / 2, gl.RenderContextProvider.Height - p1.Y));
            controlPoints.Add(new Point((p1.X + p2.X) / 2, gl.RenderContextProvider.Height - p2.Y));
            tam.X =  (p1.X + p2.X) / 2;
            tam.Y = (gl.RenderContextProvider.Height - p1.Y + gl.RenderContextProvider.Height - p2.Y) / 2;
            /*Thêm các đỉnh*/
            Vertex.Add(new Point(p1.X, gl.RenderContextProvider.Height - p1.Y));
            Vertex.Add(new Point(p2.X, gl.RenderContextProvider.Height - p1.Y));
            Vertex.Add(new Point(p2.X, gl.RenderContextProvider.Height - p2.Y));
            Vertex.Add(new Point(p1.X, gl.RenderContextProvider.Height - p2.Y));
            /*Thêm các cạnh*/
            AddEdge(gl);
        }
    }

    public class Ellipse : ShapeType
    {
        public override void ScanLine(OpenGL gl)
        {
            ET = new List<List<AEL>>();
            /*Tạo Edge Table*/
            for (int y = ymin; y <= ymax; y++)
            {
                List<AEL> subList = new List<AEL>();
                ET.Add(subList);
            }

            for (int i = 0; i < Vertex.Count; i++)
            {
                int y = Vertex[i].Y;
                int x = Vertex[i].X;
                
                if (ET[y - ymin].Count == 0)
                {
                    AEL t = new AEL();
                    t.x = x;
                    ET[y - ymin].Add(t);
                    ET[y - ymin].Add(t);
                }
                else
                {
                    AEL t = new AEL();
                    t.x = Math.Min(ET[y - ymin][0].x, x);
                    ET[y - ymin][0] = t;
                    t.x = Math.Max(ET[y - ymin][1].x, x);
                    ET[y - ymin][1] = t;
                }
                
            }
            gl.Color(colorFilling.R / 255.0, colorFilling.G / 255.0, colorFilling.B / 255.0);
            gl.Begin(OpenGL.GL_LINES);
            for (int y = ymin; y <= ymax; y++)
            {
                gl.Vertex(ET[y - ymin][0].x, y);
                gl.Vertex(ET[y - ymin][1].x, y);
            }
            gl.End();
            gl.Flush();
        }

        public override void Draw(OpenGL gl)
        {
            Vertex = new List<Point>();
            gl.Color(color.R / 255.0, color.G / 255.0, color.B / 255.0);
            gl.Begin(OpenGL.GL_POINTS);
            /* Xác định các đại lượng cơ bản của hình ellipse */
            /* Bán kính rx, ry*/
            int rx = Math.Abs(p1.X - p2.X) / 2;
            int ry = Math.Abs(p1.Y - p2.Y) / 2;
            int rx2 = rx * rx;
            int ry2 = ry * ry;
            int twoRx2 = 2 * rx2, twoRy2 = 2 * ry2;
            /* Tâm hình ellipse*/
            int xc = (p1.X + p2.X) / 2;
            int yc = (p1.Y + p2.Y) / 2;
            int x = 0, y = ry;
            int px = 0, py = twoRx2 * y;
            /* Vẽ đối xứng qua tâm*/
            gl.Vertex(xc + x, gl.RenderContextProvider.Height - (yc + y));
            gl.Vertex(xc - x, gl.RenderContextProvider.Height - (yc + y));
            gl.Vertex(xc + x, gl.RenderContextProvider.Height - (yc - y));
            gl.Vertex(xc - x, gl.RenderContextProvider.Height - (yc - y));
            Vertex.Add(new Point(xc + x, gl.RenderContextProvider.Height - (yc + y)));
            Vertex.Add(new Point(xc - x, gl.RenderContextProvider.Height - (yc + y)));
            Vertex.Add(new Point(xc + x, gl.RenderContextProvider.Height - (yc - y)));
            Vertex.Add(new Point(xc - x, gl.RenderContextProvider.Height - (yc - y)));
            /* Vùng 1: dx/dy <= 1*/
            int p = (int)(Math.Round(ry2 - rx2 * ry + 0.25 * rx2));
            while (px < py)
            {
                x++;
                px += twoRy2;
                if (p < 0)
                {
                    p += ry2 + px;
                }
                else
                {
                    y--;
                    py -= twoRx2;
                    p += ry2 + px - py;
                }
                /* Vẽ đối xứng qua tâm*/
                gl.Vertex(xc + x, gl.RenderContextProvider.Height - (yc + y));
                gl.Vertex(xc - x, gl.RenderContextProvider.Height - (yc + y));
                gl.Vertex(xc + x, gl.RenderContextProvider.Height - (yc - y));
                gl.Vertex(xc - x, gl.RenderContextProvider.Height - (yc - y));
                Vertex.Add(new Point(xc + x, gl.RenderContextProvider.Height - (yc + y)));
                Vertex.Add(new Point(xc - x, gl.RenderContextProvider.Height - (yc + y)));
                Vertex.Add(new Point(xc + x, gl.RenderContextProvider.Height - (yc - y)));
                Vertex.Add(new Point(xc - x, gl.RenderContextProvider.Height - (yc - y)));
            }

            /* Vùng 2*/
            p = (int)(Math.Round(ry2 * (x + 0.5) * (x + 0.5) + rx2 * (y - 1) * (y - 1) - rx2 * ry2));
            while (y > 0)
            {
                y--;
                py -= twoRx2;
                if (p > 0)
                {
                    p += rx2 - py;
                }
                else
                {
                    x++;
                    px += twoRy2;
                    p += rx2 - py + px;
                }
                /* Vẽ đối xứng qua tâm*/
                gl.Vertex(xc + x, gl.RenderContextProvider.Height - (yc + y));
                gl.Vertex(xc - x, gl.RenderContextProvider.Height - (yc + y));
                gl.Vertex(xc + x, gl.RenderContextProvider.Height - (yc - y));
                gl.Vertex(xc - x, gl.RenderContextProvider.Height - (yc - y));
                Vertex.Add(new Point(xc + x, gl.RenderContextProvider.Height - (yc + y)));
                Vertex.Add(new Point(xc - x, gl.RenderContextProvider.Height - (yc + y)));
                Vertex.Add(new Point(xc + x, gl.RenderContextProvider.Height - (yc - y)));
                Vertex.Add(new Point(xc - x, gl.RenderContextProvider.Height - (yc - y)));
            }

            gl.End();
            gl.Flush();
        }

        public override void Create(OpenGL gl)
        {
            controlPoints = new List<Point>();
            /**Thêm các điểm control point*/
            controlPoints.Add(new Point(p1.X, gl.RenderContextProvider.Height - p1.Y));
            controlPoints.Add(new Point(p2.X, gl.RenderContextProvider.Height - p1.Y));
            controlPoints.Add(new Point(p2.X, gl.RenderContextProvider.Height - p2.Y));
            controlPoints.Add(new Point(p1.X, gl.RenderContextProvider.Height - p2.Y));
            controlPoints.Add(new Point(p1.X, (gl.RenderContextProvider.Height - p1.Y + gl.RenderContextProvider.Height - p2.Y) / 2));
            controlPoints.Add(new Point(p2.X, (gl.RenderContextProvider.Height - p1.Y + gl.RenderContextProvider.Height - p2.Y) / 2));
            controlPoints.Add(new Point((p1.X + p2.X) / 2, gl.RenderContextProvider.Height - p1.Y));
            controlPoints.Add(new Point((p1.X + p2.X) / 2, gl.RenderContextProvider.Height - p2.Y));
            /*Tìm ymin, ymax*/
            ymin = Vertex[0].Y;
            ymax = ymin;
            for (int i = 0; i < Vertex.Count; i++)
            {
                ymin = Math.Min(ymin, Vertex[i].Y);
                ymax = Math.Max(ymax, Vertex[i].Y);
            }
        }
    }
    
    public class EquiTriangle : ShapeType
    {
        public Point p3;
        public override void Draw(OpenGL gl)
        {
            double a = Math.Sqrt((p1.X - p2.X) ^ 2 + (p1.Y - p2.Y) ^ 2);
            double c = Math.Sin(60 * Math.PI / 180); //sin(pi/3)
            int yy = p1.Y - p2.Y;
            int xx = p1.X - p2.X;

            gl.Color(color.R / 255.0, color.G / 255.0, color.B / 255.0);
            /* xác định các đỉnh của tam giác đều */
            p3.X = p1.X + p2.X;
            p3.X/=2;
            p3.Y = (int)(p1.Y + (xx * c - yy));
            //!! Chua sap xep cac dinh cua tam giac theo chieu nguoc chieu kim dong ho
            gl.Begin(OpenGL.GL_LINES);
            //Canh 1
            // cùng chiều
            gl.Vertex(p1.X, gl.RenderContextProvider.Height - p2.Y); //Đỉnh 1
            // ngược chiều
            gl.Vertex(p2.X, gl.RenderContextProvider.Height - p2.Y); //Đỉnh 2
            //Canh 2
            gl.Vertex(p2.X, gl.RenderContextProvider.Height - p2.Y); //Đỉnh 2
            gl.Vertex(p3.X, gl.RenderContextProvider.Height - p3.Y); //Đỉnh 3
            //Canh 3
            // còn lại
            gl.Vertex(p3.X, gl.RenderContextProvider.Height - p3.Y); //Đỉnh 3
            gl.Vertex(p1.X, gl.RenderContextProvider.Height - p2.Y); //Đỉnh 1

            gl.End();
            gl.Flush();
        }
        public override void Create(OpenGL gl)
        {
            Vertex = new List<Point>();
            controlPoints = new List<Point>();
            //Thêm các điểm control point
            controlPoints.Add(new Point(p1.X, gl.RenderContextProvider.Height - p2.Y));
            controlPoints.Add(new Point(p2.X, gl.RenderContextProvider.Height - p2.Y));
            controlPoints.Add(new Point(p3.X, gl.RenderContextProvider.Height - p3.Y));
            controlPoints.Add(new Point((p1.X + p2.X) / 2, gl.RenderContextProvider.Height - p2.Y));
            controlPoints.Add(new Point(p1.X, gl.RenderContextProvider.Height - p3.Y));
            controlPoints.Add(new Point(p1.X, (2 * gl.RenderContextProvider.Height - p3.Y - p2.Y) / 2));
            controlPoints.Add(new Point(p2.X, gl.RenderContextProvider.Height - p3.Y));
            controlPoints.Add(new Point(p2.X, (2 * gl.RenderContextProvider.Height - p3.Y - p2.Y) / 2));
            /*Them cac dinh*/
            Vertex = new List<Point>();

            Vertex.Add(new Point(p1.X, gl.RenderContextProvider.Height - p2.Y));
            Vertex.Add(new Point(p2.X, gl.RenderContextProvider.Height - p2.Y));
            Vertex.Add(new Point(p3.X, gl.RenderContextProvider.Height - p3.Y));
            /*Them cac canh*/
            AddEdge(gl);
        }
    }

    public class EquiPentagon : ShapeType
    {
        public override void Draw(OpenGL gl)
        {
            Vertex = new List<Point>();
            double[,] pt = new double[5, 2];
            double const_cos = 1 + Math.Cos(72 * Math.PI / 180);
            double const_sin = Math.Sin(72 * Math.PI / 180);
            //get 2 points for 2 first vertices
            pt[0, 0] = p1.X;
            pt[0, 1] = p1.Y;

            pt[1, 0] = p2.X;
            pt[1, 1] = p2.Y;

            for (int i = 2; i < 5; i++)
            {
                double xA = pt[i - 2, 0], yA = pt[i - 2, 1];
                double xB = pt[i - 1, 0], yB = pt[i - 1, 1];
                pt[i, 0] = xA + const_cos * (xB - xA) + const_sin * (yA - yB);
                pt[i, 1] = yA + const_cos * (yB - yA) + const_sin * (xB - xA);
            }

            gl.Color(color.R / 255.0, color.G / 255.0, color.B / 255.0);

            gl.Begin(OpenGL.GL_LINE_LOOP);                                         // Draws pentagon.
            for (int i = 0; i < 5; i++)
            {
                gl.Vertex((int)pt[i, 0], (int)(gl.RenderContextProvider.Height - pt[i, 1]));
                Vertex.Add(new Point((int)pt[i, 0], (int)(gl.RenderContextProvider.Height - pt[i, 1])));
            }
            gl.End();
            gl.Flush();

        }
        public override void Create(OpenGL gl)
        {
            controlPoints = new List<Point>();
            for (int i = 0; i < Vertex.Count; i++)
            {
                controlPoints.Add(Vertex[i]);
            }
            AddEdge(gl);
        }
        
    }

    public class EquiHexagon : ShapeType
    {
        public override void Draw(OpenGL gl)
        {
            Vertex = new List<Point>();
            double[,] pt = new double[6, 2];
            double const_cos = 1 + Math.Cos(60 * Math.PI / 180);
            double const_sin = Math.Sin(60 * Math.PI / 180);
            //get 2 points for 2 first vertices
            pt[0, 0] = p1.X;
            pt[0, 1] = p1.Y;

            pt[1, 0] = p2.X;
            pt[1, 1] = p2.Y;

            for (int i = 2; i < 6; i++)
            {
                double xA = pt[i - 2, 0], yA = pt[i - 2, 1];
                double xB = pt[i - 1, 0], yB = pt[i - 1, 1];
                pt[i, 0] = xA + const_cos * (xB - xA) + const_sin * (yA - yB);
                pt[i, 1] = yA + const_cos * (yB - yA) + const_sin * (xB - xA);
            }

            gl.Color(color.R / 255.0, color.G / 255.0, color.B / 255.0);

            gl.Begin(OpenGL.GL_LINE_LOOP);                                         // Draws pentagon.
            for (int i = 0; i < 6; i++)
            {
                gl.Vertex((int)pt[i, 0], (int)(gl.RenderContextProvider.Height - pt[i, 1]));
                Vertex.Add(new Point((int)pt[i, 0], (int)(gl.RenderContextProvider.Height - pt[i, 1])));
            }
            gl.End();
            gl.Flush();
        }
        public override void Create(OpenGL gl)
        {
            controlPoints = new List<Point>();
            for (int i = 0; i < Vertex.Count; i++)
            {
                controlPoints.Add(Vertex[i]);
            }
            AddEdge(gl);
        }
       
        
    }

    public class Polygon : ShapeType
    {

        public override void Draw(OpenGL gl)
        {
            gl.Color(color.R / 255.0, color.G / 255.0, color.B / 255.0);
            for (int i = 0; i < controlPoints.Count - 1; i++)
            {


                gl.Begin(OpenGL.GL_LINES);
                gl.Vertex(controlPoints[i].X, controlPoints[i].Y);
                gl.Vertex(controlPoints[i + 1].X, controlPoints[i + 1].Y);
                gl.End();
                gl.Flush();
            }
            // Mouse Move
            if (Done)
            {
                gl.Begin(OpenGL.GL_LINES);
                gl.Vertex(controlPoints[0].X, controlPoints[0].Y);
                gl.Vertex(controlPoints.Last().X, controlPoints.Last().Y);
                gl.End();
                gl.Flush();
                return;
            }
            gl.Color(color.R / 255.0, color.G / 255.0, color.B / 255.0);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(p1.X, gl.RenderContextProvider.Height - p1.Y);
            gl.Vertex(p2.X, gl.RenderContextProvider.Height - p2.Y);
            gl.End();
            gl.Flush();
        }
        public override void Create(OpenGL gl)
        {

        }
    }
}
