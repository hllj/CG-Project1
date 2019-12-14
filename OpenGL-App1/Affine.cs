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
        public double[,] Matrix;
        public void Translate(int dx, int dy)
        {
            double[,] mat = new double[3, 3]
            {
                {1,0,dx},
                {0,1,dy},
                {0,0,1}
            };
            Multiply(mat);
        }
        public void Scale(double Sx, double Sy)
        {
            double[,] mat = new double[3, 3]
            {
                {Sx,0,0},
                {0,Sy,0},
                {0,0,1}
            };
            Multiply(mat);
        }
        public void Rotate(double alpha)
        {
            double[,] mat = new double[3, 3]
            {
                {Math.Cos(alpha), -Math.Sin(alpha), 0},
                {Math.Sin(alpha), Math.Cos(alpha), 0},
                {0, 0, 1}
            };
            Multiply(mat);
        }
        public Point Transform(Point p)
        {

            p.X = (int)(Matrix[0, 0] * p.X + Matrix[0, 1] * p.Y + Matrix[0, 2]);
            p.Y = (int)(Matrix[1, 0] * p.X + Matrix[1, 1] * p.Y + Matrix[1, 2]);
            return new Point(p.X, p.Y);
        }

        private void Multiply(double[,] mat)
        {
            double[,] result = new double[3, 3];
            for (int row = 0; row < 3; row++)
                for (int col = 0; col < 3; col++)
                {
                    double sum = 0;
                    for (int i = 0; i < 3; i++)
                        sum += Matrix[row, i] * mat[i, col];
                    result[row, col] = sum;
                }
            Matrix = result;
        }

        public Affine()
        {
            Matrix = new double[3, 3]
            {
                {1,0,0 },
                {0,1,0 },
                {0,0,1 }
            };
        }

    }
}