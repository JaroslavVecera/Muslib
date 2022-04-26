using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music
{
    public enum NonzeroAccidental
    {
        DoubleFlat = -2,
        Flat = -1,
        Sharp = 1,
        DoubleSharp = 2
    }

    public static class NonzeroAccidentalExtensions
    {
        public static string GetLabel(this NonzeroAccidental a)
        {
            switch (a)
            {
                case NonzeroAccidental.DoubleFlat:
                    return "bb";
                case NonzeroAccidental.Flat:
                    return "b";
                case NonzeroAccidental.Sharp:
                    return "#";
                default:
                    return "##";
            }
        }
    }
}
