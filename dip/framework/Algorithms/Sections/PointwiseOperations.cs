using Emgu.CV.Structure;
using Emgu.CV;
using Algorithms.Utilities;

namespace Algorithms.Sections
{
    public class PointwiseOperations
    {
        #region Modify brightness

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
    }
}