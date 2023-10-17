using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;

namespace Algorithms.Utilities
{
    public class LookUpTable
    {
        private Dictionary<byte, byte> _inputPixelOutputPixel;

        public byte GetOutputPixelFor(byte inputPixel)
        {
            return _inputPixelOutputPixel[inputPixel];
        }

        public LookUpTable(Image<Gray, byte> inputImage, Func<byte, byte> processingFunction)
        {
            _inputPixelOutputPixel = new Dictionary<byte, byte>();
            GenerateOutputPixels(GeneratePresentPixelsArray(inputImage), processingFunction);
        }

        private bool[] GeneratePresentPixelsArray(Image<Gray, byte> inputImage)
        {
            bool[] presentInputPixel = new bool[256];
            for (int y = 0; y < inputImage.Height; ++y)
            {
                for (int x = 0; x < inputImage.Width; ++x)
                {
                    presentInputPixel[inputImage.Data[y, x, 0]] = true;
                }
            }
            return presentInputPixel;
        }

        private void GenerateOutputPixels(bool[] presentInputPixels, Func<byte, byte> processingFunction)
        {
            for (int i = 0; i < presentInputPixels.Length; i++)
            {
                if (presentInputPixels[i])
                {
                    _inputPixelOutputPixel.Add((byte)i, processingFunction((byte)i));
                }
            }
        }
    }
}
