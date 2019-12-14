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
        public void Translate(int dx, int dy)
        {
            float[,] mat = new float[3, 3]
            {
                {1,0,dx},
                {0,1,dy},
                {0,0,1}
            };
            Multiply(mat);
        }
        public void Scale(float Sx, float Sy)
        {
            float[,] mat = new float[3, 3]
            {
                {Sx,0,0},
                {0,Sy,0},
                {0,0,1}
            };
            Multiply(mat);
        }
        public Point Transform(Point p)
        {

            p.X = (int)(Matrix[0, 0] * p.X + Matrix[0, 1] * p.Y + Matrix[0, 2]);
            p.Y = (int)(Matrix[1, 0] * p.X + Matrix[1, 1] * p.Y + Matrix[1, 2]);
            return new Point(p.X, p.Y);
        }

        private void Multiply(float[,] mat)
        {
            float[,] result = new float[3, 3];
            for (int row = 0; row < 3; row++)
                for (int col = 0; col < 3; col++)
                {
                    float sum = 0;
                    for (int i = 0; i < 3; i++)
                        sum += Matrix[row, i] * mat[i, col];
                    result[row, col] = sum;
                }
            Matrix = result;
        }

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