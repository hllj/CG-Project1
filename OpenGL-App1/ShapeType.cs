using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;


namespace OpenGL_App1
{
    abstract public class ShapeType
    {
        public int id { get; set; }
        public bool Done;
        public Point p1 { get; set; } // starting point
        public Point p2 { get; set; } // ending point
                                      // public List<Point> lPoint;
        public Color color { get; set; }
        public List<Point> controlPoints;
        abstract public void Draw(OpenGL gl);
        abstract public ShapeType Clone();
        abstract public void Transform(Affine at);
        abstract public void DrawControlPoints(OpenGL gl);
        abstract public void Create(OpenGL gl);
        public ShapeType()
        {
            Done = false;
            controlPoints = new List<Point>();

        }
        public void Update(OpenGL gl)
        {
            controlPoints = new List<Point>();
            Create(gl);
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
            controlPoints = new List<Point>();
            controlPoints.Add(new Point(p1.X, gl.RenderContextProvider.Height - p1.Y));
            controlPoints.Add(new Point(p2.X, gl.RenderContextProvider.Height - p2.Y));
        }

        public override void DrawControlPoints(OpenGL gl)
        {
            for (int i = 0; i < controlPoints.Count; i++)
            {
                gl.Color(color.R / 255.0, 0, 0);
                gl.PointSize(5);
                gl.Begin(OpenGL.GL_POINTS);
                Point a = controlPoints[i];
                gl.Vertex(a.X, a.Y);
                gl.End();
                gl.Flush();
            }
            gl.PointSize(1);

        }
        public override ShapeType Clone()
        {
            ShapeType t = new Line();            
            for (int i =0;i<controlPoints.Count;i++)
            {
                t.controlPoints.Add(controlPoints[i]);
            }
            t.color = color;
            t.p1 = new Point(p1.X, p1.Y);
            t.p2 = new Point(p2.X, p2.Y);
            return t;
        }
        public override void Transform(Affine at)
        {
            for (int i =0;i<controlPoints.Count;i++)
            {
                controlPoints[i] = at.Translate(controlPoints[i]);
            }
            p1 = at.Translate(p1);
            p2 = at.Translate(p2);
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
        public override ShapeType Clone()
        {
            throw new NotImplementedException();
        }
        public override void Transform(Affine at) { }


        public override void DrawControlPoints(OpenGL gl)

        {
            throw new NotImplementedException();
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
            controlPoints.Add(new Point(p1.X, gl.RenderContextProvider.Height - p1.Y));
            controlPoints.Add(new Point(p2.X, gl.RenderContextProvider.Height - p1.Y));
            controlPoints.Add(new Point(p2.X, gl.RenderContextProvider.Height - p2.Y));
            controlPoints.Add(new Point(p1.X, gl.RenderContextProvider.Height - p2.Y));
            controlPoints.Add(new Point(p1.X, (gl.RenderContextProvider.Height - p1.Y + gl.RenderContextProvider.Height - p2.Y) / 2));
            controlPoints.Add(new Point(p2.X, (gl.RenderContextProvider.Height - p1.Y + gl.RenderContextProvider.Height - p2.Y) / 2));
            controlPoints.Add(new Point((p1.X + p2.X) / 2, gl.RenderContextProvider.Height - p1.Y));
            controlPoints.Add(new Point((p1.X + p2.X) / 2, gl.RenderContextProvider.Height - p2.Y));
        }
        public override void DrawControlPoints(OpenGL gl)
        {
            for (int i = 0; i < controlPoints.Count; i++)
            {
                gl.Color(color.R / 255.0, 0, 0);
                gl.PointSize(5);
                gl.Begin(OpenGL.GL_POINTS);
                Point a = controlPoints[i];
                gl.Vertex(a.X, a.Y);
                gl.End();
                gl.Flush();
            }
            gl.PointSize(1);
        }
        public override ShapeType Clone()
        {
            ShapeType t = new Rectangle();
            for (int i = 0; i < controlPoints.Count; i++)
            {
                t.controlPoints.Add(controlPoints[i]);
            }
            t.color = color;
            t.p1 = new Point(p1.X, p1.Y);
            t.p2 = new Point(p2.X, p2.Y);
            return t;
        }
        public override void Transform(Affine at)
        {
            for (int i = 0; i < controlPoints.Count; i++)
            {
                controlPoints[i] = at.Translate(controlPoints[i]);
            }
            p1 = at.Translate(p1);
            p2 = at.Translate(p2);
        }
    }

