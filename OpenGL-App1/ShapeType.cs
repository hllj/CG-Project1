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
        public List<Point> Control_points;  // list control point
        public Point p1 { get; set; } // starting point
        public Point p2 { get; set; } // ending point
       // public List<Point> lPoint;
        public Color color { get; set; }
        abstract public void Draw(OpenGL gl);
        abstract public ShapeType Clone();
        abstract public void Transform(Affine at);
        public ShapeType()
        {
            Done = false;
            Control_points = new List<Point>();
        }
    }

    public class Line : ShapeType
    {
        public override void Draw(OpenGL gl)
        {
            if (Control_points.Count<2)
            {
                Control_points.Add(p1);
                Control_points.Add(p2);
            }
            Control_points[0] = new Point(p1.X, p1.Y);
            Control_points[1] = new Point(p2.X, p2.Y);
            gl.Color(color.R / 255.0, color.G / 255.0, color.B / 255.0);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(p1.X, gl.RenderContextProvider.Height - p1.Y);
            gl.Vertex(p2.X, gl.RenderContextProvider.Height - p2.Y);
            gl.End();
            gl.Flush();
        }
        public override ShapeType Clone()
        {
            ShapeType t = new Line();
            t.Control_points = new List<Point>(Control_points);
            t.color = color;
            t.p1 = new Point(p1.X, p1.Y);
            t.p2 = new Point(p2.X,p2.Y);
            return t;
        }
        public override void Transform(Affine at)
        {
            p1 = at.Transform(p1);
            p2 = at.Transform(p2);
        }
    }

    public class Circle : ShapeType
    {
        public override void Draw(OpenGL gl)
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
        public override ShapeType Clone()
        {
            throw new NotImplementedException();
        }
        public override void Transform(Affine at)
        {
            throw new NotImplementedException();
        }
    }

    public class Ellipse : ShapeType
    {
        public override void Draw(OpenGL gl)
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
            p3.X/=2;
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
        public override ShapeType Clone()
        {
            throw new NotImplementedException();
        }
        public override void Transform(Affine at)
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
        public override ShapeType Clone()
        {
            throw new NotImplementedException();
        }
        public override void Transform(Affine at)
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
        public override ShapeType Clone()
        {
            throw new NotImplementedException();
        }
        public override void Transform(Affine at)
        {
            throw new NotImplementedException();
        }
    }
    public class Polygon : ShapeType
    {

        public override void Draw(OpenGL gl)
        {

            for (int i = 0; i < Control_points.Count - 1; i++)
            {
                // Vẽ những cạnh trước đó 
                gl.Color(color.R / 255.0, color.G / 255.0, color.B / 255.0);
                gl.Begin(OpenGL.GL_LINES);
                gl.Vertex(Control_points[i].X, gl.RenderContextProvider.Height - Control_points[i].Y);
                gl.Vertex(Control_points[i + 1].X, gl.RenderContextProvider.Height - Control_points[i + 1].Y);
                gl.End();
                gl.Flush();
            }
            // Mouse Move
            gl.Color(color.R / 255.0, color.G / 255.0, color.B / 255.0);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(p1.X, gl.RenderContextProvider.Height - p1.Y);
            gl.Vertex(p2.X, gl.RenderContextProvider.Height - p2.Y);
            gl.End();
            gl.Flush();
        }
        public override ShapeType Clone()
        {
            throw new NotImplementedException();
        }
        public override void Transform(Affine at)
        {
            throw new NotImplementedException();
        }

    }

}
