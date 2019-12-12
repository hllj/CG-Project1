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
            ColorFilling cl = new ColorFilling();
            cl.init(gl, gl.RenderContextProvider.Width, gl.RenderContextProvider.Height);
            ////gl.Color(color.R / 255.0, color.G / 255.0, color.B / 255.0);
            //gl.Color(255.0, 125.0, 0);
            byte[] Ptr = { 255, 72, 0};
            //short[] s = { (short)p1.X, (short)p1.Y };
            //gl.Begin(OpenGL.GL_LINES);
            //gl.Vertex(p1.X, gl.RenderContextProvider.Height - p1.Y);
            //gl.Vertex(p1.X, gl.RenderContextProvider.Height - p1.Y+1);
            //gl.End();
            //gl.Flush();
            gl.RasterPos(p1.X, gl.RenderContextProvider.Height - p1.Y);
            gl.DrawPixels(1, 1, OpenGL.GL_RGB, Ptr);

            RGBColor A = cl.GetPixel(p2.X, p2.Y - 1);
            RGBColor D = cl.GetPixel(p2.X - 1, p2.Y - 1);
            RGBColor B = cl.GetPixel(p2.X + 1, p2.Y);
            RGBColor C = cl.GetPixel(p1.X, p1.Y);
            RGBColor C1 = cl.GetPixel(p1.X + 1, p1.Y);
            RGBColor C2 = cl.GetPixel(p1.X - 1, p1.Y);
            RGBColor C3 = cl.GetPixel(p1.X, p1.Y + 1);
            RGBColor C4 = cl.GetPixel(p1.X, p1.Y - 1);
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
            

            /* xác định các đỉnh của hình chữ nhật tho thứ tự ngược chiều kim đồng hồ*/
            //Canh 1

            gl.Vertex(p1.X, gl.RenderContextProvider.Height - p1.Y); // Đỉnh 1 (x1,y1)
            gl.Vertex(p1.X, gl.RenderContextProvider.Height - p2.Y); //Đỉnh 2 (x1,y2)
            //Canh 2
            gl.Vertex(p1.X, gl.RenderContextProvider.Height - p2.Y); //Đỉnh 2
            gl.Vertex(p2.X, gl.RenderContextProvider.Height - p2.Y); //Đỉnh 3 (x2,y2)
            //Canh 3
            gl.Vertex(p2.X, gl.RenderContextProvider.Height - p2.Y); //Đỉnh 3
            gl.Vertex(p2.X, gl.RenderContextProvider.Height - p1.Y); //Đỉnh 4 (x2,y1)
            //Canh 4
            gl.Vertex(p2.X, gl.RenderContextProvider.Height - p1.Y); //Đỉnh 4
            gl.Vertex(p1.X, gl.RenderContextProvider.Height - p1.Y); //đỉnh 1

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
    }
  
}
