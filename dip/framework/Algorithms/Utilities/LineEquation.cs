using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Utilities
{
    public class LineEquation
    {
        public static Func<byte, byte> GenerateFunctionBasedOn(byte x1, byte y1, byte x2, byte y2)
        {
            byte m = (byte)((byte)(y2 - y1) / (byte)(x2 - x1));
            return new Func<byte, byte>((byte x) => { return (byte)(m * (x - x1) + y1); });
        }
    }
}
