using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    class Filters
    {
        public static Bitmap GrayscaleFilter(Bitmap original)
        {
            Bitmap result = new Bitmap(original.Width, original.Height);
            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    Color pixelColor = original.GetPixel(x, y);
                    int grayValue = (int)(pixelColor.R * 0.299 + pixelColor.G * 0.587 + pixelColor.B * 0.114);
                    Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue);
                    result.SetPixel(x, y, grayColor);
                }
            }
            return result;
        }
        public static Bitmap NegativeFilter(Bitmap original)
        {
            Bitmap result = new Bitmap(original.Width, original.Height);
            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    Color pixelColor = original.GetPixel(x, y);
                    Color negativeColor = Color.FromArgb(255 - pixelColor.R, 255 - pixelColor.G, 255 - pixelColor.B);
                    result.SetPixel(x, y, negativeColor);
                }
            }
            return result;
        }
        public static Bitmap ThresholdFilter(Bitmap original, int threshold = 128)
        {
            Bitmap result = new Bitmap(original.Width, original.Height);
            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    Color pixelColor = original.GetPixel(x, y);
                    int grayValue = (int)(pixelColor.R * 0.299 + pixelColor.G * 0.587 + pixelColor.B * 0.114);
                    Color thresholdColor = grayValue < threshold ? Color.Black : Color.White;
                    result.SetPixel(x, y, thresholdColor);
                }
            }
            return result;
        }
        public static Bitmap EdgeDetectionFilter(Bitmap original)
        {
            Bitmap gray = GrayscaleFilter(original);
            Bitmap result = new Bitmap(original.Width, original.Height);

            int[,] gx = new int[,]
            {
                { 1, 0, -1 },
                { 2, 0, -2 },
                { 1, 0, -1 }
            };

            int[,] gy = new int[,]
            {
                { 1, 2, 1 },
                { 0, 0, 0 },
                { -1, -2, -1 }
            };
            for (int y = 1; y < gray.Height - 1; y++)
            {
                for (int x = 1; x < gray.Width - 1; x++)
                {
                    int pixelX = 0;
                    int pixelY = 0;
                    for (int j = -1; j <= 1; j++)
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            int grayValue = gray.GetPixel(x + i ,y + j).R;
                            pixelX += gx[j + 1, i + 1] * grayValue;
                            pixelY += gy[j + 1, i + 1] * grayValue;
                        }
                    }
                    int edge = Math.Min(255,(int)Math.Sqrt(pixelX * pixelX + pixelY * pixelY));
                    Color edgeColor = Color.FromArgb(edge, edge, edge);
                    result.SetPixel(x, y, edgeColor);
                }
            }
            return result;
        }
    }
}