    public class Ellipse : ShapeType
    {
        public override void Draw(OpenGL gl)
        {
            gl.Color(color.R / 255.0, color.G / 255.0, color.B / 255.0);
            gl.Begin(OpenGL.GL_POINTS);
            /* Xác định các đại lượng cơ bản của hình ellipse */
            /* Bán kính rx, ry*/
            long rx = Math.Abs(p1.X - p2.X) / 2;
            long ry = Math.Abs(p1.Y - p2.Y) / 2;
            long rx2 = rx * rx;
            long ry2 = ry * ry;
            long twoRx2 = 2 * rx2, twoRy2 = 2 * ry2;
            /* Tâm hình ellipse*/
            long xc = (p1.X + p2.X) / 2;
            long yc = (p1.Y + p2.Y) / 2;
            long x = 0, y = ry;
            long px = 0, py = twoRx2 * y;
            /* Vẽ đối xứng qua tâm*/
            gl.Vertex(xc + x, gl.RenderContextProvider.Height - (yc + y));
            gl.Vertex(xc - x, gl.RenderContextProvider.Height - (yc + y));
            gl.Vertex(xc + x, gl.RenderContextProvider.Height - (yc - y));
            gl.Vertex(xc - x, gl.RenderContextProvider.Height - (yc - y));
            /* Vùng 1: dx/dy <= 1*/
            long p = (long)(Math.Round(ry2 - rx2 * ry + 0.25 * rx2));
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
            }

            /* Vùng 2*/
            p = (long)(Math.Round(ry2 * (x + 0.5) * (x + 0.5) + rx2 * (y - 1) * (y - 1) - rx2 * ry2));
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
            }

            gl.End();
            gl.Flush();            
        }
        public override void Create(OpenGL gl)
        {
            controlPoints.Add(new Point(p1.X, gl.RenderContextProvider.Height - p1.Y));
            controlPoints.Add(new Point(p2.X, gl.RenderContextProvider.Height - p1.Y));
            controlPoints.Add(new Point(p2.X, gl.RenderContextProvider.Height - p2.Y));
            controlPoints.Add(new Point(p1.X, gl.RenderContextProvider.Height - p2.Y));
            controlPoints.Add(new Point(p1.X, (gl.RenderContextProvider.Height - p1.Y + gl.RenderContextProvider.Height - p2.Y) / 2));
            controlPoints.Add(new Point(p2.X, (gl.RenderContextProvider.Height - p1.Y + gl.RenderContextProvider.Height - p2.Y) / 2));
            controlPoints.Add(new Point((p1.X + p2.X) / 2, gl.RenderContextProvider.Height - p1.Y));
            controlPoints.Add(new Point((p1.X + p2.X) / 2, gl.RenderContextProvider.Height - p2.Y));
        }
        public override ShapeType Clone()
        {
            ShapeType t = new Ellipse();
            for (int i = 0; i < controlPoints.Count; i++)
            {
                t.controlPoints.Add(controlPoints[i]);
            }
            t.color = color;
            t.p1 = new Point(p1.X, p1.Y);
            t.p2 = new Point(p2.X, p2.Y);
            return t;
        }
        public override void Transform(Affine at)
        {
            for (int i = 0; i < controlPoints.Count; i++)
            {
                controlPoints[i] = at.Translate(controlPoints[i]);
            }
            p1 = at.Translate(p1);
            p2 = at.Translate(p2);
        }

