using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muslib
{
    public enum Accidental
    {
        DoubleFlat = -2,
        Flat = -1,
        Natural = 0,
        Sharp = 1,
        DoubleSharp = 2
    }

    public static class AccidentalExtensions
    {
        public static string GetLabel(this Accidental a)
        {
            switch (a)
            {
                case Accidental.DoubleFlat:
                    return "bb";
                case Accidental.Flat:
                    return "b";
                case Accidental.Natural:
                    return "";
                case Accidental.Sharp:
                    return "#";
                default:
                    return "##";
            }
        }
    }
}
