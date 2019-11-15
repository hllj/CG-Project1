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
            gl.Begin(OpenGL.GL_POLYGON);
            /* xác định các đỉnh của hình chữ nhật */
            gl.Vertex(p1.X, gl.RenderContextProvider.Height - p1.Y);
            gl.Vertex(p2.X, gl.RenderContextProvider.Height - p1.Y);
            gl.Vertex(p2.X, gl.RenderContextProvider.Height - p2.Y);
            gl.Vertex(p1.X, gl.RenderContextProvider.Height - p2.Y);
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
        public override void Draw(OpenGL gl)
        {
            
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
