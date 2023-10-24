using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Algorithms.Utilities
{
    public class LineEquation
    {
        public static Func<byte, byte> GenerateFunctionBasedOn(byte x1, byte y1, byte x2, byte y2)
        {
            double m = (double)((double) (y2 - y1) / (x2 - x1)); // conversie de la float la byte cu +0,5; 10,6 -> 10 X, 10,6 -> 11 Ok; Conversia se face cu +0,5 si dupa conversie.
            return new Func<byte, byte>((byte x) => { return (byte)(m * (x - x1) + y1); });
        }
    }
}
