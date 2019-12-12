using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SharpGL;

namespace OpenGL_App1
{
    struct RGBColor
    {
        public byte r;
        public byte g;
        public byte b;
    }
    class ColorFilling
    {
        public OpenGL gl { set; get; }
        public void init(OpenGL GL)
        {
            gl = GL;
        }

        public bool isSameColor(RGBColor des, RGBColor src)
        {
            /*
            This function to check color des is the same as color src
            -----------------------------------
            Parameters:
            des,src: color to compare
            */
            des.r = (byte)(des.r >> 1);
            des.g = (byte)(des.g >> 1);
            des.b = (byte)(des.b >> 1);
            src.r = (byte)(src.r >> 1);
            src.g = (byte)(src.g >> 1);
            src.b = (byte)(src.b >> 1);
            return (des.r == src.r) && (des.g == src.g) && (des.b == src.b);
        }

        public RGBColor GetPixel(int x, int y)
        {
            /*
            Get Color of pixels
            --------------------------
            Parameters:
            x,y: Coordinate
            Return values:
            RGB color of pixel
            */
           //Console.WriteLine(x + " " + y);
            byte[] ptr = new byte[3];
            RGBColor color;
            gl.ReadPixels(x, gl.RenderContextProvider.Height - y, 1, 1, format: OpenGL.GL_RGB, type: OpenGL.GL_BYTE, ptr);
            color.r = (byte)((ptr[0]) << 1);
            color.g = (byte)((ptr[1]) << 1);
            color.b = (byte)((ptr[2]) << 1);
            return color;
        }

        public void PutPixel(int x, int y, RGBColor color)
        {
            /*
            Put pixel with RGB corlor at coordinate (x,y)
            --------------------------------------------
            Parameters:
            x, y: coordinate
            color: RGB color
            */
            byte[] ptr = { color.r, color.g, color.b };
            gl.RasterPos(x, gl.RenderContextProvider.Height - y);
            gl.DrawPixels(1, 1, OpenGL.GL_RGB, ptr);

        }

        public void BoudaryFill(int x, int y, RGBColor F_Color, RGBColor B_Color)
        {
            /*
            BoudaryFill Algorithm: Fill all pixel that are not is F_color or B_color
            ---------------------
            Parameters:
            x,y: Coordinate
            F_Color: Color to fill
            B_Color: Border Color of Shape

            */

            RGBColor curColor = GetPixel(x, y);
            if ((!isSameColor(curColor, F_Color)) && (!isSameColor(curColor, B_Color)))
            {
                PutPixel(x, y, F_Color);
                BoudaryFill(x + 1, y, F_Color, B_Color);
                BoudaryFill(x - 1, y, F_Color, B_Color);
                BoudaryFill(x, y - 1, F_Color, B_Color);
                BoudaryFill(x, y + 1, F_Color, B_Color);
            }

        }
    }
}