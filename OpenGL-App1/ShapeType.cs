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
        public Point p1 { get; set; } // starting point
        public Point p2 { get; set; } // ending point
       // public List<Point> lPoint;
        public Color color { get; set; }
        abstract public void Draw(OpenGL gl);
        public ShapeType()
        {
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
    }

    public class Circle : ShapeType
    {
        public override void Draw(OpenGL gl)
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
    }

    public class Ellipse : ShapeType
    {
        public override void Draw(OpenGL gl)
        {

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
    }

    public class EquiPentagon : ShapeType
    {
        public override void Draw(OpenGL gl)
        {
            
        }
    }

    public class EquiHexagon : ShapeType
    {
        public override void Draw(OpenGL gl)
        {

        }
    }
  
}