        public override void DrawControlPoints(OpenGL gl)
        {
            for (int i = 0; i < controlPoints.Count; i++)
            {
                gl.Color(color.R / 255.0, 0, 0);
                gl.PointSize(5);
                gl.Begin(OpenGL.GL_POINTS);
                Point a = controlPoints[i];
                gl.Vertex(a.X, a.Y);
                gl.End();
                gl.Flush();
            }
            gl.PointSize(1);

        }
    }

    public class EquiTriangle : ShapeType
    {
        private Point p3;
        public override void Draw(OpenGL gl)
        {

            double a = Math.Sqrt((p1.X - p2.X) ^ 2 + (p1.Y - p2.Y) ^ 2);
            double c = Math.Sin(60 * Math.PI / 180); //sin(pi/3)
            int yy = p1.Y - p2.Y;
            int xx = p1.X - p2.X;

            gl.Color(color.R / 255.0, color.G / 255.0, color.B / 255.0);
            /* xác định các đỉnh của tam giác đều */
            p3.X = p1.X + p2.X;
            p3.X /= 2;
            p3.Y = (int)(p1.Y + (xx * c - yy));
            //!! Chua sap xep cac dinh cua tam giac theo chieu nguoc chieu kim dong ho
            gl.Begin(OpenGL.GL_LINES);
            //Canh 1
            gl.Vertex(p1.X, gl.RenderContextProvider.Height - p2.Y); //Đỉnh 1
            gl.Vertex(p2.X, gl.RenderContextProvider.Height - p2.Y); //Đỉnh 2
            //Canh 2
            gl.Vertex(p2.X, gl.RenderContextProvider.Height - p2.Y); //Đỉnh 2
            gl.Vertex(p3.X, gl.RenderContextProvider.Height - p3.Y); //Đỉnh 3
            //Canh 3
            gl.Vertex(p3.X, gl.RenderContextProvider.Height - p3.Y); //Đỉnh 3
            gl.Vertex(p1.X, gl.RenderContextProvider.Height - p2.Y); //Đỉnh 1

            gl.End();
            gl.Flush();
        }
        public override void Create(OpenGL gl)
        {

        }
        public override ShapeType Clone()
        {
            throw new NotImplementedException();
        }
        public override void Transform(Affine at)
        {
            throw new NotImplementedException();
        }

        public override void DrawControlPoints(OpenGL gl)
        {
            throw new NotImplementedException();
        }
    }

    public class EquiPentagon : ShapeType
    {
        public override void Draw(OpenGL gl)
        {
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
                gl.Vertex(pt[i, 0], gl.RenderContextProvider.Height - pt[i, 1]);
            gl.End();
            gl.Flush();

        }
        public override void Create(OpenGL gl)
        {

        }
        public override ShapeType Clone()
        {
            throw new NotImplementedException();
        }
        public override void Transform(Affine at)
        {
            throw new NotImplementedException();
        }

        public override void DrawControlPoints(OpenGL gl)
        {
            throw new NotImplementedException();
        }
    }

    public class EquiHexagon : ShapeType
    {
        public override void Draw(OpenGL gl)
        {

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
                gl.Vertex(pt[i, 0], gl.RenderContextProvider.Height - pt[i, 1]);
            gl.End();
            gl.Flush();
        }
        public override void Create(OpenGL gl)
        {

        }
        public override ShapeType Clone()
        {
            throw new NotImplementedException();
        }
        public override void Transform(Affine at)
        {
        }

        public override void DrawControlPoints(OpenGL gl)

        {
            throw new NotImplementedException();
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
        public override ShapeType Clone()
        {
            ShapeType t = new Polygon();
            for (int i = 0; i < controlPoints.Count; i++)
            {
                t.controlPoints.Add(controlPoints[i]);
            }
            t.Done = true;
            t.color = color;
            t.p1 = new Point(p1.X, p1.Y);
            t.p2 = new Point(p2.X, p2.Y);
            return t;
        }
        public override void Transform(Affine at)
        {
            for (int i = 0; i < controlPoints.Count; i++)
            {
                controlPoints[i] = at.Translate(controlPoints[i]);
            }
            p1 = at.Translate(p1);
            p2 = at.Translate(p2);
        }
        public override void DrawControlPoints(OpenGL gl)

        {
            for (int i = 0; i < controlPoints.Count; i++)
            {
                gl.Color(color.R / 255.0, 0, 0);
                gl.PointSize(5);
                gl.Begin(OpenGL.GL_POINTS);
                Point a = controlPoints[i];
                gl.Vertex(a.X, a.Y);
                gl.End();
                gl.Flush();
            }
            gl.PointSize(1);
        }

    }

}
