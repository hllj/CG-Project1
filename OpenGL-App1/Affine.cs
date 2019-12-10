using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;


namespace OpenGL_App1
{
    public class Affine
    {
        public int id { get; set; }
        public float[,] Matrix;
        public void Transform(int dx,int dy) {
            Matrix = new float[3,3] 
            {
                {1,0,dx},
                {0,1,dy},
                {0,0,1}
            };
        }
        public Point Translate(Point p)
        {
           
            p.X =(int) Matrix[0, 0] * p.X + (int)Matrix[0, 1] * p.Y + (int)Matrix[0, 2];
            p.Y = (int)Matrix[1, 0] * p.X + (int)Matrix[1, 1] * p.Y + (int)Matrix[1, 2];
            return new Point(p.X, p.Y);
        }
        //abstract public void Draw(OpenGL gl);
        public Affine()
        {
            Matrix = new float[3, 3]
            {
                {1,0,0 },
                {0,1,0 },
                {0,0,1 }
            };
        }

    }
}
