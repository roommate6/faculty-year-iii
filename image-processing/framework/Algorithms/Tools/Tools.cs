using Emgu.CV;
using Emgu.CV.Structure;
using System.Windows;
using System.Drawing;
using Algorithms.Utilities;

namespace Algorithms.Tools
{
    public class Tools
    {
        #region Copy
        public static Image<Gray, byte> Copy(Image<Gray, byte> inputImage)
        {
            Image<Gray, byte> result = inputImage.Clone();
            return result;
        }

        public static Image<Bgr, byte> Copy(Image<Bgr, byte> inputImage)
        {
            Image<Bgr, byte> result = inputImage.Clone();
            return result;
        }
        #endregion

        #region Invert
        public static Image<Gray, byte> Invert(Image<Gray, byte> inputImage)
        {
            Image<Gray, byte> result = new Image<Gray, byte>(inputImage.Size);
            for (int y = 0; y < inputImage.Height; y++)
            {
                for (int x = 0; x < inputImage.Width; x++)
                {
                    result.Data[y, x, 0] = (byte)(255 - inputImage.Data[y, x, 0]);
                }
            }
            return result;
        }

        public static Image<Bgr, byte> Invert(Image<Bgr, byte> inputImage)
        {
            Image<Bgr, byte> result = new Image<Bgr, byte>(inputImage.Size);
            for (int y = 0; y < inputImage.Height; y++)
            {
                for (int x = 0; x < inputImage.Width; x++)
                {
                    result.Data[y, x, 0] = (byte)(255 - inputImage.Data[y, x, 0]);
                    result.Data[y, x, 1] = (byte)(255 - inputImage.Data[y, x, 1]);
                    result.Data[y, x, 2] = (byte)(255 - inputImage.Data[y, x, 2]);
                }
            }
            return result;
        }
        #endregion

        #region Convert color image to grayscale image
        public static Image<Gray, byte> Convert(Image<Bgr, byte> inputImage)
        {
            Image<Gray, byte> result = inputImage.Convert<Gray, byte>();
            return result;
        }
        #endregion

        #region Thresholding

        public static Image<Gray, byte> Thresholding(Image<Gray, byte> inputImage, byte threshold)
        {
            Image<Gray, byte> result = new Image<Gray, byte>(inputImage.Size);

            for (int y = 0; y < inputImage.Height; y++)
            {
                for (int x = 0; x < inputImage.Width; x++)
                {
                    if (inputImage.Data[y, x, 0] < threshold)
                    {
                        continue;
                    }
                    result.Data[y, x, 0] = byte.MaxValue;
                }
            }

            return result;
        }

        public static Image<Gray, byte> Thresholding(Image<Bgr, byte> inputImage, byte threshold)
        {
            return Thresholding(inputImage.Convert<Gray, byte>(), threshold);
        }

        #endregion

        #region Crop

        public static Image<Gray, byte> Crop(Image<Gray, byte> inputImage, System.Windows.Point topLeft, System.Windows.Point bottomRight)
        {
            Image<Gray, byte> result = new Image<Gray, byte>(Utils.GetSizeBasedOn(topLeft, bottomRight));

            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    result.Data[y, x, 0] = inputImage.Data[(int)(topLeft.Y + y), (int)(topLeft.X + x), 0];
                }
            }

            return result;
        }

        public static Image<Bgr, byte> Crop(Image<Bgr, byte> inputImage, System.Windows.Point topLeft, System.Windows.Point bottomRight)
        {
            Image<Bgr, byte> result = new Image<Bgr, byte>(Utils.GetSizeBasedOn(topLeft, bottomRight));

            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    result.Data[y, x, 0] = inputImage.Data[(int)(topLeft.Y + y), (int)(topLeft.X + x), 0];
                    result.Data[y, x, 1] = inputImage.Data[(int)(topLeft.Y + y), (int)(topLeft.X + x), 1];
                    result.Data[y, x, 2] = inputImage.Data[(int)(topLeft.Y + y), (int)(topLeft.X + x), 2];
                }
            }

            return result;
        }

        #endregion

        #region Mirror

        public static Image<Gray, byte> VerticalMirror(Image<Gray, byte> inputImage)
        {
            Image<Gray, byte> result = new Image<Gray, byte>(inputImage.Size);

            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    result.Data[y, x, 0] = inputImage.Data[y, result.Width - x - 1, 0];
                }
            }

            return result;
        }

        public static Image<Bgr, byte> VerticalMirror(Image<Bgr, byte> inputImage)
        {
            Image<Bgr, byte> result = new Image<Bgr, byte>(inputImage.Size);

            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    result.Data[y, x, 0] = inputImage.Data[y, result.Width - x - 1, 0];
                    result.Data[y, x, 1] = inputImage.Data[y, result.Width - x - 1, 1];
                    result.Data[y, x, 2] = inputImage.Data[y, result.Width - x - 1, 2];
                }
            }

            return result;
        }

        #endregion
    }
}