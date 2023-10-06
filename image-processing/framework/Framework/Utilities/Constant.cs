using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utilities
{
    public static class Constant
    {
        public static class Message
        {
            #region Warnings

            public const string NullInitialImage =
                "You can't use this action because you haven't added an image yet." +
                "Please add an image using the \"file\" menu button.";

            public const string ArgumentsMissingInDialogBox =
                "You should put a value inside all the text boxes before hitting \"Done\".";

            public const string ParsingStringToByte =
                "The input should be a number from interval [0, 255].";

            public static string ByteOutOfBounds(byte leftBound, byte rightBound)
            {
                return string.Format(
                    "Byte out of bounds. Insert a value from the interval [{0}, {1}].",
                    leftBound, rightBound);
            }

            #endregion
        }

        public static class Number
        {
            #region Byte

            public const byte ThresholdLeftBound = 10;
            public const byte ThresholdRightBound = 154;

            #endregion
        }
    }
}
