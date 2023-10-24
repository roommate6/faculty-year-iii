using Emgu.CV.Structure;
using Emgu.CV;
using Algorithms.Utilities;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Algorithms.Sections
{
    public class PointwiseOperations
    {
        #region Modify the brightness

        #region + / -

        public static Image<Gray, byte> ModifyBrightnessUsingAdditionOperation
            (Image<Gray, byte> inputImage, byte value)
        {
            Image<Gray, byte> outputImage = new Image<Gray, byte>(inputImage.Size);

            LookUpTable lookUpTable = new LookUpTable(inputImage,
                (byte inputPixel) =>
                {
                    int unverifiedOutput = inputPixel + value;
                    if (unverifiedOutput < 0)
                    {
                        return 0;
                    }
                    if (unverifiedOutput > 255)
                    {
                        return 255;
                    }
                    return (byte)(unverifiedOutput);
                });

            for (int y = 0; y < inputImage.Height; ++y)
            {
                for (int x = 0; x < inputImage.Width; ++x)
                {
                    outputImage.Data[y, x, 0] = lookUpTable.GetOutputPixelFor(inputImage.Data[y, x, 0]);
                }
            }

            return outputImage;
        }

        #endregion

        #endregion

        #region Modify the contrast

        public static Image<Gray, byte> ModifyContrast
            (Image<Gray, byte> inputImage, byte i1, byte o1, byte i2, byte o2)
        {
            Image<Gray, byte> outputImage = new Image<Gray, byte>(inputImage.Size);

            System.Func<byte, byte> f1 = LineEquation.GenerateFunctionBasedOn(0, 0, i1, o1);
            System.Func<byte, byte> f2 = LineEquation.GenerateFunctionBasedOn(i1, o1, i2, o2);
            System.Func<byte, byte> f3 = LineEquation.GenerateFunctionBasedOn(i2, o2, 255, 255);

            LookUpTable lookUpTable = new LookUpTable(inputImage,
                (byte inputPixel) =>
                {
                    if (inputPixel <= i1)
                    {
                        return f1(inputPixel);
                    }
                    if (inputPixel <= i2)
                    {
                        return f2(inputPixel);
                    }
                    return f3(inputPixel);
                });

            for (int y = 0; y < inputImage.Height; ++y)
            {
                for (int x = 0; x < inputImage.Width; ++x)
                {
                    outputImage.Data[y, x, 0] = lookUpTable.GetOutputPixelFor(inputImage.Data[y, x, 0]);
                }
            }

            return outputImage;
        }

        #endregion
    }
}