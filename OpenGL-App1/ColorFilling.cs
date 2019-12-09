﻿using System;
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
        public int width { get; set; }
        public int height { get; set; }

        public void init(OpenGL GL, int Width, int Height)
        {
            gl = GL;
            width = Width;
            height = Height;
        }

        public bool isSameColor(RGBColor des, RGBColor src)
        {
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

            byte[] ptr = new byte[4];
            RGBColor color;
            gl.ReadPixels(x, gl.RenderContextProvider.Height - y, 1, 1, format: OpenGL.GL_RGB, type: OpenGL.GL_BYTE, ptr);
            color.r = (byte)((ptr[0]) << 1);
            color.g = (byte)((ptr[1]) << 1);
            color.b = (byte)((ptr[2]) << 1);
            return color;
            
        }

        public void PutPixel(int x, int y, RGBColor color)
        {
            byte[] ptr = { color.r, color.g, color.b};
          //  gl.Color(color.r,color.g,color.b);
            gl.Color(ptr);
            //gl.Begin(OpenGL.GL_POINTS);
            for (int i = 0; i < 100; i++)
            {
                /* xác định các đỉnh của hình chữ nhật tho thứ tự ngược chiều kim đồng hồ*/
                //Canh 1

                gl.Vertex(100 + i, gl.RenderContextProvider.Height - 50 - i); // Đỉnh 1 (x1,y1)
                //gl.Vertex(100, gl.RenderContextProvider.Height - 100); //Đỉnh 2 (x1,y2)
                //                                                       //Canh 2
                //gl.Vertex(100, gl.RenderContextProvider.Height - 100); //Đỉnh 2
                //gl.Vertex(500, gl.RenderContextProvider.Height - 100); //Đỉnh 3 (x2,y2)
                //                                                       //Canh 3
                //gl.Vertex(500, gl.RenderContextProvider.Height - 100); //Đỉnh 3
                //gl.Vertex(500, gl.RenderContextProvider.Height - 50); //Đỉnh 4 (x2,y1)
                //                                                      //Canh 4
                //gl.Vertex(500, gl.RenderContextProvider.Height - 50); //Đỉnh 4
                //gl.Vertex(100, gl.RenderContextProvider.Height - 50); //đỉnh 1

                //gl.End();
                //  gl.RasterPos(x, gl.RenderContextProvider.Height - y);
                //gl.DrawPixels(1, 1, OpenGL.GL_BYTE, ptr);
            }
                gl.Flush();
           
       }

        public void BoudaryFill(int x, int y, RGBColor F_Color, RGBColor B_Color)
        {
            RGBColor curColor = GetPixel(x, y);
            if ((!isSameColor(curColor, F_Color)) && (!isSameColor(curColor, B_Color)))
            {
           //     PutPixel(x, y, F_Color);
                BoudaryFill(x + 1, y, F_Color, B_Color);
                BoudaryFill(x - 2, y, F_Color, B_Color);
                BoudaryFill(x, y - 1, F_Color, B_Color);
                BoudaryFill(x, y + 1, F_Color, B_Color);
            }

        }
        public void BoudaryFillLeft(int x, int y, RGBColor F_Color, RGBColor B_Color)
        {
            RGBColor curColor = GetPixel(x, y);
            if ((!isSameColor(curColor, F_Color)) && (!isSameColor(curColor, B_Color)))
            {
                PutPixel(x, y, F_Color);
                BoudaryFillLeft(x - 1, y, F_Color, B_Color);
                //BoudaryFillRight(x + 1, y, F_Color, B_Color);
                BoudaryFillBottom(x, y - 1, F_Color, B_Color);
                BoudaryFillTop(x, y + 1, F_Color, B_Color);
            }

        }
        public void BoudaryFillRight(int x, int y, RGBColor F_Color, RGBColor B_Color)
        {
            RGBColor curColor = GetPixel(x, y);
            if ((!isSameColor(curColor, F_Color)) && (!isSameColor(curColor, B_Color)))
            {
                PutPixel(x, y, F_Color);
                //BoudaryFillLeft(x - 1, y, F_Color, B_Color);
                BoudaryFillRight(x + 1, y, F_Color, B_Color);
                BoudaryFillBottom(x, y - 1, F_Color, B_Color);
                BoudaryFillTop(x, y + 1, F_Color, B_Color);
            }

        }
        public void BoudaryFillBottom(int x, int y, RGBColor F_Color, RGBColor B_Color)
        {
            RGBColor curColor = GetPixel(x, y);
            if ((!isSameColor(curColor, F_Color)) && (!isSameColor(curColor, B_Color)))
            {
                PutPixel(x, y, F_Color);
                //BoudaryFillLeft(x - 1, y, F_Color, B_Color);
                //BoudaryFillRight(x + 1, y, F_Color, B_Color);
                BoudaryFillBottom(x, y - 1, F_Color, B_Color);
                //BoudaryFillTop(x, y + 1, F_Color, B_Color);
            }
        }
        public void BoudaryFillTop(int x, int y, RGBColor F_Color, RGBColor B_Color)
        {
            RGBColor curColor = GetPixel(x, y);
            if ((!isSameColor(curColor, F_Color)) && (!isSameColor(curColor, B_Color)))
            {
                PutPixel(x, y, F_Color);
                //BoudaryFillLeft(x - 1, y, F_Color, B_Color);
                //BoudaryFillRight(x + 1, y, F_Color, B_Color);
                //BoudaryFillBottom(x, y - 1, F_Color, B_Color);
                BoudaryFillTop(x, y + 1, F_Color, B_Color);
            }

        }
    }
}
